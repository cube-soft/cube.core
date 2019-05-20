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
using System.Windows;

namespace Cube.Xui.Behaviors
{
    /* --------------------------------------------------------------------- */
    ///
    /// DialogBehavior
    ///
    /// <summary>
    /// メッセージボックスを表示する Behavior です。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class DialogBehavior : SubscribeBehavior<DialogMessage>
    {
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
            var icon    = Icons[e.Icon];
            var buttons = Buttons[e.Buttons];
            var raw     = MessageBox.Show(e.Value, e.Title, buttons, icon);

            e.Result = Results.ContainsKey(raw) ? Results[raw] : DialogResult.Cancel;
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
        private static Dictionary<DialogIcon, MessageBoxImage> Icons { get; } =
            new Dictionary<DialogIcon, MessageBoxImage>
            {
                { DialogIcon.None,        MessageBoxImage.None        },
                { DialogIcon.Error,       MessageBoxImage.Error       },
                { DialogIcon.Warning,     MessageBoxImage.Warning     },
                { DialogIcon.Information, MessageBoxImage.Information },
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
        private static Dictionary<DialogButtons, MessageBoxButton> Buttons { get; } =
            new Dictionary<DialogButtons, MessageBoxButton>
            {
                { DialogButtons.Ok,          MessageBoxButton.OK          },
                { DialogButtons.OkCancel,    MessageBoxButton.OKCancel    },
                { DialogButtons.YesNo,       MessageBoxButton.YesNo       },
                { DialogButtons.YesNoCancel, MessageBoxButton.YesNoCancel },
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
        private static Dictionary<MessageBoxResult, DialogResult> Results { get; } =
            new Dictionary<MessageBoxResult, DialogResult>
            {
                { MessageBoxResult.OK,     DialogResult.Ok },
                { MessageBoxResult.Cancel, DialogResult.Cancel },
                { MessageBoxResult.Yes,    DialogResult.Yes },
                { MessageBoxResult.No,     DialogResult.No },
            };
    }
}
