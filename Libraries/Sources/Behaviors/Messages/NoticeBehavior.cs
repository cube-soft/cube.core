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
using System.Threading;
using System.Threading.Tasks;
using Cube.Forms.Controls;
using Cube.Mixin.Tasks;

namespace Cube.Forms.Behaviors
{
    /* --------------------------------------------------------------------- */
    ///
    /// NoticeBehavior
    ///
    /// <summary>
    /// Provides functionality to show a notice window with the specified
    /// information.
    /// </summary>
    ///
    /// <remarks>
    /// When a view object is shared, exclusive control is the responsibility
    /// of the user.
    /// </remarks>
    ///
    /* --------------------------------------------------------------------- */
    public class NoticeBehavior : MessageBehaviorBase<NoticeMessage>
    {
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// NoticeBehavior
        ///
        /// <summary>
        /// Initializes a new instance of the NoticeBehavior class with
        /// the specified arguments.
        /// </summary>
        ///
        /// <param name="vm">Presentable object.</param>
        ///
        /* ----------------------------------------------------------------- */
        public NoticeBehavior(IPresentable vm) : this(default, vm) { }

        /* ----------------------------------------------------------------- */
        ///
        /// NoticeBehavior
        ///
        /// <summary>
        /// Initializes a new instance of the NoticeBehavior class with
        /// the specified arguments.
        /// </summary>
        ///
        /// <param name="view">Shared view object.</param>
        /// <param name="vm">Presentable object.</param>
        ///
        /* ----------------------------------------------------------------- */
        public NoticeBehavior(NoticeWindow view, IPresentable vm) : base(vm) { View = view; }

        #endregion

        #region Properties

        /* ----------------------------------------------------------------- */
        ///
        /// View
        ///
        /// <summary>
        /// Gets the view object that is shared in the instance.
        /// This property may be null.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected NoticeWindow View { get; }

        #endregion

        #region Implementations

        /* ----------------------------------------------------------------- */
        ///
        /// Invoke
        ///
        /// <summary>
        /// Shows a new window with the specified notice information.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected override void Invoke(NoticeMessage src) => InvokeAsync(src).Forget();

        /* ----------------------------------------------------------------- */
        ///
        /// ShowAsync
        ///
        /// <summary>
        /// Shows a new window with the specified notice information.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private async Task InvokeAsync(NoticeMessage src)
        {
            if (src.InitialDelay > TimeSpan.Zero) await Task.Delay(src.InitialDelay);
            await ShowAsync(src);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// ShowAsync
        ///
        /// <summary>
        /// Shows a new window with the specified notice information.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private async Task ShowAsync(NoticeMessage src)
        {
            var view   = View ?? new NoticeWindow();
            var shared = View != null;
            var cts    = new CancellationTokenSource();

            void handler(object s, ValueEventArgs<NoticeResult> e)
            {
                view.Selected -= handler;
                cts.Cancel();
                if (shared) view.Hide();
                else view.Close();
                src.Callback?.Invoke(src, e.Value);
            }

            view.Selected += handler;
            view.SetTopMost(false);
            view.Set(src.Text, src.Title);
            view.Set(src.Style);
            view.Set(src.Location);
            view.Show();

            if (src.DisplayTime == TimeSpan.Zero) return; // Zero means infinity.

            try
            {
                await Task.Delay(src.DisplayTime, cts.Token);
                view.Selected -= handler;
                if (shared) view.Hide();
                else view.Close();
                src.Callback?.Invoke(src, NoticeResult.Timeout);
            }
            catch (TaskCanceledException) { /* ignore user cancel */ }
        }

        #endregion
    }
}
