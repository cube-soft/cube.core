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
    /// Bindable(T)
    ///
    /// <summary>
    /// Provides functionality to make the value as bindable.
    /// </summary>
    ///
    /// <remarks>
    /// Value プロパティを通じて実際の値にアクセスします。
    /// </remarks>
    ///
    /* --------------------------------------------------------------------- */
    public class Bindable<T> : ObservableBase
    {
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// Bindable
        ///
        /// <summary>
        /// Initializes a new instance of the <c>Bindable</c> class.
        /// </summary>
        ///
        /// <param name="dispatcher">Dispatcher object.</param>
        ///
        /* ----------------------------------------------------------------- */
        public Bindable(IDispatcher dispatcher) : this(default(T), dispatcher) { }

        /* ----------------------------------------------------------------- */
        ///
        /// Bindable
        ///
        /// <summary>
        /// Initializes a new instance of the <c>Bindable</c> class
        /// with the specified arguments.
        /// </summary>
        ///
        /// <param name="value">Initial value.</param>
        /// <param name="dispatcher">Dispatcher object.</param>
        ///
        /* ----------------------------------------------------------------- */
        public Bindable(T value, IDispatcher dispatcher) :
            this(new Accessor<T>(value), dispatcher) { }

        /* ----------------------------------------------------------------- */
        ///
        /// Bindable
        ///
        /// <summary>
        /// Initializes a new instance of the <c>Bindable</c> class
        /// with the specified arguments.
        /// </summary>
        ///
        /// <param name="getter">Function to get the value.</param>
        /// <param name="dispatcher">Dispatcher object.</param>
        ///
        /* ----------------------------------------------------------------- */
        public Bindable(Getter<T> getter, IDispatcher dispatcher) :
            this(new Accessor<T>(getter), dispatcher) { }

        /* ----------------------------------------------------------------- */
        ///
        /// Bindable
        ///
        /// <summary>
        /// Initializes a new instance of the <c>Bindable</c> class
        /// with the specified arguments.
        /// </summary>
        ///
        /// <param name="getter">Function to get the value.</param>
        /// <param name="setter">Function to set the value.</param>
        /// <param name="dispatcher">Dispatcher object.</param>
        ///
        /* ----------------------------------------------------------------- */
        public Bindable(Getter<T> getter, Setter<T> setter, IDispatcher dispatcher) :
            this(new Accessor<T>(getter, setter), dispatcher) { }

        /* ----------------------------------------------------------------- */
        ///
        /// Bindable
        ///
        /// <summary>
        /// Initializes a new instance of the <c>Bindable</c> class
        /// with the specified arguments.
        /// </summary>
        ///
        /// <param name="accessor">Function to get and set value.</param>
        /// <param name="dispatcher">Dispatcher object.</param>
        ///
        /* ----------------------------------------------------------------- */
        public Bindable(Accessor<T> accessor, IDispatcher dispatcher) : base(dispatcher)
        {
            _accessor = accessor;
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
            get => _accessor.Get();
            set { if (_accessor.Set(value)) Refresh(nameof(Value)); }
        }

        #endregion

        #region Methods

        /* ----------------------------------------------------------------- */
        ///
        /// RaiseValueChanged
        ///
        /// <summary>
        /// Raises a PropertyChanged event against the Value property.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public void RaiseValueChanged() => Refresh(nameof(Value));

        #endregion

        #region Fields
        private readonly Accessor<T> _accessor;
        #endregion
    }
}
