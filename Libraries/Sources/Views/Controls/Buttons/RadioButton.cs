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

namespace Cube.Forms.Controls
{
    /* --------------------------------------------------------------------- */
    ///
    /// RadioButton
    ///
    /// <summary>
    /// Represents the radio button control.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class RadioButton : System.Windows.Forms.RadioButton
    {
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// RadioButton
        ///
        /// <summary>
        /// Initializes a new instance of the RadioButton class.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public RadioButton()
        {
            _painter = new RadioButtonPainter(this);
            _painter.Style.PropertyChanged += (s, e) => Invalidate();
        }

        #endregion

        #region Properties

        /* ----------------------------------------------------------------- */
        ///
        /// Style
        ///
        /// <summary>
        /// Gets the styles of the button.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public ButtonStyle Style => _painter.Style;

        /* ----------------------------------------------------------------- */
        ///
        /// Content
        ///
        /// <summary>
        /// Gets or sets the content to be displayed on the button.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Browsable(true)]
        public string Content
        {
            get => _painter.Content;
            set
            {
                if (_painter.Content == value) return;
                _painter.Content = value;
                Invalidate();
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// ShowFocusCues
        ///
        /// <summary>
        /// Gets the value indicating whether or not to display a border
        /// when focusing.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected override bool ShowFocusCues => false;

        #region Hiding properties

        #region ButtonBase

        /* ----------------------------------------------------------------- */
        ///
        /// BackColor
        ///
        /// <summary>
        /// Gets or sets the background color.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new Color BackColor
        {
            get => base.BackColor;
            set => base.BackColor = value;
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
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new Image BackgroundImage
        {
            get => base.BackgroundImage;
            set => base.BackgroundImage = value;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// FlatAppearance
        ///
        /// <summary>
        /// Gets the appearance of the border and the colors used to
        /// indicate check state and mouse state.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new System.Windows.Forms.FlatButtonAppearance FlatAppearance => base.FlatAppearance;

        /* ----------------------------------------------------------------- */
        ///
        /// FlatStyle
        ///
        /// <summary>
        /// Gets or sets the flat style appearance of the button control.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new System.Windows.Forms.FlatStyle FlatStyle
        {
            get => base.FlatStyle;
            set => base.FlatStyle = value;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// ForeColor
        ///
        /// <summary>
        /// Gets or sets the foreground color.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new Color ForeColor
        {
            get => base.ForeColor;
            set => base.ForeColor = value;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Image
        ///
        /// <summary>
        /// Gets or sets the image that is displayed on a button control.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new Image Image
        {
            get => base.Image;
            set => base.Image = value;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// ImageIndex
        ///
        /// <summary>
        /// Gets or sets the image list index value of the image displayed
        /// on the button control.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new int ImageIndex
        {
            get => base.ImageIndex;
            set => base.ImageIndex = value;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// ImageKey
        ///
        /// <summary>
        /// Gets or sets the key accessor for the image in the ImageList.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new string ImageKey
        {
            get => base.ImageKey;
            set => base.ImageKey = value;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// ImageList
        ///
        /// <summary>
        /// Gets or sets the ImageList that contains the Image displayed
        /// on a button control.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new System.Windows.Forms.ImageList ImageList
        {
            get => base.ImageList;
            set => base.ImageList = value;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Text
        ///
        /// <summary>
        /// Gets or sets the text associated with this control.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new string Text
        {
            get => base.Text;
            set => base.Text = value;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// TextImageRelation
        ///
        /// <summary>
        /// Gets or sets the position of text and image relative to each
        /// other.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new System.Windows.Forms.TextImageRelation TextImageRelation
        {
            get => base.TextImageRelation;
            set => base.TextImageRelation = value;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// UseVisualStyleBakColor
        ///
        /// <summary>
        /// Gets or sets a value that determines if the background is drawn
        /// using visual styles, if supported.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new bool UseVisualStyleBackColor
        {
            get => base.UseVisualStyleBackColor;
            set => base.UseVisualStyleBackColor = value;
        }

        #endregion

        #region RadioButton

        /* ----------------------------------------------------------------- */
        ///
        /// Appearance
        ///
        /// <summary>
        /// Gets or sets a value determining the appearance.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new System.Windows.Forms.Appearance Appearance
        {
            get => base.Appearance;
            set => base.Appearance = value;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// CheckAlign
        ///
        /// <summary>
        /// Gets or sets the location of the check box portion.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new ContentAlignment CheckAlign
        {
            get => base.CheckAlign;
            set => base.CheckAlign = value;
        }

        #endregion

        #endregion

        #endregion

        #region Events

        #region NcHitTest

        /* ----------------------------------------------------------------- */
        ///
        /// NcHitTest
        ///
        /// <summary>
        /// Occurs when the hit test of the non-client area.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public event QueryEventHandler<Point, Position> NcHitTest;

        /* ----------------------------------------------------------------- */
        ///
        /// OnNcHitTest
        ///
        /// <summary>
        /// Raises the NcHitTest event.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected virtual void OnNcHitTest(QueryMessage<Point, Position> e) =>
            NcHitTest?.Invoke(this, e);

        #endregion

        #endregion

        #region Fields
        private readonly ButtonPainter _painter = null;
        #endregion
    }
}
