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
    /// DelegateCommand
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
    public class DelegateCommand : DelegateCommandBase
    {
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// DelegateCommand
        ///
        /// <summary>
        /// Initializes a new instance of the BindableCommand class with
        /// the specified action.
        /// </summary>
        ///
        /// <param name="execute">Action to execute.</param>
        ///
        /* ----------------------------------------------------------------- */
        public DelegateCommand(Action execute) : this(execute, () => true) { }

        /* ----------------------------------------------------------------- */
        ///
        /// DelegateCommand
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
        public DelegateCommand(Action execute, Func<bool> canExecute)
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
        /* ----------------------------------------------------------------- */
        public bool CanExecute() => _canExecute();

        /* ----------------------------------------------------------------- */
        ///
        /// Execute
        ///
        /// <summary>
        /// Executes the command.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public void Execute() => _execute();

        /* ----------------------------------------------------------------- */
        ///
        /// Observe
        ///
        /// <summary>
        /// Observes the PropertyChanged event of the specified object.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public DelegateCommand Observe(INotifyPropertyChanged src, params string[] names)
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
        protected override bool OnCanExecute(object parameter) => CanExecute();

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
        protected override void OnExecute(object parameter) => Execute();

        #endregion

        #region Fields
        private readonly Action _execute;
        private readonly Func<bool> _canExecute;
        #endregion
    }
}
