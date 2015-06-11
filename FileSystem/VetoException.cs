/* ------------------------------------------------------------------------- */
///
/// VetoException.cs
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

namespace Cube.FileSystem
{
    /* --------------------------------------------------------------------- */
    ///
    /// Cube.FileSystem.VetoException
    /// 
    /// <summary>
    /// PnP 操作が拒否された時に送出される例外クラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class VetoException : Exception
    {
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// VetoException
        /// 
        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public VetoException(VetoType reason, string name)
            : this(reason, name, string.Empty, null) { }

        /* ----------------------------------------------------------------- */
        ///
        /// VetoException
        /// 
        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public VetoException(VetoType reason, string name, string message)
            : this(reason, name, message, null) { }

        /* ----------------------------------------------------------------- */
        ///
        /// VetoException
        /// 
        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public VetoException(VetoType reason, string name, string message, Exception inner)
            : base(message, inner)
        {
            Reason = reason;
            Name = name;
        }

        #endregion

        #region Properties

        /* ----------------------------------------------------------------- */
        ///
        /// Reason
        /// 
        /// <summary>
        /// 拒否された理由を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public VetoType Reason { get; private set; }

        /* ----------------------------------------------------------------- */
        ///
        /// Name
        /// 
        /// <summary>
        /// 名前を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string Name { get; private set; }

        #endregion
    }
}
