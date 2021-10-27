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

namespace Cube.Forms.Controls
{
    /* --------------------------------------------------------------------- */
    ///
    /// FontButton
    ///
    /// <summary>
    /// Represents a button control for setting the font.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class FontButton : Button
    {
        #region Properties

        /* ----------------------------------------------------------------- */
        ///
        /// AllowScriptChange
        ///
        /// <summary>
        /// Gets or sets a value indicating whether the user can change the
        /// character set specified in the Script combo box to display a
        /// different character set than the one currently displayed.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Browsable(true)]
        [DefaultValue(true)]
        public bool AllowScriptChange { get; set; } = true;

        /* ----------------------------------------------------------------- */
        ///
        /// AllowSimulations
        ///
        /// <summary>
        /// Gets or sets a value indicating whether the dialog box can
        /// simulate font display in the GDI.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Browsable(true)]
        [DefaultValue(true)]
        public bool AllowSimulations { get; set; } = true;

        /* ----------------------------------------------------------------- */
        ///
        /// AllowVectorFonts
        ///
        /// <summary>
        /// Gets or sets a value indicating whether vector fonts can be
        /// selected in the dialog box.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Browsable(true)]
        [DefaultValue(true)]
        public bool AllowVectorFonts { get; set; } = true;

        /* ----------------------------------------------------------------- */
        ///
        /// AllowVerticalFonts
        ///
        /// <summary>
        /// Gets or sets a value indicating whether vertical fonts can be
        /// selected in the dialog box.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Browsable(true)]
        [DefaultValue(true)]
        public bool AllowVerticalFonts { get; set; } = true;

        /* ----------------------------------------------------------------- */
        ///
        /// FixedPitchOnly
        ///
        /// <summary>
        /// Gets or sets a value indicating whether to limit the selection
        /// of fonts in the dialog box to fixed-width fonts only.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Browsable(true)]
        [DefaultValue(true)]
        public bool FixedPitchOnly { get; set; } = false;

        /* ----------------------------------------------------------------- */
        ///
        /// FontMustExist
        ///
        /// <summary>
        /// Gets or sets a value indicating whether or not an error message
        /// will be displayed in the dialog box if the user tries to select
        /// a non-existent font or style.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Browsable(true)]
        [DefaultValue(false)]
        public bool FontMustExist { get; set; } = false;

        /* ----------------------------------------------------------------- */
        ///
        /// ScriptsOnly
        ///
        /// <summary>
        /// Gets or sets a value indicating whether the dialog box allows
        /// you to select fonts for all non-OEM, Symbol, and ANSI character
        /// sets.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Browsable(true)]
        [DefaultValue(false)]
        public bool ScriptsOnly { get; set; } = false;

        /* ----------------------------------------------------------------- */
        ///
        /// ShowApply
        ///
        /// <summary>
        /// Gets or sets a value indicating whether to show the apply
        /// button in the dialog box.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Browsable(true)]
        [DefaultValue(false)]
        public bool ShowApply { get; set; } = false;

        /* ----------------------------------------------------------------- */
        ///
        /// ShowColor
        ///
        /// <summary>
        /// Gets or sets a value indicating whether the dialog box shows
        /// color selection.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Browsable(true)]
        [DefaultValue(false)]
        public bool ShowColor { get; set; } = false;

        /* ----------------------------------------------------------------- */
        ///
        /// ShowEffects
        ///
        /// <summary>
        /// Sets or gets a value indicating whether the dialog box shows
        /// controls for the user to specify options such as strike-through,
        /// underline, text color, etc.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Browsable(true)]
        [DefaultValue(true)]
        public bool ShowEffects { get; set; } = true;

        /* ----------------------------------------------------------------- */
        ///
        /// MaxSize
        ///
        /// <summary>
        /// Gets or sets the maximum size in points.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Browsable(true)]
        [DefaultValue(0)]
        public int MaxSize { get; set; } = 0;

        /* ----------------------------------------------------------------- */
        ///
        /// MinSize
        ///
        /// <summary>
        /// Gets or sets the minimum size in points.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Browsable(true)]
        [DefaultValue(0)]
        public int MinSize { get; set; } = 0;

        #endregion

        #region Events

        /* ----------------------------------------------------------------- */
        ///
        /// Apply
        ///
        /// <summary>
        /// Occurs when the apply button is clicked.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public event EventHandler Apply;

        /* ----------------------------------------------------------------- */
        ///
        /// OnApply
        ///
        /// <summary>
        /// Raises the Apply event.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected virtual void OnApply(EventArgs e) => Apply?.Invoke(this, e);

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

            var dialog = new System.Windows.Forms.FontDialog
            {
                AllowScriptChange   = AllowScriptChange,
                AllowSimulations    = AllowSimulations,
                AllowVectorFonts    = AllowVectorFonts,
                AllowVerticalFonts  = AllowVerticalFonts,
                Color               = ForeColor,
                FixedPitchOnly      = FixedPitchOnly,
                Font                = Font,
                FontMustExist       = FontMustExist,
                MaxSize             = MaxSize,
                MinSize             = MinSize,
                ScriptsOnly         = ScriptsOnly,
                ShowApply           = ShowApply,
                ShowColor           = ShowColor,
                ShowEffects         = ShowEffects,
            };

            void handler(object s, EventArgs e) => OnApply(e);
            dialog.Apply += handler;
            var result = dialog.ShowDialog();
            dialog.Apply -= handler;
            if (result == System.Windows.Forms.DialogResult.Cancel) return;

            ForeColor = dialog.Color;
            Font = dialog.Font;
        }

        #endregion
    }
}
