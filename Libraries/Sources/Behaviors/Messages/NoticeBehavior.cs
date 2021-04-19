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
    /* --------------------------------------------------------------------- */
    public class NoticeBehavior : MessageBehavior<NoticeMessage>
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
        public NoticeBehavior(IPresentable vm) : base(vm) { }

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
        protected override void Invoke(NoticeMessage src) => ShowAsync(src).Forget();

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
            var cts  = new CancellationTokenSource();
            var view = new NoticeWindow();

            void handler(object s, ValueEventArgs<NoticeComponent> e)
            {
                cts.Cancel();
                view.Selected -= handler;
                view.Close();
                src.Callback?.Invoke(e.Value, src.Value);
            }

            view.Selected += handler;
            view.SetTopMost(false);
            view.Set(src.Text, src.Title);
            view.Set(src.Style);
            view.Set(src.Location);

            if (src.InitialDelay > TimeSpan.Zero) await Task.Delay(src.InitialDelay);
            view.Show();
            if (src.DisplayTime == TimeSpan.Zero) return; // Zero means infinity.

            try
            {
                await Task.Delay(src.DisplayTime, cts.Token);
                view.Selected -= handler;
                view.Close();
            }
            catch (TaskCanceledException) { /* ignore user cancel */ }
        }

        #endregion
    }
}
