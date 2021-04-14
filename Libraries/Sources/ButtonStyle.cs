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
    /// ButtonStyle
    ///
    /// <summary>
    /// Represents the style of the customized button.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [TypeConverter(typeof(OnlyExpandableConverter))]
    public class ButtonStyle : ObservableBase
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
            get => GetProperty<Color>();
            set => SetProperty(value);
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
            get => GetProperty<Image>();
            set => SetProperty(value);
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
            get => GetProperty<Color>();
            set => SetProperty(value);
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
            get => GetProperty(() => -1);
            set => SetProperty(value);
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
            get => GetProperty<Image>();
            set => SetProperty(value);
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
            get => GetProperty<Color>();
            set => SetProperty(value);
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
    /// ButtonStyleContainer
    ///
    /// <summary>
    /// Represents the style of the customized button (default, checked,
    /// mouse-over, mouse-down, and disabled condition).
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [TypeConverter(typeof(OnlyExpandableConverter))]
    public class ButtonStyleContainer : ObservableBase
    {
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// ButtonStyleContainer
        ///
        /// <summary>
        /// Initialized a new instance of the ButtonStyleContainer class.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public ButtonStyleContainer()
        {
            Default.PropertyChanged   += (s, e) => Refresh(nameof(Default));
            Checked.PropertyChanged   += (s, e) => Refresh(nameof(Checked));
            Disabled.PropertyChanged  += (s, e) => Refresh(nameof(Disabled));
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
        public ButtonStyle Default { get; } = new ButtonStyle
        {
            ContentColor = SystemColors.ControlText,
            BorderColor  = SystemColors.ActiveBorder,
            BorderSize   = 1
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
        public ButtonStyle Checked { get; } = new ButtonStyle();

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
        public ButtonStyle Disabled { get; } = new ButtonStyle
        {
            BackColor    = SystemColors.Control,
            ContentColor = SystemColors.GrayText,
            BorderColor  = SystemColors.InactiveBorder
        };

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
        public ButtonStyle MouseOver { get; } = new ButtonStyle();

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
        public ButtonStyle MouseDown { get; } = new ButtonStyle();

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
