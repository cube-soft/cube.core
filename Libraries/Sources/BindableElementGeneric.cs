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
namespace Cube.Xui
{
    /* --------------------------------------------------------------------- */
    ///
    /// BindableElement(T)
    ///
    /// <summary>
    /// Represents a bindable element that has text, command, and value.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class BindableElement<T> : BindableElement, IElement<T>
    {
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// BindableElement(T)
        ///
        /// <summary>
        /// Initializes a new instance of the <c>BindableElement</c>
        /// class with the specified arguments.
        /// </summary>
        ///
        /// <param name="gettext">Function to get text.</param>
        /// <param name="getvalue">Function to get value.</param>
        /// <param name="dispatcher">Dispatcher object.</param>
        ///
        /* ----------------------------------------------------------------- */
        public BindableElement(Getter<string> gettext, Getter<T> getvalue, IDispatcher dispatcher) :
            this(gettext, new Accessor<T>(getvalue), dispatcher) { }

        /* ----------------------------------------------------------------- */
        ///
        /// BindableElement(T)
        ///
        /// <summary>
        /// Initializes a new instance of the <c>BindableElement</c>
        /// class with the specified arguments.
        /// </summary>
        ///
        /// <param name="gettext">Function to get text.</param>
        /// <param name="getvalue">Function to get value.</param>
        /// <param name="setvalue">Function to set value.</param>
        /// <param name="dispatcher">Dispatcher object.</param>
        ///
        /* ----------------------------------------------------------------- */
        public BindableElement(Getter<string> gettext, Getter<T> getvalue, Setter<T> setvalue, IDispatcher dispatcher) :
            this(gettext, new Accessor<T>(getvalue, setvalue), dispatcher) { }

        /* ----------------------------------------------------------------- */
        ///
        /// BindableElement(T)
        ///
        /// <summary>
        /// Initializes a new instance of the <c>BindableElement</c>
        /// class with the specified arguments.
        /// </summary>
        ///
        /// <param name="gettext">Function to get text.</param>
        /// <param name="accessor">Function to get and set value.</param>
        /// <param name="dispatcher">Dispatcher object.</param>
        ///
        /* ----------------------------------------------------------------- */
        public BindableElement(Getter<string> gettext, Accessor<T> accessor, IDispatcher dispatcher) :
            base(gettext, dispatcher) { _accessor = accessor; }

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
            get => _accessor.Get();
            set { if (_accessor.Set(value)) Refresh(nameof(Value)); }
        }

        #endregion

        #region Methods

        /* ----------------------------------------------------------------- */
        ///
        /// React
        ///
        /// <summary>
        /// Occurs when any states are changed.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected override void React()
        {
            base.React();
            Refresh(nameof(Value));
        }

        #endregion

        #region Fields
        private readonly Accessor<T> _accessor;
        #endregion
    }
}
