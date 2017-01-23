/* ------------------------------------------------------------------------- */
///
/// Process.cs
/// 
/// Copyright (c) 2010 CubeSoft, Inc.
/// 
/// Licensed under the Apache License, Version 2.0 (the "License");
/// you may not use this file except in compliance with the License.
/// You may obtain a copy of the License at
///
///  http://www.apache.org/licenses/LICENSE-2.0
///
/// Unless required by applicable law or agreed to in writing, software
/// distributed under the License is distributed on an "AS IS" BASIS,
/// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
/// See the License for the specific language governing permissions and
/// limitations under the License.
///
/* ------------------------------------------------------------------------- */
using System;
using System.Linq;
using System.Management;
using System.Runtime.InteropServices;

namespace Cube.Processes
{
    /* --------------------------------------------------------------------- */
    ///
    /// Process
    /// 
    /// <summary>
    /// プロセスを扱うクラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public static class Process
    {
        #region Properties

        /* ----------------------------------------------------------------- */
        ///
        /// LookupKey
        ///
        /// <summary>
        /// プロセスに関する情報を検索する際に利用するプロセス名を取得
        /// または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public static string LookupKey { get; set; } = "explorer";

        #endregion

        #region Methods

        /* ----------------------------------------------------------------- */
        ///
        /// StartAs
        ///
        /// <summary>
        /// 指定されたユーザ名でプログラムを実行します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public static void StartAs(string program, string[] args, string username)
            => StartAs(
            args == null ? $"\"{program}\"" : args.Aggregate($"\"{program}\"", (s, x) => s + " " + $"\"{x}\""),
            username
        );

        /* ----------------------------------------------------------------- */
        ///
        /// StartAs
        ///
        /// <summary>
        /// 指定されたユーザ名でコマンドラインを実行します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public static void StartAs(string cmdline, string username)
        {
            var id = GetProcessId(username);
            if (id < 0) throw new ArgumentException($"{LookupKey}:process not found");

            var token = GetPrimaryToken(id);
            if (token == IntPtr.Zero) throw new ArgumentException("PrimaryToken");

            var env = GetEnvironmentBlock(token);
            if (env == IntPtr.Zero) throw new ArgumentException("EnvironmentBlock");

            try { CreateProcessAsUser(cmdline, token, env); }
            finally
            {
                if (env != IntPtr.Zero) UserEnv.NativeMethods.DestroyEnvironmentBlock(env);
            }
        }

        #endregion

        #region Others

        /* ----------------------------------------------------------------- */
        ///
        /// CreateProcessAsUser
        ///
        /// <summary>
        /// Win32 API の CreateProcessAsUser を実行します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private static void CreateProcessAsUser(string cmdline, IntPtr token, IntPtr env)
        {
            var si = new STARTUPINFO();
            si.cb          = (uint)Marshal.SizeOf(si);
            si.lpDesktop   = @"WinSta0\Default";
            si.wShowWindow = 0x05; // SW_SHOW
            si.dwFlags     = 0x01 | // STARTF_USESHOWWINDOW 
                             0x40;  // STARTF_FORCEONFEEDBACK

            var sa = new SECURITY_ATTRIBUTES();
            sa.nLength = (uint)Marshal.SizeOf(sa);

            var thread = new SECURITY_ATTRIBUTES();
            thread.nLength = (uint)Marshal.SizeOf(thread);

            var pi = new PROCESS_INFORMATION();

            if (!AdvApi32.NativeMethods.CreateProcessAsUser(
                token,
                null,
                cmdline,
                ref sa,
                ref thread,
                false,
                0x0400, // CREATE_UNICODE_ENVIRONMENT
                env,
                null,
                ref si,
                out pi
            )) Win32Error("CreateProcessAsUser");
        }

        /* ----------------------------------------------------------------- */
        ///
        /// GetProcessId
        ///
        /// <summary>
        /// LookupKey で指定されたプログラムの内、指定されたユーザ名で
        /// 実行されているプロセスの ID を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private static int GetProcessId(string username)
        {
            var results = System.Diagnostics.Process.GetProcessesByName(LookupKey);
            if (results == null || results.Length <= 0) return -1;

            foreach (var ps in results)
            {
                var id = ps.Id;
                if (GetOwner(id) == username) return id;
            }
            return -1;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// GetPrimaryToken
        ///
        /// <summary>
        /// プロセス ID に対応するトークンを取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private static IntPtr GetPrimaryToken(int id)
        {
            var token   = IntPtr.Zero;
            var process = System.Diagnostics.Process.GetProcessById(id);

            if (!AdvApi32.NativeMethods.OpenProcessToken(
                process.Handle,
                0x02 /* TOKEN_DUPLICATE */,
                ref token
            )) Win32Error("OpenProcessToken");

            var dest = IntPtr.Zero;
            var attr = new SECURITY_ATTRIBUTES();
            attr.nLength = (uint)Marshal.SizeOf(attr);
            var result = AdvApi32.NativeMethods.DuplicateTokenEx(
                token,
                0x0001 | // TOKEN_ASSIGN_PRIMARY
                0x0002 | // TOKEN_DUPLICATE
                0x0008,  // TOKEN_QUERY
                ref attr,
                (int)SECURITY_IMPERSONATION_LEVEL.SecurityIdentification,
                (int)TOKEN_TYPE.TokenPrimary,
                ref dest
            );

            Kernel32.NativeMethods.CloseHandle(token);
            if (!result) Win32Error("DuplicateTokenEx");

            return dest;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// GetEnvironmentBlock
        ///
        /// <summary>
        /// EnvironmentBlock を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private static IntPtr GetEnvironmentBlock(IntPtr token)
        {
            var dest = IntPtr.Zero;
            var result = UserEnv.NativeMethods.CreateEnvironmentBlock(ref dest, token, false);
            if (!result) Win32Error("CreateEnvironmentBlock");
            return dest;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// GetOwner
        ///
        /// <summary>
        /// プロセスの所有者を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private static string GetOwner(int id)
        {
            var query = $"Select * From Win32_Process Where ProcessID = {id}";
            using (var searcher = new ManagementObjectSearcher(query))
            using (var results = searcher.Get())
            {
                foreach (ManagementObject obj in results)
                {
                    try
                    {
                        var args = new string[] { string.Empty };
                        int value = Convert.ToInt32(obj.InvokeMethod("GetOwner", args));
                        if (value == 0) return args[0];
                    }
                    catch (Exception err) { Cube.Log.Operations.Error(typeof(Process), err.Message, err); }
                }
            }
            return null;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Win32Error
        ///
        /// <summary>
        /// Win32 Error の値を持つ例外を送出します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private static void Win32Error(string message)
        {
            var s = $"{message}:{Marshal.GetLastWin32Error()}";
            Cube.Log.Operations.Error(typeof(Process), s);
            throw new ArgumentException(s);
        }

        #endregion
    }
}
