/* ------------------------------------------------------------------------- */
///
/// ApplicationSettings.cs
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
using System.Runtime.Serialization;

namespace Cube
{
    /* --------------------------------------------------------------------- */
    ///
    /// Cube.ApplicationSettings
    /// 
    /// <summary>
    /// アプリケーション固有の設定を保持するためのクラスです。
    /// </summary>
    /// 
    /* --------------------------------------------------------------------- */
    [DataContract]
    public class ApplicationSettings
    {
        /* ----------------------------------------------------------------- */
        ///
        /// InstallDirectory
        ///
        /// <summary>
        /// アプリケーションがインストールされているディレクトリへの
        /// パスを取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [DataMember]
        public string InstallDirectory
        {
            get { return _root; }
            set { _root = value; }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Version
        ///
        /// <summary>
        /// アプリケーションのバージョン番号を表す文字列を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [DataMember]
        public string Version
        {
            get { return _version; }
            set { _version = value; }
        }

        #region Fields

        private string _root = string.Empty;
        private string _version = string.Empty;

        #endregion
    }
}
