/* ------------------------------------------------------------------------- */
///
/// RegistryTester.cs
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
        Male   = 0,
        Female = 1
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
        #region Properties

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
        public string Type
        {
            get { return _type; }
            set { _type = value; }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Data
        /// 
        /// <summary>
        /// アドレスのデータを取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [DataMember]
        public string Data
        {
            get { return _data; }
            set { _data = value; }
        }

        #endregion

        #region Fields
        private string _type = "Unknown";
        private string _data = "Unknown data";
        #endregion
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
    internal class Person
    {
        #region Properties

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
            get { return _id; }
            set { _id = value; }
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
            get { return _name; }
            set { _name = value; }
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
            get { return _sex; }
            set { _sex = value; }
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
            get { return _age; }
            set { _age = value; }
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
            get { return _creation; }
            set { _creation = value; }
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
            get { return _phone; }
            set { _phone = value; }
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
            get { return _email; }
            set { _email = value; }
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
            get { return _reserved; }
            set { _reserved = value; }
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
            get { return _secret; }
            set { _secret = value; }
        }

        #endregion

        #region Fields
        private int _id = -1;
        private string _name = "Personal name";
        private int _age = -1;
        private Sex _sex = Sex.Female;
        private DateTime _creation = DateTime.MinValue;
        private Address _phone = new Address();
        private Address _email = new Address();
        private bool _reserved = false;
        private string _secret = "secret message";
        #endregion
    }
}
