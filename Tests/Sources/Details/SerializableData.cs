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

namespace Cube.Tests
{
    /* ----------------------------------------------------------------- */
    ///
    /// SerializableData
    ///
    /// <summary>
    /// Represents the example class that has the Serializable attribute.
    /// </summary>
    ///
    /* ----------------------------------------------------------------- */
    [Serializable]
    internal class SerializableData
    {
        #region Properties

        /* ----------------------------------------------------------------- */
        ///
        /// Identification
        ///
        /// <summary>
        /// Gets or sets the ID.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public int Identification { get; set; } = -1;

        /* ----------------------------------------------------------------- */
        ///
        /// Name
        ///
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string Name { get; set; } = string.Empty;

        /* ----------------------------------------------------------------- */
        ///
        /// Sex
        ///
        /// <summary>
        /// Gets or sets the sex.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public Sex Sex { get; set; } = Sex.Unknown;

        /* ----------------------------------------------------------------- */
        ///
        /// Creation
        ///
        /// <summary>
        /// Gets or sets the creation time.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public DateTime? Creation { get; set; }

        /* ----------------------------------------------------------------- */
        ///
        /// Reserved
        ///
        /// <summary>
        /// Gets or sets the reserved value.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public bool Reserved { get; set; } = false;

        #endregion
    }
}
