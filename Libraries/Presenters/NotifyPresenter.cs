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
using System.Collections.Specialized;

namespace Cube.Forms
{
    /* --------------------------------------------------------------------- */
    ///
    /// NotifyPresenter
    /// 
    /// <summary>
    /// NotifyForm と NotifyQueue を繋げるためのクラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class NotifyPresenter : PresenterBase<NotifyForm, NotifyQueue>
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
        public NotifyPresenter() : this(new NotifyForm(), new NotifyQueue()) { }

        /* --------------------------------------------------------------------- */
        ///
        /// NotifyPresenter
        /// 
        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        public NotifyPresenter(NotifyForm view) : this(view, new NotifyQueue()) { }

        /* --------------------------------------------------------------------- */
        ///
        /// NotifyPresenter
        /// 
        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        public NotifyPresenter(NotifyForm view, NotifyQueue model)
            : base(view, model)
        {
            View.Hidden += View_Hidden;
            Model.CollectionChanged += Model_CollectionChanged;
        }

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

            Sync(() =>
            {
                if (Model.Count <= 0 || View.IsBusy) return;
                Execute(Model.Dequeue());
            });
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
            if (Model.Count <= 0) return;
            Sync(() => Execute(Model.Dequeue()));
        }

        #endregion

        #region Implementations

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
            View.Description = item.Description;
            View.InitialDelay = (int)item.InitialDelay.TotalMilliseconds;
            View.Tag = item.Data;
            if (item.Image != null) View.Image = item.Image;

            View.Show((int)item.DisplayTime.TotalMilliseconds);
        }

        #endregion
    }
}
