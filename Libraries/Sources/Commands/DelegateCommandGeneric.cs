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
using System.ComponentModel;

namespace Cube.Xui.Commands
{
    /* --------------------------------------------------------------------- */
    ///
    /// DelegateCommand(T)
    ///
    /// <summary>
    /// Represents an ICommand implementation that can be associated with
    /// INotifyPropertyChanged objects.
    /// </summary>
    ///
    /// <remarks>
    /// Observe メソッドによって関連付けられたオブジェクトの PropertyChanged
    /// イベント発生時に CanExecuteChanged イベントを発生させます。
    /// </remarks>
    ///
    /* --------------------------------------------------------------------- */
    public class DelegateCommand<T> : DelegateCommandBase
    {
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// DelegateCommand(T)
        ///
        /// <summary>
        /// Initializes a new instance of the BindableCommand class with
        /// the specified action.
        /// </summary>
        ///
        /// <param name="execute">Action to execute.</param>
        ///
        /* ----------------------------------------------------------------- */
        public DelegateCommand(Action<T> execute) : this(execute, e => true) { }

        /* ----------------------------------------------------------------- */
        ///
        /// DelegateCommand(T)
        ///
        /// <summary>
        /// Initializes a new instance of the BindableCommand class with
        /// the specified action.
        /// </summary>
        ///
        /// <param name="execute">Action to execute.</param>
        /// <param name="canExecute">
        /// Function to determine whether the command can be executed.
        /// </param>
        ///
        /* ----------------------------------------------------------------- */
        public DelegateCommand(Action<T> execute, Func<T, bool> canExecute)
        {
            _execute    = execute    ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute ?? throw new ArgumentNullException(nameof(canExecute));
        }

        #endregion

        #region Methods

        /* ----------------------------------------------------------------- */
        ///
        /// CanExecute
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
        public bool CanExecute(T parameter) => _canExecute(parameter);

        /* ----------------------------------------------------------------- */
        ///
        /// Execute
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
        public void Execute(T parameter) => _execute(parameter);

        /* ----------------------------------------------------------------- */
        ///
        /// Observe
        ///
        /// <summary>
        /// Observes the PropertyChanged event of the specified object.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public DelegateCommand<T> Observe(INotifyPropertyChanged src, params string[] names)
        {
            OnObserve(src, names);
            return this;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// OnCanExecute
        ///
        /// <summary>
        /// Determines whether the command can execute in its current state.
        /// </summary>
        ///
        /// <param name="parameter">Not used parameter.</param>
        ///
        /* ----------------------------------------------------------------- */
        protected override bool OnCanExecute(object parameter) => CanExecute((T)parameter);

        /* ----------------------------------------------------------------- */
        ///
        /// OnExecute
        ///
        /// <summary>
        /// Executes the command.
        /// </summary>
        ///
        /// <param name="parameter">Not used parameter.</param>
        ///
        /* ----------------------------------------------------------------- */
        protected override void OnExecute(object parameter) => Execute((T)parameter);

        #endregion

        #region Fields
        private readonly Action<T> _execute;
        private readonly Func<T, bool> _canExecute;
        #endregion
    }
}
