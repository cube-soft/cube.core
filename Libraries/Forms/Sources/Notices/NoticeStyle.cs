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

namespace Cube
{
    #region NoticeStyle

    /* --------------------------------------------------------------------- */
    ///
    /// NoticeStyle
    ///
    /// <summary>
    /// Represents the styles of the notice window.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class NoticeStyle
    {
        /* --------------------------------------------------------------------- */
        ///
        /// Color
        ///
        /// <summary>
        /// Gets or sets the background color.
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        public Color Color { get; set; }

        /* --------------------------------------------------------------------- */
        ///
        /// Image
        ///
        /// <summary>
        /// Gets or sets the style of the image part.
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        public NoticeImageStyle Image { get; set; }

        /* --------------------------------------------------------------------- */
        ///
        /// Title
        ///
        /// <summary>
        /// Gets or sets the style of the title part.
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        public NoticeTextStyle Title { get; set; }

        /* --------------------------------------------------------------------- */
        ///
        /// Text
        ///
        /// <summary>
        /// Gets or sets the style of the message part.
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        public NoticeTextStyle Text { get; set; }
    }

    #endregion

    #region NoticeTextStyle

    /* --------------------------------------------------------------------- */
    ///
    /// NoticeTextStyle
    ///
    /// <summary>
    /// Represents the styles of a text part of the notice window.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class NoticeTextStyle
    {
        /* --------------------------------------------------------------------- */
        ///
        /// Color
        ///
        /// <summary>
        /// Gets or sets the content/foreground color of the component.
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        public Color Color { get; set; }

        /* --------------------------------------------------------------------- */
        ///
        /// Font
        ///
        /// <summary>
        /// Gets or sets the font object of the component.
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        public Font Font { get; set; }
    }

    #endregion

    #region NoticeImageStyle

    /* --------------------------------------------------------------------- */
    ///
    /// NoticeImageStyle
    ///
    /// <summary>
    /// Represents the styles of an image part of the notice window.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class NoticeImageStyle
    {
        /* --------------------------------------------------------------------- */
        ///
        /// Color
        ///
        /// <summary>
        /// Gets or sets the background color of the component.
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        public Color Color { get; set; }

        /* --------------------------------------------------------------------- */
        ///
        /// Value
        ///
        /// <summary>
        /// Gets or sets the image object of the component.
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        public Image Value { get; set; }
    }

    #endregion
}
