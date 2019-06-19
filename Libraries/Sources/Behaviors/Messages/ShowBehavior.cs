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
using System.Windows.Forms;

namespace Cube.Forms.Behaviors
{
    #region ShowBehavior

    /* --------------------------------------------------------------------- */
    ///
    /// ShowBehavior
    ///
    /// <summary>
    /// Provides functionality to show a window.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class ShowBehavior<TView, TViewModel> : MessageBehavior<TViewModel>
        where TView : Form, new()
        where TViewModel : IPresentable
    {
        /* ----------------------------------------------------------------- */
        ///
        /// ShowBehavior
        ///
        /// <summary>
        /// Initializes a new instance of the ShowBehavior class with the
        /// specified arguments.
        /// </summary>
        ///
        /// <param name="src">Presentable object.</param>
        ///
        /* ----------------------------------------------------------------- */
        public ShowBehavior(IPresentable src) : base(src) { }

        /* ----------------------------------------------------------------- */
        ///
        /// Invoke
        ///
        /// <summary>
        /// Shows a new window with the specified ViewModel.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected override void Invoke(TViewModel e)
        {
            var view = new TView();
            if (view is IBinder binder) binder.Bind(e);
            view.Show();
        }
    }

    #endregion

    #region ShowDialogBehavior

    /* --------------------------------------------------------------------- */
    ///
    /// ShowDialogBehavior
    ///
    /// <summary>
    /// Provides functionality to show a window as a modal dialog.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class ShowDialogBehavior<TView, TViewModel> : MessageBehavior<TViewModel>
        where TView : Form, new()
        where TViewModel : IPresentable
    {
        /* ----------------------------------------------------------------- */
        ///
        /// ShowDialogBehavior
        ///
        /// <summary>
        /// Initializes a new instance of the ShowDialogBehavior class
        /// with the specified arguments.
        /// </summary>
        ///
        /// <param name="src">Presentable object.</param>
        ///
        /* ----------------------------------------------------------------- */
        public ShowDialogBehavior(IPresentable src) : base(src) { }

        /* ----------------------------------------------------------------- */
        ///
        /// Invoke
        ///
        /// <summary>
        /// Shows a new window with the specified ViewModel.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected override void Invoke(TViewModel e)
        {
            using(var view = new TView())
            {
                if (view is IBinder binder) binder.Bind(e);
                _ = view.ShowDialog();
            }
        }
    }

    #endregion
}
