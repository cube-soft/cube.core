/* ------------------------------------------------------------------------- */
///
/// SelectedIndices.cs
/// 
/// Copyright (c) 2010 CubeSoft, Inc.
/// 
/// Licensed under the Apache License, Version 2.0 (the "License");
/// you may not use this file except in compliance with the License.
/// You may obtain a copy of the License at
///
///  http://www.apache.org/licenses/LICENSE-2.0
///
/// Unless required by applicable law or agreed to in writing, software
/// distributed under the License is distributed on an "AS IS" BASIS,
/// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
/// See the License for the specific language governing permissions and
/// limitations under the License.
///
/* ------------------------------------------------------------------------- */
using System.Collections.Generic;
using System.Linq;

namespace Cube.Forms.Controls
{
    /* --------------------------------------------------------------------- */
    ///
    /// SelectedIndices
    /// 
    /// <summary>
    /// System.Windows.Forms.ListView.SelectedIndexCollection の
    /// 拡張メソッドを定義したクラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public static class SelectedIndices
    {
        /* ----------------------------------------------------------------- */
        ///
        /// Ascend
        /// 
        /// <summary>
        /// 昇順にソートします。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public static IEnumerable<int> Ascend(
            this System.Windows.Forms.ListView.SelectedIndexCollection indices)
            => indices.Cast<int>().OrderBy(x => x);

        /* ----------------------------------------------------------------- */
        ///
        /// Descend
        /// 
        /// <summary>
        /// 降順にソートします。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public static IEnumerable<int> Descend(
            this System.Windows.Forms.ListView.SelectedIndexCollection indices)
            => indices.Cast<int>().OrderByDescending(x => x);
    }
}
