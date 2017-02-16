/* ------------------------------------------------------------------------- */
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
using System.ComponentModel;
using System.Linq;
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
        #region Methods

        /* ----------------------------------------------------------------- */
        ///
        /// StartAsActiveUser
        ///
        /// <summary>
        /// アクティブユーザ権限でプログラムを実行します。
        /// </summary>
        /// 
        /// <param name="program">実行プログラムのパス</param>
        /// <param name="arguments">プログラムの引数</param>
        /// <returns>実行に成功した <c>Process</c> オブジェクト</returns>
        ///
        /* ----------------------------------------------------------------- */
        public static System.Diagnostics.Process StartAsActiveUser(string program, string[] arguments)
            => StartAsActiveUser(CreateCmdLine(program, arguments));

        /* ----------------------------------------------------------------- */
        ///
        /// StartAsActiveUser
        ///
        /// <summary>
        /// アクティブユーザ権限でプログラムを実行します。
        /// </summary>
        /// 
        /// <param name="cmdline">実行するコマンドライン</param>
        /// <returns>実行に成功した <c>Process</c> オブジェクト</returns>
        ///
        /* ----------------------------------------------------------------- */
        public static System.Diagnostics.Process StartAsActiveUser(string cmdline)
            => StartAs(GetActiveSessionToken(), cmdline);

        /* ----------------------------------------------------------------- */
        ///
        /// StartAsActiveUser
        ///
        /// <summary>
        /// 指定されたトークンの権限でプログラムを実行します。
        /// </summary>
        /// 
        /// <param name="token">実行ユーザのトークン</param>
        /// <param name="program">実行プログラムのパス</param>
        /// <param name="arguments">プログラムの引数</param>
        /// <returns>実行に成功した <c>Process</c> オブジェクト</returns>
        ///
        /* ----------------------------------------------------------------- */
        public static System.Diagnostics.Process StartAs(IntPtr token,
            string program, string[] arguments)
            => StartAs(token, CreateCmdLine(program, arguments));

        /* ----------------------------------------------------------------- */
        ///
        /// StartAsActiveUser
        ///
        /// <summary>
        /// 指定されたトークンの権限でプログラムを実行します。
        /// </summary>
        /// 
        /// <param name="token">実行ユーザのトークン</param>
        /// <param name="cmdline">実行するコマンドライン</param>
        /// <returns>実行に成功した <c>Process</c> オブジェクト</returns>
        ///
        /* ----------------------------------------------------------------- */
        public static System.Diagnostics.Process StartAs(IntPtr token, string cmdline)
        {
            var env = IntPtr.Zero;

            try
            {
                if (token == IntPtr.Zero) throw new ArgumentException("PrimaryToken");

                env = GetEnvironmentBlock(token);
                if (env == IntPtr.Zero) throw new ArgumentException("EnvironmentBlock");

                return CreateProcessAsUser(cmdline, token, env);
            }
            finally
            {
                if (env != IntPtr.Zero) UserEnv.NativeMethods.DestroyEnvironmentBlock(env);
                CloseHandle(token);
            }
        }

        #endregion

        #region Implementations

        /* ----------------------------------------------------------------- */
        ///
        /// CreateCmdLine
        ///
        /// <summary>
        /// コマンドライン用の文字列を生成します。
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        private static string CreateCmdLine(string program, string[] arguments)
            => arguments == null ?
            $"\"{program}\"" :
            arguments.Aggregate($"\"{program}\"", (s, x) => s + " " + $"\"{x}\"");

        /* ----------------------------------------------------------------- */
        ///
        /// CreateProcessAsUser
        ///
        /// <summary>
        /// Win32 API の CreateProcessAsUser を実行します。
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        private static System.Diagnostics.Process CreateProcessAsUser(string cmdline, IntPtr token, IntPtr env)
        {
            var si = new STARTUPINFO();
            si.cb          = (uint)Marshal.SizeOf(si);
            si.lpDesktop   = @"WinSta0\Default";
            si.wShowWindow = 0x05;  // SW_SHOW
            si.dwFlags     = 0x01 | // STARTF_USESHOWWINDOW 
                             0x40;  // STARTF_FORCEONFEEDBACK

            var sa = new SECURITY_ATTRIBUTES();
            sa.nLength = (uint)Marshal.SizeOf(sa);

            var thread = new SECURITY_ATTRIBUTES();
            thread.nLength = (uint)Marshal.SizeOf(thread);

            var pi = new PROCESS_INFORMATION();
            try
            {
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

                return System.Diagnostics.Process.GetProcessById((int)pi.dwProcessId);
            }
            finally
            {
                CloseHandle(pi.hProcess);
                CloseHandle(pi.hThread);
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// GetActiveSessionToken
        ///
        /// <summary>
        /// アクティブなセッションに対応するトークンを取得します。
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        private static IntPtr GetActiveSessionToken()
        {
            var id = Kernel32.NativeMethods.WTSGetActiveConsoleSessionId();
            var token = IntPtr.Zero;

            if (!WtsApi32.NativeMethods.WTSQueryUserToken(id, out token)) Win32Error("WTSQueryUserToken");
            try { return GetPrimaryToken(token, SECURITY_IMPERSONATION_LEVEL.SecurityImpersonation); }
            finally { CloseHandle(token); }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// GetPrimaryToken
        ///
        /// <summary>
        /// プライマリトークンを取得します。
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        private static IntPtr GetPrimaryToken(IntPtr token, SECURITY_IMPERSONATION_LEVEL level)
        {
            var dest = IntPtr.Zero;
            var attr = new SECURITY_ATTRIBUTES();
            attr.nLength = (uint)Marshal.SizeOf(attr);
            var result = AdvApi32.NativeMethods.DuplicateTokenEx(
                token,
                0x0001 | // TOKEN_ASSIGN_PRIMARY
                0x0002 | // TOKEN_DUPLICATE
                0x0004 | // TOKEN_IMPERSONATE
                0x0008,  // TOKEN_QUERY
                ref attr,
                (int)level,
                (int)TOKEN_TYPE.TokenPrimary,
                ref dest
            );

            AdvApi32.NativeMethods.RevertToSelf();

            if (!result) Win32Error("DuplicateTokenEx");
            return dest;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// GetEnvironmentBlock
        ///
        /// <summary>
        /// 環境ブロックを取得します。
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
        /// CloseHandle
        ///
        /// <summary>
        /// ハンドルを閉じます。
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        private static void CloseHandle(IntPtr handle)
        {
            if (handle == IntPtr.Zero) return;
            Kernel32.NativeMethods.CloseHandle(handle);
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
            throw new Win32Exception(Marshal.GetLastWin32Error(), message);
        }

        #endregion
    }
}
