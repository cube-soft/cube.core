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
namespace Cube.Xui.Behaviors;

using System.Collections.Generic;
using System.Linq;
using System.Windows;

/* ------------------------------------------------------------------------- */
///
/// DialogBehavior
///
/// <summary>
/// Represents the behavior to show a dialog using a DialogMessage.
/// </summary>
///
/* ------------------------------------------------------------------------- */
public class DialogBehavior : MessageBehavior<DialogMessage>
{
    #region Methods

    /* --------------------------------------------------------------------- */
    ///
    /// Invoke
    ///
    /// <summary>
    /// Invokes the action.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    protected override void Invoke(DialogMessage e)
    {
        var icon    = Icons[e.Icon];
        var buttons = Buttons[e.Buttons];
        var status  = MessageBox.Show(e.Text, e.Title, buttons, icon);

        e.Value  = Results.TryGetValue(status, out var dest) ? dest : DialogStatus.Empty;
        e.Cancel = e.CancelCandidates.Any(z => z == e.Value);
    }

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
    private static Dictionary<DialogIcon, MessageBoxImage> Icons { get; } = new()
    {
        { DialogIcon.None,        MessageBoxImage.None        },
        { DialogIcon.Error,       MessageBoxImage.Error       },
        { DialogIcon.Warning,     MessageBoxImage.Warning     },
        { DialogIcon.Information, MessageBoxImage.Information },
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
    private static Dictionary<DialogButtons, MessageBoxButton> Buttons { get; } = new()
    {
        { DialogButtons.Ok,          MessageBoxButton.OK          },
        { DialogButtons.OkCancel,    MessageBoxButton.OKCancel    },
        { DialogButtons.YesNo,       MessageBoxButton.YesNo       },
        { DialogButtons.YesNoCancel, MessageBoxButton.YesNoCancel },
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
    private static Dictionary<MessageBoxResult, DialogStatus> Results { get; } = new()
    {
        { MessageBoxResult.None,   DialogStatus.Empty  },
        { MessageBoxResult.OK,     DialogStatus.Ok     },
        { MessageBoxResult.Cancel, DialogStatus.Cancel },
        { MessageBoxResult.Yes,    DialogStatus.Yes    },
        { MessageBoxResult.No,     DialogStatus.No     },
    };

    #endregion
}
