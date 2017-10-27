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
using System.ComponentModel;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Cube
{
    /* --------------------------------------------------------------------- */
    ///
    /// ObservableProperty
    /// 
    /// <summary>
    /// INotifyPropertyChanged の汎用的な実装です。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [DataContract]
    public class ObservableProperty : INotifyPropertyChanged
    {
        #region Constructor

        /* ----------------------------------------------------------------- */
        ///
        /// ObservableProperty
        /// 
        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        /// 
        /// <remarks>
        /// このクラスを直接オブジェクト化する事はできません。継承クラスを
        /// 使用して下さい。
        /// </remarks>
        ///
        /* ----------------------------------------------------------------- */
        protected ObservableProperty() { }

        #endregion

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

        /* ----------------------------------------------------------------- */
        ///
        /// OnPropertyChanged
        /// 
        /// <summary>
        /// PropertyChanged イベントを発生させます。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
            => PropertyChanged?.Invoke(this, e);

        #endregion

        #region Methods

        /* ----------------------------------------------------------------- */
        ///
        /// SetProperty
        /// 
        /// <summary>
        /// プロパティに値を設定します。
        /// </summary>
        /// 
        /// <param name="field">設定先の参照</param>
        /// <param name="value">設定値</param>
        /// <param name="name">設定するプロパティの名前</param>
        /// 
        /// <returns>設定したかどうかを示す値</returns>
        ///
        /* ----------------------------------------------------------------- */
        protected bool SetProperty<T>(ref T field, T value,
            [CallerMemberName] string name = null) =>
            SetProperty(ref field, value, EqualityComparer<T>.Default, name);

        /* ----------------------------------------------------------------- */
        ///
        /// SetProperty
        /// 
        /// <summary>
        /// プロパティに値を設定します。
        /// </summary>
        ///
        /// <param name="field">設定先の参照</param>
        /// <param name="value">設定値</param>
        /// <param name="func">比較用オブジェクト</param>
        /// <param name="name">設定するプロパティの名前</param>
        /// 
        /// <returns>設定したかどうかを示す値</returns>
        ///
        /* ----------------------------------------------------------------- */
        protected bool SetProperty<T>(ref T field, T value,
            IEqualityComparer<T> func, [CallerMemberName] string name = null)
        {
            if (func.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(new PropertyChangedEventArgs(name));
            return true;
        }

        #endregion
    }
}
