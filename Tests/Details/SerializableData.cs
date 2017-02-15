/* ------------------------------------------------------------------------- */
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
using System;

namespace Cube.Tests
{
    /* ----------------------------------------------------------------- */
    ///
    /// SerializableData
    /// 
    /// <summary>
    /// Serializabl 属性を持つクラスのサンプルです。
    /// </summary>
    ///
    /* ----------------------------------------------------------------- */
    [Serializable]
    internal class SerializableData
    {
        /* ----------------------------------------------------------------- */
        ///
        /// Identification
        /// 
        /// <summary>
        /// ID を取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public int Identification { get; set; } = -1;

        /* ----------------------------------------------------------------- */
        ///
        /// Name
        /// 
        /// <summary>
        /// 名前を取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string Name { get; set; } = string.Empty;

        /* ----------------------------------------------------------------- */
        ///
        /// Sex
        /// 
        /// <summary>
        /// 性別を取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public Sex Sex { get; set; } = Sex.Unknown;

        /* ----------------------------------------------------------------- */
        ///
        /// Creation
        /// 
        /// <summary>
        /// 作成日時を取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public DateTime Creation { get; set; } = DateTime.MinValue;

        /* ----------------------------------------------------------------- */
        ///
        /// Reserved
        /// 
        /// <summary>
        /// フラグを取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public bool Reserved { get; set; } = false;
    }
}
