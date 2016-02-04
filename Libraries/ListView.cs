/* ------------------------------------------------------------------------- */
///
/// Button.cs
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
using System.Collections.Generic;
using System.Linq;

namespace Cube.Forms
{
    /* --------------------------------------------------------------------- */
    ///
    /// ListView
    /// 
    /// <summary>
    /// リストビューを表示するクラスです。
    /// </summary>
    /// 
    /* --------------------------------------------------------------------- */
    public class ListView : System.Windows.Forms.ListView
    {
        #region Properties

        /* ----------------------------------------------------------------- */
        ///
        /// Theme
        ///
        /// <summary>
        /// 表示用のテーマを取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Browsable(true)]
        [DefaultValue(WindowTheme.Normal)]
        public WindowTheme Theme
        {
            get { return _theme; }
            set
            {
                if (_theme != value)
                {
                    _theme = value;
                    UpdateTheme(_theme);
                }
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Count
        ///
        /// <summary>
        /// 項目数を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public int Count
        {
            get { return Items.Count; }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// AnyItemsSelected
        ///
        /// <summary>
        /// 項目を 1 つ以上選択しているかどうかを示す値を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public bool AnyItemsSelected
        {
            get { return SelectedIndices != null && SelectedIndices.Count > 0; }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Converter
        ///
        /// <summary>
        /// 変換用オブジェクトを取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public IListViewItemConverter Converter { get; set; }

        #endregion

        #region Methods

        /* ----------------------------------------------------------------- */
        ///
        /// Add
        ///
        /// <summary>
        /// 項目を追加します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public void Add<T>(T item)
        {
            Items.Add(
                Converter != null ?
                Converter.Convert(item) :
                new System.Windows.Forms.ListViewItem(item.ToString())
            );
            HackAlignmentBug();
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Insert
        ///
        /// <summary>
        /// 指定されたインデックスに項目を追加します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public void Insert<T>(int index, T item)
        {
            Items.Insert(index,
                Converter != null ?
                Converter.Convert(item) :
                new System.Windows.Forms.ListViewItem(item.ToString())
            );
            HackAlignmentBug();
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Replace
        ///
        /// <summary>
        /// 指定されたインデックスの内容を置換します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public void Replace<T>(int index, T item)
        {
            if (index < 0 || index >= Items.Count) return;
            Items[index] = Converter != null ?
                           Converter.Convert(item) :
                           new System.Windows.Forms.ListViewItem(item.ToString());
        }

        /* ----------------------------------------------------------------- */
        ///
        /// RemoveItems
        ///
        /// <summary>
        /// 項目を削除します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public void RemoveItems(IEnumerable<int> indices)
        {
            foreach (var index in indices.OrderByDescending(x => x))
            {
                if (index < 0 || index >= Items.Count) continue;
                Items.RemoveAt(index);
            }
            
        }

        /* ----------------------------------------------------------------- */
        ///
        /// RemoveItems
        ///
        /// <summary>
        /// 選択されている項目を削除します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public void RemoveItems()
        {
            var indices = new List<int>();
            foreach (int index in SelectedIndices) indices.Add(index);
            RemoveItems(indices);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// ClearItems
        ///
        /// <summary>
        /// 全ての項目を削除します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public void ClearItems()
        {
            Items.Clear();
        }

        /* ----------------------------------------------------------------- */
        ///
        /// MoveItems
        ///
        /// <summary>
        /// 項目を移動します。
        /// </summary>
        /// 
        /// <remarks>
        /// offset が正の数の場合は後ろに、負の数の場合は前に移動します。
        /// </remarks>
        ///
        /* ----------------------------------------------------------------- */
        public void MoveItems(IEnumerable<int> indices, int offset)
        {
            if (offset == 0) return;

            var sorted = offset > 0 ?
                         indices.OrderByDescending(x => x) :
                         indices.OrderBy(x => x);

            foreach (var index in sorted)
            {
                if (index < 0 || index >= Items.Count) continue;
                var item = Items[index];
                Items.RemoveAt(index);

                var newindex = Math.Max(Math.Min(index + offset, Items.Count), 0);
                Items.Insert(newindex, item);
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// MoveItems
        ///
        /// <summary>
        /// 選択されている項目を移動します。
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        public void MoveItems(int offset)
        {
            var indices = new List<int>();
            foreach (int index in SelectedIndices) indices.Add(index);
            MoveItems(indices, offset);
        }

        #endregion

        #region Override methods

        /* ----------------------------------------------------------------- */
        ///
        /// OnCreateControl
        ///
        /// <summary>
        /// コントロールの生成時に実行されるハンドラです。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected override void OnCreateControl()
        {
            UpdateTheme(_theme);
            base.OnCreateControl();
        }

        /* ----------------------------------------------------------------- */
        ///
        /// OnSelectedIndexChanged
        ///
        /// <summary>
        /// 選択項目が変更された時に実行されるハンドラです。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected override void OnSelectedIndexChanged(EventArgs e)
        {
            base.OnSelectedIndexChanged(e);
            if (Theme == WindowTheme.Normal) return;
            User32.SendMessage(Handle, 0x127, (IntPtr)0x10001, IntPtr.Zero);
        }
        /* ----------------------------------------------------------------- */
        ///
        /// OnEnter
        ///
        /// <summary>
        /// カーソルが領域内に侵入した時に実行されるハンドラです。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected override void OnEnter(EventArgs e)
        {
            base.OnEnter(e);
            if (Theme == WindowTheme.Normal) return;
            User32.SendMessage(Handle, 0x127, (IntPtr)0x10001, IntPtr.Zero);
        }

        #endregion

        #region Other private methods

        /* ----------------------------------------------------------------- */
        ///
        /// UpdateTheme
        ///
        /// <summary>
        /// 表示用のテーマを更新します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void UpdateTheme(WindowTheme theme)
        {
            if (theme == WindowTheme.Normal) UxTheme.SetWindowTheme(Handle, null, null);
            else UxTheme.SetWindowTheme(Handle, theme.ToString(), null);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// HackAlignmentBug
        ///
        /// <summary>
        /// Aligment に関連したバグの影響で表示順序がおかしくなる場合が
        /// あるので、強制的に再描画させます。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void HackAlignmentBug()
        {
            if (View == System.Windows.Forms.View.List ||
                View == System.Windows.Forms.View.Details) return;

            var alignment = Alignment;
            Alignment = System.Windows.Forms.ListViewAlignment.Default;
            Alignment = alignment;
        }

        #endregion

        #region Fields
        private WindowTheme _theme = WindowTheme.Normal;
        #endregion
    }
}
