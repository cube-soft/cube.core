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
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;

namespace Cube.Xui.Commands
{
    /* --------------------------------------------------------------------- */
    ///
    /// DelegateCommandBase
    ///
    /// <summary>
    /// Provides an implementation of the ICommand.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public abstract class DelegateCommandBase : DisposableBase, ICommand
    {
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// DelegateCommandBase
        ///
        /// <summary>
        /// Initializes a new instance of the BindableCommandBase.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected DelegateCommandBase() { }

        #endregion

        #region Events

        /* ----------------------------------------------------------------- */
        ///
        /// OnCanExecuteChanged
        ///
        /// <summary>
        /// Occurs when changes occur that affect whether or not the
        /// command should execute.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public event EventHandler CanExecuteChanged;

        /* ----------------------------------------------------------------- */
        ///
        /// OnCanExecuteChanged
        ///
        /// <summary>
        /// Raises the CanExecuteChanged event.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected virtual void OnCanExecuteChanged(EventArgs e) =>
            CanExecuteChanged?.Invoke(this, e);

        #endregion

        #region Methods

        /* ----------------------------------------------------------------- */
        ///
        /// Refresh
        ///
        /// <summary>
        /// Raises the CanExecuteChanged event.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public void Refresh() => OnCanExecuteChanged(EventArgs.Empty);

        /* ----------------------------------------------------------------- */
        ///
        /// OnCanExecute
        ///
        /// <summary>
        /// Determines whether the command can execute in its current state.
        /// </summary>
        ///
        /// <param name="parameter">
        /// Data used by the command. If the command does not require data
        /// to be passed, this object can be set to null.
        /// </param>
        ///
        /* ----------------------------------------------------------------- */
        protected abstract bool OnCanExecute(object parameter);

        /* ----------------------------------------------------------------- */
        ///
        /// OnExecute
        ///
        /// <summary>
        /// Executes the command.
        /// </summary>
        ///
        /// <param name="parameter">
        /// Data used by the command. If the command does not require data
        /// to be passed, this object can be set to null.
        /// </param>
        ///
        /* ----------------------------------------------------------------- */
        protected abstract void OnExecute(object parameter);

        /* ----------------------------------------------------------------- */
        ///
        /// OnObserve
        ///
        /// <summary>
        /// Observes the PropertyChanged event of the specified object.
        /// </summary>
        ///
        /// <param name="src">Observed object.</param>
        /// <param name="names">Target property names.</param>
        ///
        /* ----------------------------------------------------------------- */
        protected void OnObserve(INotifyPropertyChanged src, params string[] names)
        {
            var set = new HashSet<string>(names);
            void handler(object s, PropertyChangedEventArgs e)
            {
                if (set.Count <= 0 || set.Contains(e.PropertyName)) Refresh();
            }

            src.PropertyChanged += handler;
            _observer.Add(Disposable.Create(() => src.PropertyChanged -= handler));
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Dispose
        ///
        /// <summary>
        /// Releases the unmanaged resources used by the object and
        /// optionally releases the managed resources.
        /// </summary>
        ///
        /// <param name="disposing">
        /// true to release both managed and unmanaged resources;
        /// false to release only unmanaged resources.
        /// </param>
        ///
        /* ----------------------------------------------------------------- */
        protected override void Dispose(bool disposing)
        {
            if (!disposing) return;
            foreach (var obj in _observer) obj.Dispose();
            _observer.Clear();
        }

        #region ICommand

        /* ----------------------------------------------------------------- */
        ///
        /// CanExecute
        ///
        /// <summary>
        /// Defines the method that determines whether the command can
        /// execute in its current state.
        /// </summary>
        ///
        /// <param name="parameter">
        /// Data used by the command. If the command does not require data
        /// to be passed, this object can be set to null.
        /// </param>
        ///
        /* ----------------------------------------------------------------- */
        bool ICommand.CanExecute(object parameter) => OnCanExecute(parameter);

        /* ----------------------------------------------------------------- */
        ///
        /// Execute
        ///
        /// <summary>
        /// Defines the method to be called when the command is invoked.
        /// </summary>
        ///
        /// <param name="parameter">
        /// Data used by the command. If the command does not require data
        /// to be passed, this object can be set to null.
        /// </param>
        ///
        /* ----------------------------------------------------------------- */
        void ICommand.Execute(object parameter) => OnExecute(parameter);

        #endregion

        #endregion

        #region Fields
        private readonly IList<IDisposable> _observer = new List<IDisposable>();
        #endregion
    }
}
