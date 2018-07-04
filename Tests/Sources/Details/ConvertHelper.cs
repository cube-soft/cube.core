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
using System.Globalization;
using System.Windows.Data;

namespace Cube.Xui.Tests
{
    /* --------------------------------------------------------------------- */
    ///
    /// ConvertHelper
    ///
    /// <summary>
    /// 各種 Converter クラスをテストする際の補助クラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    class ConvertHelper
    {
        #region Methods

        /* ----------------------------------------------------------------- */
        ///
        /// Convert
        ///
        /// <summary>
        /// IValueConverter.Convert を実行します。
        /// </summary>
        ///
        /// <param name="src">Convert を実行するオブジェクト</param>
        /// <param name="value">変換元オブジェクト</param>
        ///
        /// <returns>変換結果</returns>
        ///
        /* ----------------------------------------------------------------- */
        public T Convert<T>(IValueConverter src, object value) =>
            (T)src.Convert(value, typeof(T), null, CultureInfo.CurrentCulture);

        /* ----------------------------------------------------------------- */
        ///
        /// Execute
        ///
        /// <summary>
        /// IValueConverter.ConvertBack を実行します。
        /// </summary>
        ///
        /// <param name="src">ConvertBack を実行するオブジェクト</param>
        /// <param name="value">変換元オブジェクト</param>
        ///
        /// <returns>変換結果</returns>
        ///
        /* ----------------------------------------------------------------- */
        public T ConvertBack<T>(IValueConverter src, object value) =>
            (T)src.ConvertBack(value, typeof(T), null, CultureInfo.CurrentCulture);

        #endregion
    }
}
