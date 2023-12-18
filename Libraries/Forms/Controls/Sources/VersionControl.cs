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
namespace Cube.Forms.Controls;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Reflection;
using Cube.Reflection.Extensions;
using Cube.Text.Extensions;

/* ------------------------------------------------------------------------- */
///
/// VersionControl
///
/// <summary>
/// Represents the user control to show the version information.
/// </summary>
///
/* ------------------------------------------------------------------------- */
public class VersionControl : ControlBase
{
    #region Constructors

    /* --------------------------------------------------------------------- */
    ///
    /// VersionControl
    ///
    /// <summary>
    /// Initializes a new instance of the VersionControl class.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public VersionControl()
    {
        var asm = Assembly.GetEntryAssembly() ?? GetType().Assembly;

        Copyright = asm.GetCopyright();
        Product   = asm.GetProduct();
        Version   = asm.GetVersionString(3, true);
        Size      = new(340, 100);

        InitializeLayout();
    }

    #endregion

    #region Properties

    /* --------------------------------------------------------------------- */
    ///
    /// Image
    ///
    /// <summary>
    /// Gets or sets the product image.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [Browsable(true)]
    public Image Image
    {
        get => _image.Image;
        set
        {
            if (_image.Image == value) return;
            _image.Image = value;

            var show = value is not null;
            _panel.Panel1Collapsed  = !show;
            _panel.SplitterDistance = show ? Math.Max(value.Width, 1) : 1;
            _panel.SplitterWidth    = show ? 8 : 1;
        }
    }

    /* --------------------------------------------------------------------- */
    ///
    /// Product
    ///
    /// <summary>
    /// Gets or sets the product name.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [Browsable(true)]
    public string Product
    {
        get => _product;
        set => Set(ref _product, value);
    }

    /* --------------------------------------------------------------------- */
    ///
    /// Version
    ///
    /// <summary>
    /// Gets or sets the version information.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [Browsable(true)]
    public string Version
    {
        get => _version;
        set => Set(ref _version, value);
    }

    /* --------------------------------------------------------------------- */
    ///
    /// OneLine
    ///
    /// <summary>
    /// Gets or sets a value indicating whether the product name and
    /// version number should be written on a single line.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [Browsable(true)]
    [DefaultValue(false)]
    public bool OneLine
    {
        get => _oneline;
        set => Set(ref _oneline, value);
    }

    /* --------------------------------------------------------------------- */
    ///
    /// Description
    ///
    /// <summary>
    /// Gets or sets other information to be displayed on the version
    /// control.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [Browsable(true)]
    public string Description
    {
        get => _others.Text;
        set => Set(_others, value);
    }

    /* --------------------------------------------------------------------- */
    ///
    /// Copyright
    ///
    /// <summary>
    /// Gets or sets the copyright notation.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [Browsable(true)]
    public string Copyright
    {
        get => _copyright.Text;
        set => Set(_copyright, value);
    }

    /* --------------------------------------------------------------------- */
    ///
    /// Uri
    ///
    /// <summary>
    /// Gets or sets the URL of the Web page to be displayed when the
    /// copyright notation is clicked.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [Browsable(true)]
    public Uri Uri { get; set; }

    /* --------------------------------------------------------------------- */
    ///
    /// Platform
    ///
    /// <summary>
    /// Gets the platform information.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [Browsable(false)]
    public string Platform => string.Format("{0} ({1}){2}{3}{4}",
        Environment.OSVersion,
        Environment.Is64BitOperatingSystem ? "64bit" : "32bit",
        Environment.NewLine,
        $".NET Framework {Environment.Version}",
        Environment.NewLine
    );

    #endregion

    #region Implementations

    /* --------------------------------------------------------------------- */
    ///
    /// Set
    ///
    /// <summary>
    /// Sets the specified value to the specified field.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    private void Set<T>(ref T field, T value)
    {
        if (!EqualityComparer<T>.Default.Equals(field, value))
        {
            field = value;
            var sep = OneLine ? " " : Environment.NewLine;
            Set(_info, $"{Product}{sep}{Version}");
        }
    }

    /* --------------------------------------------------------------------- */
    ///
    /// Set
    ///
    /// <summary>
    /// Updates the label content.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    private void Set(System.Windows.Forms.Label src, string value)
    {
        if (!src.Text.Equals(value, StringComparison.InvariantCulture))
        {
            src.Text    = value;
            src.Visible = value.HasValue();
        }
    }

    /* --------------------------------------------------------------------- */
    ///
    /// InitializeLayout
    ///
    /// <summary>
    /// Initializes the layout.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    private void InitializeLayout()
    {
        SuspendLayout();

        var index = 0;

        _panel.Dock = System.Windows.Forms.DockStyle.Fill;
        _panel.Margin = new System.Windows.Forms.Padding(0);
        _panel.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
        _panel.IsSplitterFixed = true;
        _panel.Panel1MinSize = 0;
        _panel.Panel2MinSize = 0;
        _panel.SplitterDistance = 48;
        _panel.SplitterWidth = 8;
        _panel.Size = Size;
        _panel.SuspendLayout();

        _image.Dock = System.Windows.Forms.DockStyle.Fill;
        _image.Image = Properties.Resources.Logo;
        _image.Margin = new System.Windows.Forms.Padding(0);

        _contents.Dock = System.Windows.Forms.DockStyle.Fill;
        _contents.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
        _contents.Margin = new System.Windows.Forms.Padding(0);
        _contents.SuspendLayout();

        _info.AutoEllipsis = true;
        _info.AutoSize = true;
        _info.Margin = new System.Windows.Forms.Padding(0);
        _info.TabIndex = index++;

        _platform.AutoEllipsis = true;
        _platform.AutoSize = true;
        _platform.ForeColor = SystemColors.GrayText;
        _platform.Margin = new System.Windows.Forms.Padding(0, Space, 0, 0);
        _platform.TabIndex = index++;
        _platform.Text = Platform;

        _others.AutoEllipsis = true;
        _others.AutoSize = true;
        _others.ForeColor = SystemColors.GrayText;
        _others.Margin = new System.Windows.Forms.Padding(0, Space, 0, 0);
        _others.TabIndex = index++;
        _others.Text = string.Empty;
        _others.Visible = false;

        _copyright.AutoSize = true;
        _copyright.Margin = new System.Windows.Forms.Padding(0, Space, 0, 0);
        _copyright.TabIndex = index++;
        _copyright.LinkClicked += (_, _) =>
        {
            if (Uri is null) return;
            Logger.Try(() => Process.Start(new ProcessStartInfo
            {
                FileName = Uri.ToString(),
                UseShellExecute = true,
            }));
        };

        _contents.Controls.Add(_info);
        _contents.Controls.Add(_platform);
        _contents.Controls.Add(_others);
        _contents.Controls.Add(_copyright);

        _panel.Panel1.Controls.Add(_image);
        _panel.Panel2.Controls.Add(_contents);

        Controls.Add(_panel);
        SetStyle(System.Windows.Forms.ControlStyles.Selectable, false);

        _contents.ResumeLayout(false);
        _panel.ResumeLayout(false);
        ResumeLayout(false);
    }

    #endregion

    #region Fields
    private readonly System.Windows.Forms.SplitContainer _panel = new();
    private readonly System.Windows.Forms.FlowLayoutPanel _contents = new();
    private readonly System.Windows.Forms.PictureBox _image = new();
    private readonly System.Windows.Forms.Label _info = new();
    private readonly System.Windows.Forms.Label _platform = new();
    private readonly System.Windows.Forms.Label _others = new();
    private readonly System.Windows.Forms.LinkLabel _copyright = new();
    private string _product;
    private string _version;
    private bool _oneline = true;
    private const int Space = 16;
    #endregion
}
