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
using System.Collections.Specialized;
using System.Threading;
using System.Windows.Forms;

namespace Cube.Mixin.Forms
{
    /* --------------------------------------------------------------------- */
    ///
    /// BindingExtension
    ///
    /// <summary>
    /// Provides extended methods about Binding functions.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public static class BindingExtension
    {
        #region Methods

        /* ----------------------------------------------------------------- */
        ///
        /// ToBindingSource
        ///
        /// <summary>
        /// Converts the specified INotifyCollectionChanged implementation
        /// object to a BindingSource object.
        /// </summary>
        ///
        /// <param name="src">
        /// INotifyCollectionChanged implementation object.
        /// </param>
        ///
        /// <returns>BindingSource object.</returns>
        ///
        /// <remarks>
        /// Invokes the ResetBindings(false) method when CollectionChanged
        /// event is fired.
        /// </remarks>
        ///
        /* ----------------------------------------------------------------- */
        public static BindingSource ToBindingSource(this INotifyCollectionChanged src) =>
            ToBindingSource(src, SynchronizationContext.Current);

        /* ----------------------------------------------------------------- */
        ///
        /// ToBindingSource
        ///
        /// <summary>
        /// Converts the specified INotifyCollectionChanged implementation
        /// object to a BindingSource object.
        /// </summary>
        ///
        /// <param name="src">
        /// INotifyCollectionChanged implementation object.
        /// </param>
        ///
        /// <param name="context">Synchronization context.</param>
        ///
        /// <returns>BindingSource object.</returns>
        ///
        /// <remarks>
        /// Invokes the ResetBindings(false) method when CollectionChanged
        /// event is fired.
        /// </remarks>
        ///
        /* ----------------------------------------------------------------- */
        public static BindingSource ToBindingSource(this INotifyCollectionChanged src,
            SynchronizationContext context)
        {
            var dest = new BindingSource { DataSource = src };
            src.CollectionChanged += (s, e) => context.Send(_ => dest.ResetBindings(false), null);
            return dest;
        }

        #endregion
    }
}
