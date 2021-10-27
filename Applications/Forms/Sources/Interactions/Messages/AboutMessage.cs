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
    /// AboutMessage
    ///
    /// <summary>
    /// Represents the message to show a version dialog.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class AboutMessage : Message<Assembly>
    {
        /* --------------------------------------------------------------------- */
        ///
        /// AboutMessage
        ///
        /// <summary>
        /// Initializes a new instance of the AboutMessage class with the
        /// specified arguments.
        /// </summary>
        ///
        /// <param name="src">Assembly information.</param>
        ///
        /* --------------------------------------------------------------------- */
        public AboutMessage(Assembly src) { Value = src; }
    }
}
