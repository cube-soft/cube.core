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
namespace Cube.Xui;

using System.ComponentModel;
using System.Windows.Input;

#region IValue<T>

/* ------------------------------------------------------------------------- */
///
/// IValue(T)
///
/// <summary>
/// Provides interface of a ViewModel element that has a value.
/// </summary>
///
/* ------------------------------------------------------------------------- */
public interface IValue<T> : INotifyPropertyChanged
{
    /* --------------------------------------------------------------------- */
    ///
    /// Value
    ///
    /// <summary>
    /// Gets a value to be bound to the View.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    T Value { get; set; }
}

#endregion

#region IElement

/* ------------------------------------------------------------------------- */
///
/// IElement
///
/// <summary>
/// Provides interface of a ViewModel element that has text and command.
/// </summary>
///
/* ------------------------------------------------------------------------- */
public interface IElement : INotifyPropertyChanged
{
    /* --------------------------------------------------------------------- */
    ///
    /// Text
    ///
    /// <summary>
    /// Gets a text to be displayed in the View.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    string Text { get; }

    /* --------------------------------------------------------------------- */
    ///
    /// Command
    ///
    /// <summary>
    /// Gets or sets a command to be executed by the View.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    ICommand Command { get; set; }
}

#endregion

#region IElement<T>

/* ------------------------------------------------------------------------- */
///
/// IElement
///
/// <summary>
/// Provides interface of a ViewModel element that has text, command,
/// and value.
/// </summary>
///
/* ------------------------------------------------------------------------- */
public interface IElement<T> : IElement, IValue<T>;

#endregion

#region IListItem

/* ------------------------------------------------------------------------- */
///
/// IListItem
///
/// <summary>
/// Provides interface to bind to either the ListBoxItem or the
/// ListViewItem.
/// </summary>
///
/* ------------------------------------------------------------------------- */
public interface IListItem : INotifyPropertyChanged
{
    /* --------------------------------------------------------------------- */
    ///
    /// Selected
    ///
    /// <summary>
    /// Gets or sets a value indicating whether the item is selected.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    bool Selected { get; set; }
}

#endregion
