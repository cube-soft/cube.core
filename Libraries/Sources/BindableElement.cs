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
using System.Windows.Input;

namespace Cube.Xui
{
    /* --------------------------------------------------------------------- */
    ///
    /// BindableElement
    ///
    /// <summary>
    /// Represents a bindable element that has two kinds of text (summary
    /// and description) and a command.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class BindableElement : ObservableProperty, IDisposable
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
        /// <param name="summary">Function to get summary.</param>
        /// <param name="description">Function to get description.</param>
        ///
        /* ----------------------------------------------------------------- */
        public BindableElement(Func<string> summary, Func<string> description)
        {
            _dispose     = new OnceAction<bool>(Dispose);
            _summary     = summary;
            _description = description;
            _registry    = ResourceCulture.Subscribe(() =>
            {
                RaisePropertyChanged(nameof(Summary));
                RaisePropertyChanged(nameof(Description));
            });
        }

        #endregion

        #region Properties

        /* ----------------------------------------------------------------- */
        ///
        /// Summary
        ///
        /// <summary>
        /// Gets the summary text.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string Summary => _summary();

        /* ----------------------------------------------------------------- */
        ///
        /// Description
        ///
        /// <summary>
        /// Gets the description text.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string Description => _description();

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
        /// ~BindableElement
        ///
        /// <summary>
        /// Finalizes the <c>BindableElement</c>.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        ~BindableElement() { _dispose.Invoke(false); }

        /* ----------------------------------------------------------------- */
        ///
        /// Dispose
        ///
        /// <summary>
        /// Releases all resources used by the <c>BindableElement</c>.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public void Dispose()
        {
            _dispose.Invoke(true);
            GC.SuppressFinalize(this);
        }

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
        protected virtual void Dispose(bool disposing)
        {
            if (disposing) _registry.Dispose();
        }

        #endregion

        #region Fields
        private readonly OnceAction<bool> _dispose;
        private readonly Func<string> _summary;
        private readonly Func<string> _description;
        private readonly IDisposable _registry;
        private ICommand _command;
        #endregion
    }

    /* --------------------------------------------------------------------- */
    ///
    /// BindableElement(T)
    ///
    /// <summary>
    /// Represents a bindable element that has two kinds of text (summary
    /// and description), a command, and a value.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class BindableElement<T> : BindableElement
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
        /// <param name="summary">Function to get summary.</param>
        /// <param name="description">Function to get description.</param>
        ///
        /* ----------------------------------------------------------------- */
        public BindableElement(Func<string> summary, Func<string> description) :
            this(default(T), summary, description) { }

        /* ----------------------------------------------------------------- */
        ///
        /// BindableElement
        ///
        /// <summary>
        /// Initializes a new instance of the <c>BindableElement</c>
        /// class with the specified arguments.
        /// </summary>
        ///
        /// <param name="value">Initial value.</param>
        /// <param name="summary">Function to get summary.</param>
        /// <param name="description">Function to get description.</param>
        ///
        /* ----------------------------------------------------------------- */
        public BindableElement(T value, Func<string> summary, Func<string> description) :
            base(summary, description)
        {
            var field = value;
            _getter = () => field;
            _setter = e =>
            {
                if (EqualityComparer<T>.Default.Equals(field, e)) return false;
                field = e;
                return true;
            };
        }

        /* ----------------------------------------------------------------- */
        ///
        /// BindableElement
        ///
        /// <summary>
        /// Initializes a new instance of the <c>BindableElement</c>
        /// class with the specified arguments.
        /// </summary>
        ///
        /// <param name="getter">Function to get the value.</param>
        /// <param name="setter">Function to set the value.</param>
        /// <param name="summary">Function to get summary.</param>
        /// <param name="description">Function to get description.</param>
        ///
        /* ----------------------------------------------------------------- */
        public BindableElement(Func<T> getter, Func<T, bool>setter,
            Func<string> summary, Func<string> description) :
            base(summary, description)
        {
            _getter = getter;
            _setter = setter;
        }

        #endregion

        #region Properties

        /* ----------------------------------------------------------------- */
        ///
        /// Value
        ///
        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public T Value
        {
            get => _getter();
            set { if (_setter(value)) RaisePropertyChanged(nameof(Value)); }
        }

        #endregion

        #region Fields
        private readonly Func<T> _getter;
        private readonly Func<T, bool> _setter;
        #endregion
    }
}
