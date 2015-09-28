﻿/* ------------------------------------------------------------------------- */
///
/// NtsForm.cs
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
using System.Drawing;
using log4net;

namespace Cube.Forms
{
    /* --------------------------------------------------------------------- */
    ///
    /// Cube.Forms.NtsForm
    /// 
    /// <summary>
    /// 自動スケールモードを無効にした Form クラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class NtsForm : System.Windows.Forms.Form
    {
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// NtsForm
        ///
        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public NtsForm()
            : base()
        {
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            DoubleBuffered = true;
            Font = FontFactory.Create(12, Font.Style, GraphicsUnit.Pixel);
            Logger = LogManager.GetLogger(GetType());
        }

        #endregion

        #region Properties

        /* ----------------------------------------------------------------- */
        ///
        /// Logger
        ///
        /// <summary>
        /// ログ出力用オブジェクトを取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected ILog Logger { get; private set; }

        #endregion
    }
}
