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
using System.Collections.Generic;
using System.Windows.Forms;

namespace Cube.Forms.Behaviors
{
    /* --------------------------------------------------------------------- */
    ///
    /// MessageBoxBehavior
    ///
    /// <summary>
    /// Provides functionality to show a message box.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class DialogBehavior : SubscribeBehavior<DialogMessage>
    {
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// DialogBehavior
        ///
        /// <summary>
        /// Initializes a new instance of the DialogBehavior class
        /// with the specified presentable object.
        /// </summary>
        ///
        /// <param name="src">Presentable object.</param>
        ///
        /* ----------------------------------------------------------------- */
        public DialogBehavior(IPresentable src) : base(src) { }

        #endregion

        #region Implementations

        /* ----------------------------------------------------------------- */
        ///
        /// Invoke
        ///
        /// <summary>
        /// Shows a message box with the specified message.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected override void Invoke(DialogMessage e)
        {
            var icon = Icons[e.Icon];
            var buttons = Buttons[e.Buttons];
            var status = MessageBox.Show(e.Value, e.Title, buttons, icon);

            e.Status = Results.ContainsKey(status) ? Results[status] : DialogStatus.None;
            e.Callback?.Invoke(e);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Icons
        ///
        /// <summary>
        /// Gets the map collection of DialogIcon and MessageBoxImage.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private static Dictionary<DialogIcon, MessageBoxIcon> Icons { get; } =
            new Dictionary<DialogIcon, MessageBoxIcon>
            {
                { DialogIcon.None,        MessageBoxIcon.None        },
                { DialogIcon.Error,       MessageBoxIcon.Error       },
                { DialogIcon.Warning,     MessageBoxIcon.Warning     },
                { DialogIcon.Information, MessageBoxIcon.Information },
            };

        /* ----------------------------------------------------------------- */
        ///
        /// Buttons
        ///
        /// <summary>
        /// Gets the map collection of DialogButtons and MessageBoxButton.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private static Dictionary<DialogButtons, MessageBoxButtons> Buttons { get; } =
            new Dictionary<DialogButtons, MessageBoxButtons>
            {
                { DialogButtons.Ok,          MessageBoxButtons.OK          },
                { DialogButtons.OkCancel,    MessageBoxButtons.OKCancel    },
                { DialogButtons.YesNo,       MessageBoxButtons.YesNo       },
                { DialogButtons.YesNoCancel, MessageBoxButtons.YesNoCancel },
            };

        /* ----------------------------------------------------------------- */
        ///
        /// Results
        ///
        /// <summary>
        /// Gets the map collection of MessageBoxResult and DialogResult.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private static Dictionary<DialogResult, DialogStatus> Results { get; } =
            new Dictionary<DialogResult, DialogStatus>
            {
                { DialogResult.None,   DialogStatus.None   },
                { DialogResult.OK,     DialogStatus.Ok     },
                { DialogResult.Cancel, DialogStatus.Cancel },
                { DialogResult.Yes,    DialogStatus.Yes    },
                { DialogResult.No,     DialogStatus.No     },
            };

        #endregion
    }
}
