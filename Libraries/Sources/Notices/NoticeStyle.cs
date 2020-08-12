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
using System.Drawing;
using System.ComponentModel;

namespace Cube.Forms
{
    /* --------------------------------------------------------------------- */
    ///
    /// NoticeStyle
    ///
    /// <summary>
    /// Repesents the styles of the notice window.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [TypeConverter(typeof(OnlyExpandableConverter))]
    public class NoticeStyle
    {
        #region Properties

        /* --------------------------------------------------------------------- */
        ///
        /// BackColor
        ///
        /// <summary>
        /// Gets or sets the background color.
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        [Browsable(true)]
        public Color BackColor { get; set; }

        /* --------------------------------------------------------------------- */
        ///
        /// Image
        ///
        /// <summary>
        /// Gets or sets the image.
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        [Browsable(true)]
        public Image Image { get; set; }

        /* --------------------------------------------------------------------- */
        ///
        /// ImageColor
        ///
        /// <summary>
        /// Gets or sets the background color of the image component.
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        [Browsable(true)]
        public Color ImageColor { get; set; }

        /* --------------------------------------------------------------------- */
        ///
        /// Title
        ///
        /// <summary>
        /// Gets or sets the font of the notice title.
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        [Browsable(true)]
        public Font Title { get; set; }

        /* --------------------------------------------------------------------- */
        ///
        /// TitleColor
        ///
        /// <summary>
        /// Gets or sets the color of the notice title.
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        [Browsable(true)]
        public Color TitleColor { get; set; }

        /* --------------------------------------------------------------------- */
        ///
        /// Description
        ///
        /// <summary>
        /// Gets or sets the font of the notice description.
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        [Browsable(true)]
        public Font Description { get; set; }

        /* --------------------------------------------------------------------- */
        ///
        /// DescriptionColor
        ///
        /// <summary>
        /// Gets or sets the color of the notice description.
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        [Browsable(true)]
        public Color DescriptionColor { get; set; }

        #endregion
    }
}
