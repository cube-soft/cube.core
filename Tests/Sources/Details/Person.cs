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
namespace Cube.Xui.Tests
{
    /* ----------------------------------------------------------------- */
    ///
    /// Person
    ///
    /// <summary>
    /// テスト用のクラスです。
    /// </summary>
    ///
    /* ----------------------------------------------------------------- */
    class Person : ObservableProperty
    {
        #region Properties

        /* ----------------------------------------------------------------- */
        ///
        /// Name
        ///
        /// <summary>
        /// 名前を取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Age
        ///
        /// <summary>
        /// 年齢を取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public int Age
        {
            get => _age;
            set => SetProperty(ref _age, value);
        }

        #endregion

        #region Fields
        private string _name;
        private int _age;
        #endregion
    }
}
