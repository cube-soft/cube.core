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

namespace Cube
{
    /* --------------------------------------------------------------------- */
    ///
    /// Getter
    ///
    /// <summary>
    /// Represents the delegation for getting value of type T.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public delegate T Getter<T>();

    /* --------------------------------------------------------------------- */
    ///
    /// Setter
    ///
    /// <summary>
    /// Represents the delegation for setting value of type T.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public delegate bool Setter<T>(T value);

    /* --------------------------------------------------------------------- */
    ///
    /// Accessor
    ///
    /// <summary>
    /// Provides functionality to get and set value of type T.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class Accessor<T>
    {
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// Accessor
        ///
        /// <summary>
        /// Initializes a new instance of the <c>Accessor</c> class.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public Accessor() : this(default(T)) { }

        /* ----------------------------------------------------------------- */
        ///
        /// Accessor
        ///
        /// <summary>
        /// Initializes a new instance of the <c>Accessor</c> class with
        /// the specified value.
        /// </summary>
        ///
        /// <param name="value">Initial value.</param>
        ///
        /* ----------------------------------------------------------------- */
        public Accessor(T value)
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
        /// Accessor
        ///
        /// <summary>
        /// Initializes a new instance of the <c>Accessor</c> class with
        /// the specified delegation.
        /// </summary>
        ///
        /// <param name="getter">Function to get value.</param>
        ///
        /// <remarks>
        /// 生成されたオブジェクトは読み込み専用となり、<c>Set</c>
        /// メソッド実行時には <c>InvalidOperationException</c> が送出
        /// されます。
        /// </remarks>
        ///
        /* ----------------------------------------------------------------- */
        public Accessor(Getter<T> getter) :
            this(getter, e => throw new InvalidOperationException(nameof(Set))) { }

        /* ----------------------------------------------------------------- */
        ///
        /// Accessor
        ///
        /// <summary>
        /// Initializes a new instance of the <c>Accessor</c> class with
        /// the specified delegations.
        /// </summary>
        ///
        /// <param name="getter">Function to get value.</param>
        /// <param name="setter">Function to set value.</param>
        ///
        /* ----------------------------------------------------------------- */
        public Accessor(Getter<T> getter, Setter<T> setter)
        {
            _getter = getter;
            _setter = setter;
        }

        #endregion

        #region Methods

        /* ----------------------------------------------------------------- */
        ///
        /// Get
        ///
        /// <summary>
        /// Get the value.
        /// </summary>
        ///
        /// <returns>Result of Getter(T).</returns>
        ///
        /* ----------------------------------------------------------------- */
        public T Get() => _getter();

        /* ----------------------------------------------------------------- */
        ///
        /// Get
        ///
        /// <summary>
        /// Set the value.
        /// </summary>
        ///
        /// <param name="value">Value to be set.</param>
        ///
        /// <returns>Result of Setter(T).</returns>
        ///
        /* ----------------------------------------------------------------- */
        public bool Set(T value) => _setter(value);

        #endregion

        #region Fields
        private readonly Getter<T> _getter;
        private readonly Setter<T> _setter;
        #endregion
    }
}
