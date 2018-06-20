/* ------------------------------------------------------------------------- */
//
// Copyright (c) 2010 CubeSoft, Inc.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//  http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
/* ------------------------------------------------------------------------- */
using System;
using System.Reflection;

namespace Cube.Forms.Controls
{
    /* --------------------------------------------------------------------- */
    ///
    /// FormExtension
    ///
    /// <summary>
    /// System.Windows.Forms.Form の拡張用クラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public static class FormExtension
    {
        #region Methods

        #region UpdateText

        /* ----------------------------------------------------------------- */
        ///
        /// UpdateText
        ///
        /// <summary>
        /// フォームのタイトルを "message - ProductName" と言う表記で
        /// 更新します。
        /// </summary>
        ///
        /// <param name="form">フォーム</param>
        /// <param name="message">タイトルに表示するメッセージ</param>
        ///
        /* ----------------------------------------------------------------- */
        public static void UpdateText(this System.Windows.Forms.Form form, string message) =>
            UpdateText(form, message, AssemblyReader.Default);

        /* ----------------------------------------------------------------- */
        ///
        /// UpdateText
        ///
        /// <summary>
        /// フォームのタイトルを "message - ProductName" と言う表記で
        /// 更新します。
        /// </summary>
        ///
        /// <param name="form">フォーム</param>
        /// <param name="message">タイトルに表示するメッセージ</param>
        /// <param name="assembly">アセンブリ情報</param>
        ///
        /* ----------------------------------------------------------------- */
        public static void UpdateText(this System.Windows.Forms.Form form,
            string message, Assembly assembly) =>
            UpdateText(form, message, new AssemblyReader(assembly));

        /* ----------------------------------------------------------------- */
        ///
        /// UpdateText
        ///
        /// <summary>
        /// フォームのタイトルを "message - ProductName" と言う表記で
        /// 更新します。
        /// </summary>
        ///
        /// <param name="form">フォーム</param>
        /// <param name="message">タイトルに表示するメッセージ</param>
        /// <param name="assembly">アセンブリ情報</param>
        ///
        /* ----------------------------------------------------------------- */
        public static void UpdateText(this System.Windows.Forms.Form form,
            string message, AssemblyReader assembly)
        {
            var ss = new System.Text.StringBuilder();
            ss.Append(message);
            if (!string.IsNullOrEmpty(message) && !string.IsNullOrEmpty(assembly.Product)) ss.Append(" - ");
            ss.Append(assembly.Product);

            form.Text = ss.ToString();
        }

        #endregion

        #region TopMost

        /* ----------------------------------------------------------------- */
        ///
        /// BringToFront
        ///
        /// <summary>
        /// フォームを最前面に表示します。
        /// </summary>
        ///
        /// <param name="form">フォーム</param>
        ///
        /* ----------------------------------------------------------------- */
        public static void BringToFront(this System.Windows.Forms.Form form)
        {
            form.ResetTopMost();
            form.Activate();
        }

        /* ----------------------------------------------------------------- */
        ///
        /// ResetTopMost
        ///
        /// <summary>
        /// TopMost の値をリセットします。
        /// </summary>
        ///
        /// <param name="form">フォーム</param>
        ///
        /* ----------------------------------------------------------------- */
        public static void ResetTopMost(this System.Windows.Forms.Form form)
        {
            var tmp = form.TopMost;
            form.TopMost = false;
            form.TopMost = true;
            form.TopMost = tmp;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// SetTopMost
        ///
        /// <summary>
        /// フォームを最前面に表示します。
        /// </summary>
        ///
        /// <param name="form">フォーム</param>
        /// <param name="active">アクティブ状態にするかどうか</param>
        ///
        /// <remarks>
        /// SetTopMost メソッドは主にフォーカスを奪わずに最前面に表示する
        /// 時に使用します。この場合、最前面に表示された状態でも TopMost
        /// プロパティは false となります。
        /// </remarks>
        ///
        /* ----------------------------------------------------------------- */
        public static void SetTopMost(this System.Windows.Forms.Form form, bool active)
        {
            if (active)
            {
                form.TopMost = true;
                form.Activate();
            }
            else
            {
                const uint SWP_NOSIZE         = 0x0001;
                const uint SWP_NOMOVE         = 0x0002;
                const uint SWP_NOACTIVATE     = 0x0010;
                const uint SWP_NOSENDCHANGING = 0x0400;

                User32.NativeMethods.SetWindowPos(form.Handle,
                    (IntPtr)(-1), /* HWND_TOPMOST */
                    0, 0, 0, 0,
                    SWP_NOACTIVATE | SWP_NOMOVE | SWP_NOSENDCHANGING | SWP_NOSIZE
                );
            }
        }

        #endregion

        #endregion
    }
}
