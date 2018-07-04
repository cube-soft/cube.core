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

namespace Cube.Forms
{
    /* --------------------------------------------------------------------- */
    ///
    /// OnlyExpandableConverter
    ///
    /// <summary>
    /// プロパティエディタにおいて、ネストされたプロパティを展開可能に
    /// するクラスです。
    /// </summary>
    ///
    /// <remarks>
    /// ネストされたプロパティに対して、文字列では編集できないようにする
    /// ための ExpandableObjectConverter です。編集を行う際にはプロパティを
    /// 展開し、個々のプロパティを編集するように強制します。
    /// </remarks>
    ///
    /* --------------------------------------------------------------------- */
    public class OnlyExpandableConverter : ExpandableObjectConverter
    {
        #region Methods

        /* ----------------------------------------------------------------- */
        ///
        /// CanConvertTo
        ///
        /// <summary>
        /// コンバーターがオブジェクトを指定した型に変換できるかどうかを
        /// 示す値を返します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if (destinationType == typeof(string)) return false;
            return base.CanConvertTo(context, destinationType);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// ConvertTo
        ///
        /// <summary>
        /// 指定したコンテキストとカルチャ情報を使用して、
        /// 指定した値オブジェクトを、指定した型に変換します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public override object ConvertTo(ITypeDescriptorContext context,
            System.Globalization.CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string)) return string.Empty;
            return base.ConvertTo(context, culture, value, destinationType);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// CanConvertFrom
        ///
        /// <summary>
        /// 指定したコンテキストを使用して、コンバーターが特定の型の
        /// オブジェクトをコンバーターの型に変換できるかどうかを示す値を
        /// 返します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(string)) return false;
            return base.CanConvertFrom(context, sourceType);
        }

        #endregion
    }
}
