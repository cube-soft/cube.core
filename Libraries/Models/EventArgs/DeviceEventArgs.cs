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

namespace Cube.Forms
{
    /* --------------------------------------------------------------------- */
    ///
    /// DeviceEventArgs
    ///
    /// <summary>
    /// デバイスの着脱が発生した時の情報を保持するためのクラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class DeviceEventArgs : EventArgs
    {
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// DeviceEventArgs
        ///
        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public DeviceEventArgs(char letter, DeviceType type)
            : base()
        {
            Letter = letter;
            Type = type;
        }

        #endregion

        #region Properties

        /* ----------------------------------------------------------------- */
        ///
        /// Letter
        ///
        /// <summary>
        /// ドライブレターを取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public char Letter { get; }

        /* ----------------------------------------------------------------- */
        ///
        /// Type
        ///
        /// <summary>
        /// 対象となるドライブの種類を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public DeviceType Type { get; }

        #endregion
    }

    /* --------------------------------------------------------------------- */
    ///
    /// DeviceType
    ///
    /// <summary>
    /// デバイスの種類を表す列挙型です。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public enum DeviceType : uint
    {
        Drive   = 0, // physical device or drive
        Media   = 1, // media in drive
        Network = 2, // network volume
    }
}
