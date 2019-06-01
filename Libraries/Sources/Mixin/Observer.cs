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
using Cube.Xui;
using System.ComponentModel;

namespace Cube.Mixin.Observer
{
    /* --------------------------------------------------------------------- */
    ///
    /// Extension
    ///
    /// <summary>
    /// Provides extende methods of IObserver implemented classes.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public static class Extension
    {
        /* ----------------------------------------------------------------- */
        ///
        /// Associate
        ///
        /// <summary>
        /// Associates the specified observer and specified object.
        /// </summary>
        ///
        /// <param name="src">Observer object.</param>
        /// <param name="target">Object to be observed.</param>
        /// <param name="names">Property names.</param>
        ///
        /* ----------------------------------------------------------------- */
        public static T Associate<T>(this T src, INotifyPropertyChanged target, params string[] names)
            where T : IObserver
        {
            src.Observe(target, names);
            return src;
        }
    }
}
