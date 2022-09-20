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
namespace Cube.FileSystem.Tests;

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Cube.DataContract;

#region Person

/* ------------------------------------------------------------------------- */
///
/// Dummy
///
/// <summary>
/// Represents the serializable dummy data.
/// </summary>
///
/* ------------------------------------------------------------------------- */
[DataContract]
class Dummy : SerializableBase
{
    #region Properties

    /* --------------------------------------------------------------------- */
    ///
    /// Number
    ///
    /// <summary>
    /// Gets or sets the ID number.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [DataMember(Name = "ID")]
    public int Number
    {
        get => Get(() => -1);
        set => Set(value);
    }

    /* --------------------------------------------------------------------- */
    ///
    /// Name
    ///
    /// <summary>
    /// Gets or sets the name.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [DataMember]
    public string Name
    {
        get => Get(() => string.Empty);
        set => Set(value);
    }

    /* --------------------------------------------------------------------- */
    ///
    /// Sex
    ///
    /// <summary>
    /// Gets or sets the sex.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [DataMember]
    public Sex Sex
    {
        get => Get(() => Sex.Unknown);
        set => Set(value);
    }

    /* --------------------------------------------------------------------- */
    ///
    /// Age
    ///
    /// <summary>
    /// Gets or sets the age.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [DataMember]
    public int Age
    {
        get => Get(() => -1);
        set => Set(value);
    }

    /* --------------------------------------------------------------------- */
    ///
    /// Creation
    ///
    /// <summary>
    /// Gets or sets the creation time.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [DataMember]
    public DateTime? Creation
    {
        get => Get<DateTime?>();
        set => Set(value);
    }

    /* --------------------------------------------------------------------- */
    ///
    /// Contact
    ///
    /// <summary>
    /// Gets or sets the address to contact.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [DataMember]
    public Address Contact
    {
        get => Get(() => new Address { Type = "Phone", Value = string.Empty });
        set => Set(value);
    }

    /* --------------------------------------------------------------------- */
    ///
    /// Others
    ///
    /// <summary>
    /// Gets or sets the other addresses.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [DataMember]
    public IList<Address> Others
    {
        get => Get(() => new List<Address>());
        set => Set(value);
    }

    /* --------------------------------------------------------------------- */
    ///
    /// Messages
    ///
    /// <summary>
    /// Gets or sets the message.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [DataMember]
    public string[] Messages
    {
        get => Get(() => new string[0]);
        set => Set(value);
    }

    /* --------------------------------------------------------------------- */
    ///
    /// Reserved
    ///
    /// <summary>
    /// Gets or sets the reserved value.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [DataMember]
    public bool Reserved
    {
        get => _reserved;
        set => Set(ref _reserved, value);
    }

    /* --------------------------------------------------------------------- */
    ///
    /// Secret
    ///
    /// <summary>
    /// Gets or sets the non-datamember value.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public string Secret
    {
        get => Get(() => "secret message");
        set => Set(value);
    }

    #endregion

    #region Fields
    private bool _reserved;
    #endregion
}

#endregion

#region Sex

/* ------------------------------------------------------------------------- */
///
/// Sex
///
/// <summary>
/// Specifies the sex.
/// </summary>
///
/* ------------------------------------------------------------------------- */
enum Sex
{
    Male    =  0,
    Female  =  1,
    Unknown = -1
}

#endregion

#region Address

/* ------------------------------------------------------------------------- */
///
/// Address
///
/// <summary>
/// Represents the address.
/// </summary>
///
/* ------------------------------------------------------------------------- */
[DataContract]
class Address
{
    /* --------------------------------------------------------------------- */
    ///
    /// Type
    ///
    /// <summary>
    /// Gets or sets the type of the provided address.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [DataMember]
    public string Type { get; set; }

    /* --------------------------------------------------------------------- */
    ///
    /// Value
    ///
    /// <summary>
    /// Gets or sets the content of the provided address.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [DataMember]
    public string Value { get; set; }
}

#endregion
