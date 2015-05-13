/* ------------------------------------------------------------------------- */
///
/// DemoStockIcons.cs
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
using System.Windows.Forms;

namespace Cube.Forms.Demo
{
    /* --------------------------------------------------------------------- */
    ///
    /// Cube.Forms.Demo.DemoStockIcons
    /// 
    /// <summary>
    /// システムアイコン一覧を表示するためのデモ用クラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public partial class DemoStockIcons : FormBase
    {
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// DemoStockIcons
        ///
        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public DemoStockIcons()
        {
            InitializeComponent();

            IconListView.LargeImageList = new ImageList();
            IconListView.LargeImageList.ImageSize = new Size(48, 48);
            IconListView.LargeImageList.ColorDepth = ColorDepth.Depth32Bit;
            foreach (StockIcons.Kind kind in Enum.GetValues(typeof(StockIcons.Kind)))
            {
                // var icon = Cube.Forms.StockIcons.Create(kind, StockIcons.Size.ExtraLarge);
                var icon = Cube.Forms.StockIcons.Create(kind, StockIcons.Size.Large);
                var text = string.Format("{0} ({1}x{2})\n{3}", (int)kind, icon.Size.Width, icon.Size.Height, kind);
                IconListView.LargeImageList.Images.Add(icon.ToBitmap());
                IconListView.Items.Add(new ListViewItem(text, IconListView.LargeImageList.Images.Count - 1));
            }
        }

        #endregion
    }
}
