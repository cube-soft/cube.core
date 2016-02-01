/* ------------------------------------------------------------------------- */
///
/// IReadOnlyCollection.cs
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
namespace System.Collections.Generic
{
    /* --------------------------------------------------------------------- */
    ///
    /// System.Collections.Generic.IReadOnlyCollection
    /// 
    /// <summary>
    /// 要素の読み取り専用のコレクションを表します。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public interface IReadOnlyCollection<T> : IEnumerable<T>, IEnumerable
    {
        /* ----------------------------------------------------------------- */
        ///
        /// Count
        /// 
        /// <summary>
        /// コレクション内の要素の数を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        int Count { get;  }
    }
}
