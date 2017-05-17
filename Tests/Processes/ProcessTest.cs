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
using System.Runtime.InteropServices;
using NUnit.Framework;

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
        /// StartAsCurrentUser
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
        public void StartAsCurrentUser()
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
        /// StartAsCurrentThread
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
        public void StartAsCurrentThread()
            => Assert.That(() =>
            {
                var tid  = GetCurrentThreadId();
                var exec = "explorer.exe";
                var proc = Cube.Processes.Process.StartAs(tid, exec, null);
                proc.Kill();
            },
            Throws.TypeOf<Win32Exception>().And.Message.EqualTo("OpenThreadToken"));

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
