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
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace Cube.Forms
{
    /* --------------------------------------------------------------------- */
    ///
    /// VersionWindow
    ///
    /// <summary>
    /// Represents the window to show the version information.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class VersionWindow : Window
    {
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// VersionWindow
        ///
        /// <summary>
        /// Initializes a new instance of the VersionWindow class.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public VersionWindow() : this(Assembly.GetExecutingAssembly()) { }

        /* ----------------------------------------------------------------- */
        ///
        /// VersionForm
        ///
        /// <summary>
        /// Initializes a new instance of the VersionWindow class with the
        /// specified assembly.
        /// </summary>
        ///
        /// <param name="assembly">Assembly object.</param>
        ///
        /* ----------------------------------------------------------------- */
        public VersionWindow(Assembly assembly)
        {
            _control = new(assembly);
            InitializeLayout();
        }

        #endregion

        #region Properties

        /* ----------------------------------------------------------------- */
        ///
        /// Image
        ///
        /// <summary>
        /// Gets or sets the Image object to show in the window.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Browsable(true)]
        public Image Image
        {
            get => _control.Image;
            set => _control.Image = value;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Product
        ///
        /// <summary>
        /// Gets or sets the product name.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Browsable(true)]
        public string Product
        {
            get => _control.Product;
            set => _control.Product = value;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Version
        ///
        /// <summary>
        /// Gets or sets the string that represents the version.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Browsable(true)]
        public string Version
        {
            get => _control.Version;
            set => _control.Version = value;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Description
        ///
        /// <summary>
        /// Gets or sets other information to be displayed on the window.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Browsable(true)]
        public string Description
        {
            get => _control.Description;
            set => _control.Description = value;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Copyright
        ///
        /// <summary>
        /// Gets or sets the copyright notation.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Browsable(true)]
        public string Copyright
        {
            get => _control.Copyright;
            set => _control.Copyright = value;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Uri
        ///
        /// <summary>
        /// Get or set the URL of the Web page to be displayed when the
        /// copyright notation is clicked.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Browsable(true)]
        public Uri Uri
        {
            get => _control.Uri;
            set => _control.Uri = value;
        }

        #endregion

        #region Methods

        /* ----------------------------------------------------------------- */
        ///
        /// Update
        ///
        /// <summary>
        /// Updates the display contents based on the specified assembly
        /// information.
        /// </summary>
        ///
        /// <param name="assembly">Assembly object.</param>
        ///
        /* ----------------------------------------------------------------- */
        public void Update(Assembly assembly) => _control.Update(assembly);

        #endregion

        #region Implementations

        /* ----------------------------------------------------------------- */
        ///
        /// InitializeLayout
        ///
        /// <summary>
        /// Initializes the layout of the window.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void InitializeLayout()
        {
            Size = new Size(400, 270);
            SuspendLayout();

            _panel.Dock = DockStyle.Fill;
            _panel.Margin = new(0);
            _panel.ColumnCount = 1;
            _ = _panel.ColumnStyles.Add(new(SizeType.Percent, 100F));
            _panel.RowCount = 2;
            _ = _panel.RowStyles.Add(new(SizeType.Percent, 100F));
            _ = _panel.RowStyles.Add(new(SizeType.Absolute, 60F));
            _panel.SuspendLayout();

            _control.BackColor = SystemColors.Window;
            _control.Dock = DockStyle.Fill;
            _control.Margin = new(0);
            _control.Padding = new(20, 20, 0, 0);

            _button.Anchor = AnchorStyles.None;
            _button.Size = new(100, 25);
            _button.Text = "OK";
            _button.Click += (s, e) => Close();

            _panel.Controls.Add(_control, 0, 0);
            _panel.Controls.Add(_button, 0, 1);

            Font = FontFactory.Create(9, FontStyle.Regular, GraphicsUnit.Point);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            ShowInTaskbar = false;

            Controls.Add(_panel);
            _panel.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        #region Fields
        private readonly Controls.VersionControl _control;
        private readonly Controls.TableLayoutPanel _panel = new();
        private readonly Controls.Button _button = new();
        #endregion
    }
}
