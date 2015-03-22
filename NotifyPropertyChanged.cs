/* ------------------------------------------------------------------------- */
///
/// NotifyPropertyChanged.cs
/// 
/// Copyright (c) 2010 CubeSoft, Inc.
/// 
/// This is distributed under the Microsoft Public License (Ms-PL).
/// See http://www.opensource.org/licenses/ms-pl.html
///
/* ------------------------------------------------------------------------- */
using System;
using System.ComponentModel;

namespace Cube
{
    /* --------------------------------------------------------------------- */
    ///
    /// Cube.NotifyPropertyChanged
    /// 
    /// <summary>
    /// INotifyPropertyChanged の実装クラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class NotifyPropertyChanged : INotifyPropertyChanged
    {
        #region Events

        /* ----------------------------------------------------------------- */
        ///
        /// PropertyChanged
        /// 
        /// <summary>
        /// プロパティが変更された時に発生するイベントです。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Virtual methods

        /* ----------------------------------------------------------------- */
        ///
        /// OnPropertyChanged
        /// 
        /// <summary>
        /// プロパティが変更された時に発生するイベントです。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null) PropertyChanged(this, e);
        }

        #endregion
    }
}
