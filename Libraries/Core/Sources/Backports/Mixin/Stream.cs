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
using System.IO;

namespace Cube.Backports
{
    /* --------------------------------------------------------------------- */
    ///
    /// StreamExtension
    ///
    /// <summary>
    /// Provides extended methods for the Stream class for compatibility
    /// with .NET Framework 3.5.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public static class StreamExtension
    {
        /* ----------------------------------------------------------------- */
        ///
        /// CopyTo
        ///
        /// <summary>
        /// Reads the bytes from the source stream and writes them to
        /// another stream.
        /// </summary>
        ///
        /// <param name="src">The source stream.</param>
        ///
        /// <param name="dest">
        /// The stream to which the contents of the source stream will be
        /// copied.
        /// </param>
        ///
        /* ----------------------------------------------------------------- */
        public static void CopyTo(this Stream src, Stream dest)
        {
            var buffer = new byte[16 * 1024];
            var result = 0;

            while ((result = src.Read(buffer, 0, buffer.Length)) > 0)
            {
                dest.Write(buffer, 0, result);
            }
        }
    }
}
