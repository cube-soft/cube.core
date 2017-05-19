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
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices;
using NUnit.Framework;
using Cube.Processes;

namespace Cube.Tests
{
    /* --------------------------------------------------------------------- */
    ///
    /// ProcessTest
    /// 
    /// <summary>
    /// Process クラスのテスト用クラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [TestFixture]
    class ProcessTest
    {
        #region Tests

        /* ----------------------------------------------------------------- */
        ///
        /// StartAsActiveUser
        ///
        /// <summary>
        /// ログオン中のユーザでプロセスを実行するテストを実行します。
        /// </summary>
        /// 
        /// <remarks>
        /// TODO: 現在、通常ユーザからの実行には失敗するので要検証。
        /// </remarks>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void StartAsActiveUser()
            => Assert.That(() =>
            {
                var exec = "explorer.exe";
                var proc = Cube.Processes.Process.StartAsActiveUser(exec, null);
                proc.Kill();
            },
            Throws.TypeOf<Win32Exception>().And.Message.EqualTo("WTSQueryUserToken"));

        /* ----------------------------------------------------------------- */
        ///
        /// StartAs_UserName
        ///
        /// <summary>
        /// ログオン中のユーザでプロセスを実行するテストを実行します。
        /// </summary>
        /// 
        /// <remarks>
        /// TODO: 現在、通常ユーザからの実行には失敗するので要検証。
        /// </remarks>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void StartAs_UserName()
            => Assert.That(() =>
            {
                var user = Environment.UserName;
                var exec = "explorer.exe";
                var proc = Cube.Processes.Process.StartAs(user, exec, null);
                proc.Kill();
            },
            Throws.TypeOf<Win32Exception>().And.Message.EqualTo("WTSQueryUserToken"));

        /* ----------------------------------------------------------------- */
        ///
        /// StartAs_ThreadID
        ///
        /// <summary>
        /// 現在のスレッド上でプロセスを実行するテストを実行します。
        /// </summary>
        /// 
        /// <remarks>
        /// TODO: 現在、通常ユーザからの実行には失敗するので要検証。
        /// </remarks>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void StartAs_ThreadID()
            => Assert.That(() =>
            {
                var tid  = GetCurrentThreadId();
                var exec = "explorer.exe";
                var proc = Cube.Processes.Process.StartAs(tid, exec, null);
                proc.Kill();
            },
            Throws.TypeOf<Win32Exception>().And.Message.EqualTo("OpenThreadToken"));

        /* ----------------------------------------------------------------- */
        ///
        /// Activate
        ///
        /// <summary>
        /// 指定されたプロセスのウィンドウをアクティブ化するテストを
        /// 実行します。
        /// </summary>
        /// 
        /// <remarks>
        /// TODO: 成功するテストケースを作成できていないので要検討。
        /// </remarks>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void Activate()
            => Assert.DoesNotThrow(() => System.Diagnostics.Process.GetCurrentProcess().Activate());

        #endregion

        #region Helper methods

        /* ----------------------------------------------------------------- */
        ///
        /// GetCurrentThreadId
        ///
        /// <summary>
        /// https://msdn.microsoft.com/en-us/library/windows/desktop/ms683183.aspx
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        [DllImport("kernel32.dll")]
        private static extern uint GetCurrentThreadId();

        #endregion
    }
}
