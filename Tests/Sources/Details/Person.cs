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
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Cube.FileSystem.Tests
{
    /* ----------------------------------------------------------------- */
    ///
    /// Sex
    ///
    /// <summary>
    /// Specifies the sex.
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
    /// Represents the address.
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
        /// Gets or sets the type of the provided address.
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
        /// Gets or sets the content of the provided address.
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
    /// Represents the example class that is serializable.
    /// </summary>
    ///
    /* ----------------------------------------------------------------- */
    [DataContract]
    internal class Person : SerializableBase
    {
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// Person
        ///
        /// <summary>
        /// Initializes a new instance of the Persion class.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public Person() { Reset(); }

        #endregion

        #region Properties

        /* ----------------------------------------------------------------- */
        ///
        /// Identification
        ///
        /// <summary>
        /// Gets or sets the ID.
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
        /// Gets or sets the name.
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
        /// Gets or sets the sex.
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
        /// Gets or sets the age.
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
        /// Gets or sets the creation time.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [DataMember]
        public DateTime? Creation
        {
            get => _creation;
            set => SetProperty(ref _creation, value);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Contact
        ///
        /// <summary>
        /// Gets or sets the address to contact.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [DataMember]
        public Address Contact
        {
            get => _contact;
            set => SetProperty(ref _contact, value);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Others
        ///
        /// <summary>
        /// Gets or sets the other addresses.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [DataMember]
        public IList<Address> Others
        {
            get => _others;
            set => SetProperty(ref _others, value);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Messages
        ///
        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [DataMember]
        public string[] Messages
        {
            get => _messages;
            set => SetProperty(ref _messages, value);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Reserved
        ///
        /// <summary>
        /// Gets or sets the reserved value.
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
        /// Gets or sets the secret value that is not serialized.
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
        /// Gets the GUID of the object.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public Guid Guid { get; } = Guid.NewGuid();

        #endregion

        #region Implementations

        /* ----------------------------------------------------------------- */
        ///
        /// Reset
        ///
        /// <summary>
        /// Resets values.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void Reset()
        {
            _identification = -1;
            _name           = string.Empty;
            _sex            = Sex.Unknown;
            _age            = 0;
            _creation       = DateTime.MinValue;
            _contact        = new Address { Type = "Phone", Value = string.Empty };
            _others         = new List<Address>();
            _messages       = new string[0];
            _reserved       = false;
            _secret         = "secret message";
        }

        #endregion

        #region Fields
        private int _identification;
        private string _name;
        private Sex _sex;
        private int _age;
        private DateTime? _creation;
        private Address _contact;
        private IList<Address> _others;
        private string[] _messages;
        private bool _reserved;
        private string _secret;
        #endregion
    }
}