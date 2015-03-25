/* ------------------------------------------------------------------------- */
///
/// NotifyPresenter.cs
/// 
/// Copyright (c) 2010 CubeSoft, Inc.
/// 
/// This is distributed under the Microsoft Public License (Ms-PL).
/// See http://www.opensource.org/licenses/ms-pl.html
///
/* ------------------------------------------------------------------------- */
using System;

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
        public NotifyPresenter(NotifyForm view, NotifyQueue queue)
        {
            View = view;
            View.Hidden += View_Hidden;

            Queue = queue;
            Queue.Queued += Model_Queued;
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
        /// Queue
        /// 
        /// <summary>
        /// 通知キューを取得します。
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        public NotifyQueue Queue { get; private set; }

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
        private void Model_Queued(object sender, EventArgs e)
        {
            if (Queue.Count <= 0 || View.IsBusy) return;
            Execute(Queue.Dequeue());
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
            if (Queue.Count <= 0) return;
            Execute(Queue.Dequeue());
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
            View.InitialDelay = (int)item.InitialDelay.TotalMilliseconds;
            View.Tag = item.Data;
            if (item.Image != null) View.Image = item.Image;

            View.Show((int)item.DisplayTime.TotalMilliseconds);
        }

        #endregion
    }
}
