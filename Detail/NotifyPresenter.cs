/* ------------------------------------------------------------------------- */
///
/// NotifyPresenter.cs
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
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace Cube.Forms
{
    /* --------------------------------------------------------------------- */
    ///
    /// Cube.Forms.NotifyPresenter
    /// 
    /// <summary>
    /// NotifyForm と NotifyQueue を繋げるためのクラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class NotifyPresenter
    {
        #region Constructors

        /* --------------------------------------------------------------------- */
        ///
        /// NotifyPresenter
        /// 
        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        public NotifyPresenter(NotifyForm view)
        {
            View = view;
            View.Hidden += View_Hidden;

            Items = new ObservableCollection<NotifyItem>();
            Items.CollectionChanged += Model_CollectionChanged;
        }

        /* --------------------------------------------------------------------- */
        ///
        /// NotifyPresenter
        /// 
        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        public NotifyPresenter() : this(new NotifyForm()) { }

        #endregion

        #region Properties

        /* --------------------------------------------------------------------- */
        ///
        /// View
        /// 
        /// <summary>
        /// ビューオブジェクトを取得します。
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        public NotifyForm View { get; private set; }

        /* --------------------------------------------------------------------- */
        ///
        /// Items
        /// 
        /// <summary>
        /// 未通知の項目一覧を取得します。
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        public ObservableCollection<NotifyItem> Items { get; private set; }

        #endregion

        #region Event handlers

        /* --------------------------------------------------------------------- */
        ///
        /// Model_Queued
        /// 
        /// <summary>
        /// キューにデータが追加された時に実行されるハンドラです。
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        private void Model_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action != NotifyCollectionChangedAction.Add) return;
            if (Items.Count <= 0 || View.IsBusy) return;
            Execute(Pop());
        }

        /* --------------------------------------------------------------------- */
        ///
        /// View_Hidden
        /// 
        /// <summary>
        /// 画面が非表示になった直後に実行されるハンドラです。
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        private void View_Hidden(object sender, EventArgs e)
        {
            if (Items.Count <= 0) return;
            Execute(Pop());
        }

        #endregion

        #region Implementations

        /* --------------------------------------------------------------------- */
        ///
        /// Pop
        /// 
        /// <summary>
        /// 未通知一覧から最初の項目を取得します。
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        private NotifyItem Pop()
        {
            if (Items.Count <= 0) return null;
            var dest = Items[0];
            Items.RemoveAt(0);
            return dest;
        }

        /* --------------------------------------------------------------------- */
        ///
        /// Execute
        /// 
        /// <summary>
        /// 通知を実行します。
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        private void Execute(NotifyItem item)
        {
            View.Level = item.Level;
            View.Title = item.Title;
            View.InitialDelay = (int)item.InitialDelay.TotalMilliseconds;
            View.Tag = item.Data;
            if (item.Image != null) View.Image = item.Image;

            View.Show((int)item.DisplayTime.TotalMilliseconds);
        }

        #endregion
    }
}
