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

namespace Cube.Forms.Bindings
{
    /* --------------------------------------------------------------------- */
    ///
    /// BindingExtension
    ///
    /// <summary>
    /// Binding 関連の拡張用クラスです。
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
        /// INotifyCollectionChanged を実装するコレクションを
        /// BindingSource オブジェクトに変換します。
        /// </summary>
        ///
        /// <remarks>
        /// CollectionChanged イベント発生時に ResetBindings(false) を
        /// 実行します。
        /// </remarks>
        ///
        /// <param name="src">コレクションオブジェクト</param>
        ///
        /* ----------------------------------------------------------------- */
        public static BindingSource ToBindingSource(this INotifyCollectionChanged src) =>
            ToBindingSource(src, SynchronizationContext.Current);

        /* ----------------------------------------------------------------- */
        ///
        /// ToBindingSource
        ///
        /// <summary>
        /// INotifyCollectionChanged を実装するコレクションを
        /// BindingSource オブジェクトに変換します。
        /// </summary>
        ///
        /// <remarks>
        /// CollectionChanged イベント発生時に ResetBindings(false) を
        /// 実行します。
        /// </remarks>
        ///
        /// <param name="src">コレクションオブジェクト</param>
        /// <param name="ctx">UI スレッドを表すコンテキスト</param>
        ///
        /* ----------------------------------------------------------------- */
        public static BindingSource ToBindingSource(this INotifyCollectionChanged src,
            SynchronizationContext ctx)
        {
            var dest = new BindingSource { DataSource = src };
            src.CollectionChanged += (s, e) => ctx.Send(_ => dest.ResetBindings(false), null);
            return dest;
        }

        #endregion
    }
}
