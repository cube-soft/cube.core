/* ------------------------------------------------------------------------- */
///
/// ApplicationSettings.cs
/// 
/// Copyright (c) 2010 CubeSoft, Inc.
/// 
/// This is distributed under the Microsoft Public License (Ms-PL).
/// See http://www.opensource.org/licenses/ms-pl.html
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
