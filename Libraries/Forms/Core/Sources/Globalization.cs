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
namespace Cube.Forms.Globalization;

using System.ComponentModel;
using System.Threading;
using System.Windows.Forms;
using Cube.Globalization;

/* ------------------------------------------------------------------------- */
///
/// Methods
///
/// <summary>
/// Provides the extended methods for i18n.
/// </summary>
///
/* ------------------------------------------------------------------------- */
public static class Methods
{
    #region Methods

    /* --------------------------------------------------------------------- */
    ///
    /// Update
    ///
    /// <summary>
    /// Updates the culture information corresponding to the specified
    /// language.
    /// </summary>
    ///
    /// <param name="src">Form object.</param>
    /// <param name="value">Language to show.</param>
    ///
    /* --------------------------------------------------------------------- */
    public static void Update<TForm>(this TForm src, Language value) where TForm : Form
    {
        var ci = value.ToCultureInfo();
        Thread.CurrentThread.CurrentCulture = ci;
        Thread.CurrentThread.CurrentUICulture = ci;

        var cm = new ComponentResourceManager(typeof(TForm));
        cm.ApplyResources(src, "$this");
        Update(src.Controls, cm);
    }

    #endregion

    #region Implementations

    /* --------------------------------------------------------------------- */
    ///
    /// Update
    ///
    /// <summary>
    /// Updates the culture information.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    private static void Update(Control.ControlCollection src, ComponentResourceManager cm)
    {
        foreach (Control e in src)
        {
            cm.ApplyResources(e, e.Name);
            Update(e.Controls, cm);
        }
    }

    #endregion
}
