﻿/* ------------------------------------------------------------------------- */
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
using System.Windows.Forms;
using Cube.Mixin.String;

namespace Cube.Forms.Behaviors
{
    /* --------------------------------------------------------------------- */
    ///
    /// OpenDirectoryBehavior
    ///
    /// <summary>
    /// Provides functionality to show a directory dialog.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class OpenDirectoryBehavior : MessageBehavior<OpenDirectoryMessage>
    {
        /* ----------------------------------------------------------------- */
        ///
        /// OpenDirectoryBehavior
        ///
        /// <summary>
        /// Initializes a new instance of the OpenDirectoryBehavior class
        /// with the specified presentable object.
        /// </summary>
        ///
        /// <param name="aggregator">Presentable object.</param>
        ///
        /* ----------------------------------------------------------------- */
        public OpenDirectoryBehavior(IAggregator aggregator) : base(aggregator, e =>
        {
            var view = new FolderBrowserDialog { ShowNewFolderButton = e.NewButton };

            if (e.Text.HasValue()) view.Description = e.Text;
            if (e.Value.HasValue()) view.SelectedPath = e.Value;

            e.Cancel = view.ShowDialog() != DialogResult.OK;
            if (!e.Cancel) e.Value = view.SelectedPath;
        }) { }
    }
}
