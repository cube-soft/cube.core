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
namespace Cube.Forms.Controls
{
    /* --------------------------------------------------------------------- */
    ///
    /// WebControlVersion
    ///
    /// <summary>
    /// Specifies the version of the WebControl rendering engine.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public enum WebControlVersion
    {
        /// <summary>IE7.</summary>
        IE7 = 7000,
        /// <summary>IE8 compatibility mode.</summary>
        IE8Quirks = 8000,
        /// <summary>IE8.</summary>
        IE8 = 8888,
        /// <summary>IE9 compatibility mode.</summary>
        IE9Quirks = 9000,
        /// <summary>IE9.</summary>
        IE9 = 9999,
        /// <summary>IE10 compatibility mode.</summary>
        IE10Quirks = 10000,
        /// <summary>IE10.</summary>
        IE10 = 10001,
        /// <summary>IE11 compatibility mode.</summary>
        IE11Quirks = 11000,
        /// <summary>IE11.</summary>
        IE11 = 11001,
        /// <summary>Latest available version.</summary>
        Latest = -1,
    }
}
