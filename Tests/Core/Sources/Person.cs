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
namespace Cube.Tests;

using System;

#region Person

/* ------------------------------------------------------------------------- */
///
/// Person
///
/// <summary>
/// Represents the example class that is serializable.
/// </summary>
///
/* ------------------------------------------------------------------------- */
class Person : ObservableBase
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
    public int Age
    {
        get => _age.Get();
        set { if (_age.Set(value)) Refresh(nameof(Age)); }
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
    public Address Contact
    {
        get => Get(() => new Address { Type = "Phone", Value = string.Empty });
        set => Set(value);
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
        get => _secret;
        set => Set(ref _secret, value);
    }

    /* --------------------------------------------------------------------- */
    ///
    /// Guid
    ///
    /// <summary>
    /// Gets the GUID of the object.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public Guid Guid { get; } = Guid.NewGuid();

    #endregion

    #region Methods

    /* --------------------------------------------------------------------- */
    ///
    /// Dispose
    ///
    /// <summary>
    /// Releases the unmanaged resources used by the object and
    /// optionally releases the managed resources.
    /// </summary>
    ///
    /// <param name="disposing">
    /// true to release both managed and unmanaged resources;
    /// false to release only unmanaged resources.
    /// </param>
    ///
    /* --------------------------------------------------------------------- */
    protected override void Dispose(bool disposing) { }

    #endregion

    #region Fields
    private readonly Accessor<int> _age = new(0);
    private string _secret;
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
    public string Value { get; set; }
}

#endregion
