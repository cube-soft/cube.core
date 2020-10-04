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
using System.Reflection;

namespace Cube.Forms.Demo
{
    /* --------------------------------------------------------------------- */
    ///
    /// MainViewModel
    ///
    /// <summary>
    /// Represents the ViewModel for the main window.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class MainViewModel : Presentable<Assembly>
    {
        #region Constructors

        /* --------------------------------------------------------------------- */
        ///
        /// MainViewModel
        ///
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        public MainViewModel() : base(Assembly.GetExecutingAssembly()) { }

        #endregion

        #region Methods

        /* --------------------------------------------------------------------- */
        ///
        /// Notice
        ///
        /// <summary>
        /// Invokes the command to show a notice dialog.
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        public void Notice() => Send(MessageFactory.CreateForNotice(Facade));

        /* --------------------------------------------------------------------- */
        ///
        /// About
        ///
        /// <summary>
        /// Invokes the command to show a version dialog.
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        public void About() => Send(new AboutMessage(Facade));

        #endregion
    }
}
