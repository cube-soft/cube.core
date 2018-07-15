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
using System.Globalization;
using System.Windows;

namespace Cube.Xui.Converters
{
    /* --------------------------------------------------------------------- */
    ///
    /// Inverse
    ///
    /// <summary>
    /// Provides functionality to inverse a boolean value.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class Inverse : DuplexConverter
    {
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// Inverse
        ///
        /// <summary>
        /// Initializes a new instance of the Inverse class.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public Inverse() : base(e => !(bool)e, e => !(bool)e) { }

        #endregion
    }

    /* --------------------------------------------------------------------- */
    ///
    /// BooleanToValue(T)
    ///
    /// <summary>
    /// Provides functionality to convert a boolean to the value of T.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class BooleanToValue<T> : SimplexConverter
    {
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// BooleanToValue
        ///
        /// <summary>
        /// Initializes a new instance of the BooleanToValue class
        /// with the specified parameters.
        /// </summary>
        ///
        /// <param name="positive">Value for ture.</param>
        /// <param name="negative">Value for false.</param>
        ///
        /* ----------------------------------------------------------------- */
        public BooleanToValue(T positive, T negative) :
            this(positive, negative, (e, _, __, ___) => (bool)e) { }

        /* ----------------------------------------------------------------- */
        ///
        /// BooleanToGeneric
        ///
        /// <summary>
        /// Initializes a new instance of the BooleanToValue class
        /// with the specified parameters.
        /// </summary>
        ///
        /// <param name="positive">Value for ture.</param>
        /// <param name="negative">Value for false.</param>
        /// <param name="func">
        /// Function object that determines whether the source is true.
        /// </param>
        ///
        /* ----------------------------------------------------------------- */
        public BooleanToValue(T positive, T negative, Func<object, bool> func) :
            this(positive, negative, (e, _, __, ___) => func(e)) { }

        /* ----------------------------------------------------------------- */
        ///
        /// BooleanToGeneric
        ///
        /// <summary>
        /// Initializes a new instance of the BooleanToValue class
        /// with the specified parameters.
        /// </summary>
        ///
        /// <param name="positive">Value for ture.</param>
        /// <param name="negative">Value for false.</param>
        /// <param name="func">
        /// Function object that determines whether the source is true.
        /// </param>
        ///
        /* ----------------------------------------------------------------- */
        public BooleanToValue(T positive, T negative,
            Func<object, object, bool> func) :
            this(positive, negative, (e, _, p, __) => func(e, p)) { }

        /* ----------------------------------------------------------------- */
        ///
        /// BooleanToGeneric
        ///
        /// <summary>
        /// Initializes a new instance of the BooleanToValue class
        /// with the specified parameters.
        /// </summary>
        ///
        /// <param name="positive">Value for ture.</param>
        /// <param name="negative">Value for false.</param>
        /// <param name="func">
        /// Function object that determines whether the source is true.
        /// </param>
        ///
        /* ----------------------------------------------------------------- */
        public BooleanToValue(T positive, T negative,
            Func<object, Type, object, CultureInfo, bool> func) :
            base ((e, t, p, c) => func(e, t, p, c) ? positive : negative) { }

        #endregion
    }

    /* --------------------------------------------------------------------- */
    ///
    /// BooleanToInteger
    ///
    /// <summary>
    /// Provides functionality to convert a boolean to the integer value.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class BooleanToInteger : BooleanToValue<int>
    {
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// BooleanToInteger
        ///
        /// <summary>
        /// Initializes a new instance of the BooleanToInteger class.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public BooleanToInteger() : this(1, 0) { }

        /* ----------------------------------------------------------------- */
        ///
        /// BooleanToInteger
        ///
        /// <summary>
        /// Initializes a new instance of the BooleanToInteger class
        /// with the specified parameters.
        /// </summary>
        ///
        /// <param name="positive">true に対応する整数値</param>
        /// <param name="negative">false に対応する整数値</param>
        ///
        /* ----------------------------------------------------------------- */
        public BooleanToInteger(int positive, int negative) :
            base(positive, negative) { }

        /* ----------------------------------------------------------------- */
        ///
        /// BooleanToInteger
        ///
        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        ///
        /// <param name="positive">true に対応する整数値</param>
        /// <param name="negative">false に対応する整数値</param>
        /// <param name="func">真偽判定用オブジェクト</param>
        ///
        /* ----------------------------------------------------------------- */
        public BooleanToInteger(int negative, int positive, Func<object, bool> func) :
            base(positive, negative, func) { }

        #endregion
    }

    /* --------------------------------------------------------------------- */
    ///
    /// BooleanToVisibility
    ///
    /// <summary>
    /// 真偽値を Visibility に変換させるためのクラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class BooleanToVisibility : BooleanToValue<Visibility>
    {
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// BooleanToVisibility
        ///
        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public BooleanToVisibility() :
            base(Visibility.Visible, Visibility.Collapsed) { }

        /* ----------------------------------------------------------------- */
        ///
        /// BooleanToVisibility
        ///
        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        ///
        /// <param name="func">真偽判定用オブジェクト</param>
        ///
        /* ----------------------------------------------------------------- */
        public BooleanToVisibility(Func<object, bool> func) :
            this(Visibility.Visible, Visibility.Collapsed, func) { }

        /* ----------------------------------------------------------------- */
        ///
        /// BooleanToVisibility
        ///
        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        ///
        /// <param name="positive">true に対応する値</param>
        /// <param name="negative">false に対応する値</param>
        /// <param name="func">真偽判定用オブジェクト</param>
        ///
        /* ----------------------------------------------------------------- */
        public BooleanToVisibility(Visibility positive, Visibility negative, Func<object, bool> func) :
            base(positive, negative, func) { }

        #endregion
    }
}
