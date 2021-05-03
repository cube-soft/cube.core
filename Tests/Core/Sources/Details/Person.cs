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

namespace Cube.Tests
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
    internal enum Sex
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
            get => Get<int>();
            set => Set(value);
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
            get => Get<string>();
            set => Set(value);
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
            get => Get<Sex>();
            set => Set(value);
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
            get => _age.Get();
            set { if (_age.Set(value)) Refresh(nameof(Age)); }
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
            get => Get<DateTime?>();
            set => Set(value);
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
            get => Get<Address>();
            set => Set(value);
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
            set => Set(ref _others, value);
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
            get => Get<string[]>();
            set => Set(value);
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
            set => Set(ref _reserved, value);
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
            set => Set(ref _secret, value);
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

        #region Methods

        /* ----------------------------------------------------------------- */
        ///
        /// CreateDummy
        ///
        /// <summary>
        /// Creates a dummy object to test.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public static Person CreateDummy() => new Person
        {
            Identification = 123,
            Name           = "山田花子",
            Sex            = Sex.Female,
            Age            = 15,
            Creation       = new DateTime(2014, 12, 31, 23, 25, 30),
            Contact        = new Address { Type = "Phone", Value = "080-9876-5432" },
            Reserved       = true,
            Secret         = "dummy data",
            Others         = new List<Address>
            {
                new Address { Type = "PC",     Value = "pc@example.com" },
                new Address { Type = "Mobile", Value = "mobile@example.com" }
            },
            Messages       = new[]
            {
                "1st message",
                "2nd message",
                "3rd message",
            }
        };

        #endregion

        #region Implementations

        /* ----------------------------------------------------------------- */
        ///
        /// OnDeserializing
        ///
        /// <summary>
        /// Occurs before deserializing.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [OnDeserializing]
        private void OnDeserializing(StreamingContext context) => Reset();

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
            Identification = -1;
            Name           = string.Empty;
            Creation       = DateTime.MinValue;
            Contact        = new Address { Type = "Phone", Value = string.Empty };
            Sex            = Sex.Unknown;
            Messages       = new string[0];

            _age      = new Accessor<int>(0);
            _others   = new List<Address>();
            _reserved = false;
            _secret   = "secret message";
        }

        #endregion

        #region Fields
        private Accessor<int> _age;
        private IList<Address> _others;
        private bool _reserved;
        private string _secret;
        #endregion
    }
}