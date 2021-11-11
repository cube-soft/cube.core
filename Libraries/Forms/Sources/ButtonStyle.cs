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
using System.ComponentModel;
using System.Drawing;

namespace Cube.Forms
{
    /* --------------------------------------------------------------------- */
    ///
    /// ButtonStyleElement
    ///
    /// <summary>
    /// Represents the style of the customized button.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [TypeConverter(typeof(OnlyExpandableConverter))]
    public class ButtonStyleElement : ObservableBase
    {
        #region Properties

        /* ----------------------------------------------------------------- */
        ///
        /// BackColor
        ///
        /// <summary>
        /// Gets or sets the background color.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Browsable(true)]
        [DefaultValue(typeof(Color), "")]
        public Color BackColor
        {
            get => Get<Color>();
            set => Set(value);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// BackgroundImage
        ///
        /// <summary>
        /// Gets or sets the background image.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Browsable(true)]
        [DefaultValue(null)]
        public Image BackgroundImage
        {
            get => Get<Image>();
            set => Set(value);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// BorderColor
        ///
        /// <summary>
        /// Gets or sets the border color.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Browsable(true)]
        [DefaultValue(typeof(Color), "")]
        public Color BorderColor
        {
            get => Get<Color>();
            set => Set(value);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// BorderSize
        ///
        /// <summary>
        /// Gets or sets the border width.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Browsable(true)]
        [DefaultValue(-1)]
        public int BorderSize
        {
            get => Get(() => -1);
            set => Set(value);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Image
        ///
        /// <summary>
        /// Gets or sets the image displayed in the control.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Browsable(true)]
        [DefaultValue(null)]
        public Image Image
        {
            get => Get<Image>();
            set => Set(value);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// ContentColor
        ///
        /// <summary>
        /// Gets or sets the color of the main content (a.k.a ForeColor).
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Browsable(true)]
        [DefaultValue(typeof(Color), "")]
        public Color ContentColor
        {
            get => Get<Color>();
            set => Set(value);
        }

        #endregion

        #region  Methods

        /* ----------------------------------------------------------------- */
        ///
        /// Dispose
        ///
        /// <summary>
        /// Releases the unmanaged resources used by the object and
        /// optionally releases the managed resources.
        /// </summary>
        ///
        /// <param name="disposing">
        /// true to release both managed and unmanaged resources;
        /// false to release only unmanaged resources.
        /// </param>
        ///
        /* ----------------------------------------------------------------- */
        protected override void Dispose(bool disposing) { }

        #endregion
    }

    /* --------------------------------------------------------------------- */
    ///
    /// ButtonStyle
    ///
    /// <summary>
    /// Represents the style of the customized button (default, checked,
    /// mouse-over, mouse-down, and disabled condition).
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [TypeConverter(typeof(OnlyExpandableConverter))]
    public class ButtonStyle : ObservableBase
    {
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// ButtonStyle
        ///
        /// <summary>
        /// Initialized a new instance of the ButtonStyle class.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public ButtonStyle()
        {
            Default.PropertyChanged   += (s, e) => Refresh(nameof(Default));
            Disabled.PropertyChanged  += (s, e) => Refresh(nameof(Disabled));
            Checked.PropertyChanged   += (s, e) => Refresh(nameof(Checked));
            MouseOver.PropertyChanged += (s, e) => Refresh(nameof(MouseOver));
            MouseDown.PropertyChanged += (s, e) => Refresh(nameof(MouseDown));
        }

        #endregion

        #region Properties

        /* ----------------------------------------------------------------- */
        ///
        /// Default
        ///
        /// <summary>
        /// Gets or sets the style of the normal state.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public ButtonStyleElement Default { get; } = new ButtonStyleElement
        {
            ContentColor    = SystemColors.ControlText,
            BorderColor     = SystemColors.ActiveBorder,
            BorderSize      = 1,
        };

        /* ----------------------------------------------------------------- */
        ///
        /// Disabled
        ///
        /// <summary>
        /// Gets or sets the style of the disabled state.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public ButtonStyleElement Disabled { get; } = new ButtonStyleElement
        {
            BackColor = SystemColors.Control,
            ContentColor    = SystemColors.GrayText,
            BorderColor     = SystemColors.InactiveBorder
        };

        /* ----------------------------------------------------------------- */
        ///
        /// Checked
        ///
        /// <summary>
        /// Gets or sets the style of the checked state.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public ButtonStyleElement Checked { get; } = new ButtonStyleElement();

        /* ----------------------------------------------------------------- */
        ///
        /// MouseOver
        ///
        /// <summary>
        /// Gets or sets the style of the mouse-over state.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public ButtonStyleElement MouseOver { get; } = new ButtonStyleElement();

        /* ----------------------------------------------------------------- */
        ///
        /// MouseDown
        ///
        /// <summary>
        /// Gets or sets the style of the mouse-down state.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public ButtonStyleElement MouseDown { get; } = new ButtonStyleElement();

        #endregion

        #region Methods

        /* ----------------------------------------------------------------- */
        ///
        /// Dispose
        ///
        /// <summary>
        /// Releases the unmanaged resources used by the object and
        /// optionally releases the managed resources.
        /// </summary>
        ///
        /// <param name="disposing">
        /// true to release both managed and unmanaged resources;
        /// false to release only unmanaged resources.
        /// </param>
        ///
        /* ----------------------------------------------------------------- */
        protected override void Dispose(bool disposing) { }

        #endregion
    }
}
