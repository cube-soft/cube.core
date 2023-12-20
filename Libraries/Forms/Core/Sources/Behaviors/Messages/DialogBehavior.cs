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
namespace Cube.Forms.Behaviors;

using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

/* ------------------------------------------------------------------------- */
///
/// MessageBoxBehavior
///
/// <summary>
/// Provides functionality to show a message box.
/// </summary>
///
/* ------------------------------------------------------------------------- */
public class DialogBehavior : MessageBehavior<DialogMessage>
{
    #region Constructors

    /* --------------------------------------------------------------------- */
    ///
    /// DialogBehavior
    ///
    /// <summary>
    /// Initializes a new instance of the DialogBehavior class
    /// with the specified presentable object.
    /// </summary>
    ///
    /// <param name="aggregator">Message aggregator.</param>
    ///
    /* --------------------------------------------------------------------- */
    public DialogBehavior(IAggregator aggregator) : base(aggregator, e =>
    {
        var icon    = Icons[e.Icon];
        var buttons = Buttons[e.Buttons];
        var status  = MessageBox.Show(e.Text, e.Title, buttons, icon);

        e.Value  = Results.TryGetValue(status, out var dest) ? dest : DialogStatus.Empty;
        e.Cancel = e.CancelCandidates.Any(z => z == e.Value);
    }) { }

    #endregion

    #region Implementations

    /* --------------------------------------------------------------------- */
    ///
    /// Icons
    ///
    /// <summary>
    /// Gets the map collection of DialogIcon and MessageBoxImage.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    private static Dictionary<DialogIcon, MessageBoxIcon> Icons { get; } = new()
    {
        { DialogIcon.None,        MessageBoxIcon.None        },
        { DialogIcon.Error,       MessageBoxIcon.Error       },
        { DialogIcon.Warning,     MessageBoxIcon.Warning     },
        { DialogIcon.Information, MessageBoxIcon.Information },
    };

    /* --------------------------------------------------------------------- */
    ///
    /// Buttons
    ///
    /// <summary>
    /// Gets the map collection of DialogButtons and MessageBoxButton.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    private static Dictionary<DialogButtons, MessageBoxButtons> Buttons { get; } = new()
    {
        { DialogButtons.Ok,          MessageBoxButtons.OK          },
        { DialogButtons.OkCancel,    MessageBoxButtons.OKCancel    },
        { DialogButtons.YesNo,       MessageBoxButtons.YesNo       },
        { DialogButtons.YesNoCancel, MessageBoxButtons.YesNoCancel },
    };

    /* --------------------------------------------------------------------- */
    ///
    /// Results
    ///
    /// <summary>
    /// Gets the map collection of MessageBoxResult and DialogResult.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    private static Dictionary<DialogResult, DialogStatus> Results { get; } = new()
    {
        { DialogResult.None,   DialogStatus.Empty  },
        { DialogResult.OK,     DialogStatus.Ok     },
        { DialogResult.Cancel, DialogStatus.Cancel },
        { DialogResult.Yes,    DialogStatus.Yes    },
        { DialogResult.No,     DialogStatus.No     },
    };

    #endregion
}
