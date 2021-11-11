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
using System.ComponentModel;
using System.Drawing;
using System.Linq;

namespace Cube.Forms.Controls
{
    /* --------------------------------------------------------------------- */
    ///
    /// ColorButton
    ///
    /// <summary>
    /// Represents a button control for setting the color.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class ColorButton : Button
    {
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// ColorButton
        ///
        /// <summary>
        /// Initializes a new instance of the ColorButton class.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public ColorButton()
        {
            UseVisualStyleBackColor = false;
            BackColor = SystemColors.Control;
            ForeColor = BackColor;
            FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            FlatAppearance.BorderColor = SystemColors.ControlDark;
            FlatAppearance.BorderSize = 1;
        }

        #endregion

        #region Properties

        /* ----------------------------------------------------------------- */
        ///
        /// AnyColor
        ///
        /// <summary>
        /// Gets or sets a value indicating whether all available colors
        /// should be displayed in the dialog box as a basic color set.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Browsable(true)]
        [DefaultValue(true)]
        public bool AnyColor { get; set; } = true;

        /* ----------------------------------------------------------------- */
        ///
        /// SolidColorOnly
        ///
        /// <summary>
        /// Gets or sets a value indicating whether to limit the colors
        /// in the dialog box to only solid colors.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Browsable(true)]
        [DefaultValue(false)]
        public bool SolidColorOnly { get; set; } = false;

        /* ----------------------------------------------------------------- */
        ///
        /// AllowFullOpen
        ///
        /// <summary>
        /// Gets or sets a value indicating whether the user can define a
        /// custom color using the dialog box.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Browsable(true)]
        [DefaultValue(true)]
        public bool AllowFullOpen { get; set; } = true;

        /* ----------------------------------------------------------------- */
        ///
        /// FullOpen
        ///
        /// <summary>
        /// Gets or sets a value indicating whether to display the controls
        /// for creating a custom color when the dialog box is opened.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Browsable(true)]
        [DefaultValue(true)]
        public bool FullOpen { get; set; } = true;

        /* ----------------------------------------------------------------- */
        ///
        /// CustomColors
        ///
        /// <summary>
        /// Gets or sets the custom color set displayed in the dialog box.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IList<int> CustomColors { get; set; } = new List<int>();

        #endregion

        #region Implementations

        /* ----------------------------------------------------------------- */
        ///
        /// OnClick
        ///
        /// <summary>
        /// Occurs when the button is clicked.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);

            var dialog = new System.Windows.Forms.ColorDialog
            {
                Color          = BackColor,
                AnyColor       = AnyColor,
                SolidColorOnly = SolidColorOnly,
                AllowFullOpen  = AllowFullOpen,
                FullOpen       = FullOpen
            };

            if (CustomColors != null) dialog.CustomColors = CustomColors.ToArray();

            var result = dialog.ShowDialog();

            FullOpen = dialog.FullOpen;
            if (CustomColors != null)
            {
                CustomColors.Clear();
                foreach (var color in dialog.CustomColors) CustomColors.Add(color);
            }

            if (result == System.Windows.Forms.DialogResult.Cancel) return;

            BackColor = dialog.Color;
            ForeColor = BackColor;
        }

        #endregion
    }
}
