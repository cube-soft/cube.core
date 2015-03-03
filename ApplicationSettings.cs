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
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// ApplicationSettings
        ///
        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public ApplicationSettings(string product, string publisher = "CubeSoft")
        {
            Product = product;
            Publisher = publisher;
        }

        #endregion

        #region Properties

        /* ----------------------------------------------------------------- */
        ///
        /// Publisher
        ///
        /// <summary>
        /// アプリケーションの発行元を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string Publisher
        {
            get { return _publisher; }
            private set { _publisher = value; }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Product
        ///
        /// <summary>
        /// アプリケーション名を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string Product
        {
            get { return _product; }
            private set { _product = value; }
        }

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

        #endregion

        #region Methods

        /* ----------------------------------------------------------------- */
        ///
        /// Load
        ///
        /// <summary>
        /// アプリケーション固有の設定を読み込みます。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void Load()
        {
            var root = string.Format(@"Software\{0}\{1}", Publisher, Product);
            using (var subkey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(root))
            {
                Settings.Load(root, this);
            }
        }

        #endregion

        #region Fields
        private string _publisher = string.Empty;
        private string _product = string.Empty;
        private string _root = string.Empty;
        private string _version = string.Empty;
        #endregion
    }
}
