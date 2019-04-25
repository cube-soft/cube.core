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
using System.Threading;

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
        /* ----------------------------------------------------------------- */
        public Bindable() : this(default(T)) { }

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
        ///
        /* ----------------------------------------------------------------- */
        public Bindable(T value) : this(new Accessor<T>(value)) { }

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
        ///
        /* ----------------------------------------------------------------- */
        public Bindable(Getter<T> getter) : this(new Accessor<T>(getter)) { }

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
        ///
        /* ----------------------------------------------------------------- */
        public Bindable(Getter<T> getter, Setter<T> setter) :
            this(new Accessor<T>(getter, setter)) { }

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
        ///
        /* ----------------------------------------------------------------- */
        public Bindable(Accessor<T> accessor)
        {
            Context   = SynchronizationContext.Current;
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
            set { if (_accessor.Set(value)) RaisePropertyChanged(nameof(Value)); }
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
        public void RaiseValueChanged() => RaisePropertyChanged(nameof(Value));

        #endregion

        #region Fields
        private readonly Accessor<T> _accessor;
        #endregion
    }
}
