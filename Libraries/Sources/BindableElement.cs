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
using System.Windows.Input;

namespace Cube.Xui
{
    /* --------------------------------------------------------------------- */
    ///
    /// BindableElement
    ///
    /// <summary>
    /// Represents a bindable element that has text and command.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class BindableElement : DisposableObservable
    {
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// BindableElement
        ///
        /// <summary>
        /// Initializes a new instance of the <c>BindableElement</c>
        /// class with the specified arguments.
        /// </summary>
        ///
        /// <param name="gettext">Function to get text.</param>
        /// <param name="dispatcher">Dispatcher object.</param>
        ///
        /* ----------------------------------------------------------------- */
        public BindableElement(Getter<string> gettext, IDispatcher dispatcher) : base(dispatcher)
        {
            _getter  = gettext;
            _locale  = Locale.Subscribe(e => OnLanguageChanged(e));
        }

        #endregion

        #region Properties

        /* ----------------------------------------------------------------- */
        ///
        /// Text
        ///
        /// <summary>
        /// Gets the text.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string Text => _getter();

        /* ----------------------------------------------------------------- */
        ///
        /// Command
        ///
        /// <summary>
        /// Gets or sets the command object.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public ICommand Command
        {
            get => _command;
            set => SetProperty(ref _command, value);
        }

        #endregion

        #region Methods

        /* ----------------------------------------------------------------- */
        ///
        /// Dispose
        ///
        /// <summary>
        /// Releases the unmanaged resources used by <c>BindableElement</c>
        /// and optionally releases the managed resources.
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
            if (disposing) _locale.Dispose();
        }

        /* ----------------------------------------------------------------- */
        ///
        /// OnLanguageChanged
        ///
        /// <summary>
        /// Occurs when the current language is changed.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected virtual void OnLanguageChanged(Language e) => Refresh(nameof(Text));

        #endregion

        #region Fields
        private readonly Getter<string> _getter;
        private readonly IDisposable _locale;
        private ICommand _command;
        #endregion
    }
}
