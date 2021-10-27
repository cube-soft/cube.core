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
using System.Globalization;

namespace Cube.Forms
{
    /* --------------------------------------------------------------------- */
    ///
    /// OnlyExpandableConverter
    ///
    /// <summary>
    /// Provides functionality to expand nested properties in the property
    /// editor.
    /// </summary>
    ///
    /// <remarks>
    /// This is an ExpandableObjectConverter for nested properties to prevent
    /// editing by string. When editing, it expands the property and forces
    /// the user to edit the individual property.
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
        /// Gets a value indicating whether the converter can convert the
        /// object to the specified type.
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
        /// Converts the specified object to the specified type with the
        /// specified arguments.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture,
            object value, Type destinationType)
        {
            if (destinationType == typeof(string)) return string.Empty;
            return base.ConvertTo(context, culture, value, destinationType);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// CanConvertFrom
        ///
        /// <summary>
        /// Gets a value indicating whether the converter can convert objects
        /// of a certain type to the converter's type using the specified
        /// context.
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
