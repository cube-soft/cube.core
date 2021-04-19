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
using System.Reflection;
using Cube.Mixin.Assembly;

namespace Cube.Forms.Demo
{
    /* --------------------------------------------------------------------- */
    ///
    /// MessageFactory
    ///
    /// <summary>
    /// Provides functionality to create messages.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public static class MessageFactory
    {
        #region Methods

        /* ----------------------------------------------------------------- */
        ///
        /// CreateForNotice
        ///
        /// <summary>
        /// Creates a new instance of the NoticeMessage class.
        /// </summary>
        ///
        /// <param name="src">Assembly information.</param>
        /// <param name="callback">
        /// Callback action called when a part of the notice window is
        /// clicked.
        /// </param>
        ///
        /* ----------------------------------------------------------------- */
        public static NoticeMessage CreateForNotice(Assembly src, NoticeCallback callback) => new()
        {
            Title        = src.GetTitle(),
            Text         = $"{Properties.Resources.NoticeSample} ({++_notice})",
            Value        = "DummyData",
            DisplayTime  = TimeSpan.FromSeconds(60),
            InitialDelay = TimeSpan.FromSeconds(1),
            Priority     = NoticePriority.Normal,
            Location     = (NoticeLocation)(_notice % Enum.GetValues(typeof(NoticeLocation)).Length),
            Callback     = callback,
        };

        #endregion

        #region Fields
        private static int _notice = 0;
        #endregion
    }
}
