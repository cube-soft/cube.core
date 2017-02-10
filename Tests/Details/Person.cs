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
        Male = 0,
        Female = 1,
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
        public int Identification { get; set; } = -1;

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
        public string Name { get; set; } = string.Empty;

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
        public Sex Sex { get; set; } = Sex.Unknown;

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
        public int Age { get; set; } = 0;

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
        public DateTime Creation { get; set; } = DateTime.MinValue;

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
        public Address Phone { get; set; } = new Address { Type = "Phone", Value = string.Empty };

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
        public Address Email { get; set; } = new Address { Type = "Email", Value = string.Empty };

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
        public bool Reserved { get; set; } = false;

        /* ----------------------------------------------------------------- */
        ///
        /// Secret
        /// 
        /// <summary>
        /// 秘密のメモ用データを取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string Secret { get; set; } = "secret message";

        #endregion
    }
}