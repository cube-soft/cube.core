/* ------------------------------------------------------------------------- */
///
/// TitleControl.cs
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
using System.Drawing;

namespace Cube.Forms
{
    /* --------------------------------------------------------------------- */
    ///
    /// TitleControl
    /// 
    /// <summary>
    /// タイトルバーを表すコントロールクラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class TitleControl : UserControl
    {
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// TitleControl
        /// 
        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public TitleControl() : base() { }

        #endregion

        #region Override methods

        /* ----------------------------------------------------------------- */
        ///
        /// OnNcHitTest
        /// 
        /// <summary>
        /// マウスのヒットテスト発生時に実行されます。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected override void OnNcHitTest(QueryEventArgs<Point, Position> e)
        {
            e.Cancel = true;

            base.OnNcHitTest(e);
            if (!e.Cancel) return;

            e.Result = Position.Transparent;
            e.Cancel = false;
        }

        #endregion
    }
}
