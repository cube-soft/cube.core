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
using System;
using System.Runtime.Serialization;

namespace Cube.Tests
{
    /* ----------------------------------------------------------------- */
    ///
    /// Sex
    /// 
    /// <summary>
    /// 性別を表す列挙体です。
    /// </summary>
    ///
    /* ----------------------------------------------------------------- */
    internal enum Sex : int
    {
        Male    =  0,
        Female  =  1,
        Unknown = -1
    }

    /* ----------------------------------------------------------------- */
    ///
    /// Address
    /// 
    /// <summary>
    /// アドレスを保持するためのクラスです。
    /// </summary>
    ///
    /* ----------------------------------------------------------------- */
    [DataContract]
    internal class Address
    {
        /* ----------------------------------------------------------------- */
        ///
        /// Type
        /// 
        /// <summary>
        /// アドレスの種類を取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [DataMember]
        public string Type { get; set; }

        /* ----------------------------------------------------------------- */
        ///
        /// Value
        /// 
        /// <summary>
        /// アドレスの内容を取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [DataMember]
        public string Value { get; set; }
    }

    /* ----------------------------------------------------------------- */
    ///
    /// Person
    /// 
    /// <summary>
    /// 個人情報を保持するためのクラスです。
    /// </summary>
    ///
    /* ----------------------------------------------------------------- */
    [DataContract]
    internal class Person : Cube.ObservableProperty
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
        [DataMember(Name = "ID")]
        public int Identification
        {
            get => _identification;
            set => SetProperty(ref _identification, value);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Name
        /// 
        /// <summary>
        /// 名前を取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [DataMember]
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Sex
        /// 
        /// <summary>
        /// 性別を取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [DataMember]
        public Sex Sex
        {
            get => _sex;
            set => SetProperty(ref _sex, value);
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
        [DataMember]
        public int Age
        {
            get => _age;
            set => SetProperty(ref _age, value);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Creation
        /// 
        /// <summary>
        /// 作成日時を取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [DataMember]
        public DateTime Creation
        {
            get => _creation;
            set => SetProperty(ref _creation, value);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Phone
        /// 
        /// <summary>
        /// 電話番号を取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [DataMember]
        public Address Phone
        {
            get => _phone;
            set => SetProperty(ref _phone, value);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Email
        /// 
        /// <summary>
        /// Email アドレスを取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [DataMember]
        public Address Email
        {
            get => _email;
            set => SetProperty(ref _email, value);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Reserved
        /// 
        /// <summary>
        /// フラグを取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [DataMember]
        public bool Reserved
        {
            get => _reserved;
            set => SetProperty(ref _reserved, value);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Secret
        /// 
        /// <summary>
        /// 秘密のメモ用データを取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string Secret
        {
            get => _secret;
            set => SetProperty(ref _secret, value);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Guid
        /// 
        /// <summary>
        /// Guid オブジェクトを取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public Guid Guid { get; } = Guid.NewGuid();

        #region Fields
        private int _identification = -1;
        private string _name = string.Empty;
        private Sex _sex = Sex.Unknown;
        private int _age = 0;
        private DateTime _creation = DateTime.MinValue;
        private Address _phone = new Address { Type = "Phone", Value = string.Empty };
        private Address _email = new Address { Type = "Email", Value = string.Empty };
        private bool _reserved = false;
        private string _secret = "secret message";
        #endregion
    }
}