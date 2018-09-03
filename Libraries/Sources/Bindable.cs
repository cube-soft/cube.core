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
    /// PropertyChanged イベントは、コンストラクタで指定された同期
    /// コンテキストを用いて発生します。
    /// </remarks>
    ///
    /* --------------------------------------------------------------------- */
    public class Bindable<T> : ObservableProperty
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
        public Bindable(T value) : this(value, SynchronizationContext.Current) { }

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
        /// <param name="context">Synchronization context.</param>
        ///
        /* ----------------------------------------------------------------- */
        public Bindable(T value, SynchronizationContext context)
        {
            Context = context;

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
        /// Bindable
        ///
        /// <summary>
        /// Initializes a new instance of the <c>Bindable</c> class
        /// with the specified arguments.
        /// </summary>
        ///
        /// <param name="getter">Function to get the value.</param>
        /// <param name="setter">Function to set the value.</param>
        /// <param name="context">Synchronization context.</param>
        ///
        /* ----------------------------------------------------------------- */
        public Bindable(Func<T> getter, Func<T, bool> setter, SynchronizationContext context)
        {
            Context  = context;
            _getter  = getter;
            _setter  = setter;
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
