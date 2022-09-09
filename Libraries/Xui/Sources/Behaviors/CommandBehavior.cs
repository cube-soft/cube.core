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
namespace Cube.Xui.Behaviors;

using System.Windows;
using System.Windows.Input;
using Cube.Generics.Extensions;
using Microsoft.Xaml.Behaviors;

#region CommandBehavior<TView>

/* ------------------------------------------------------------------------- */
///
/// CommandBehavior(TView)
///
/// <summary>
/// Represents the inherited Behavior(TView) class that has a Command
/// bindable property.
/// </summary>
///
/* ------------------------------------------------------------------------- */
public class CommandBehavior<TView> : Behavior<TView> where TView : DependencyObject
{
    #region Properties

    /* --------------------------------------------------------------------- */
    ///
    /// Command
    ///
    /// <summary>
    /// Gets or sets the command.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public ICommand Command
    {
        get => GetValue(CommandProperty) as ICommand;
        set => SetValue(CommandProperty, value);
    }

    /* --------------------------------------------------------------------- */
    ///
    /// CommandProperty
    ///
    /// <summary>
    /// Gets the DependencyProperty object for the Command property.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public static readonly DependencyProperty CommandProperty =
        DependencyFactory.Create<CommandBehavior<TView>, ICommand>(
            nameof(Command), (s, e) => s.Command = e);

    #endregion
}

#endregion

#region CommandBehavior<Tview, TParameter>

/* ------------------------------------------------------------------------- */
///
/// CommandBehavior(TView, TParameter)
///
/// <summary>
/// Represents the inherited Behavior(TView) class that has Command
/// and CommandParameter bindable properties.
/// </summary>
///
/* ------------------------------------------------------------------------- */
public class CommandBehavior<TView, TParameter> :
    CommandBehavior<TView> where TView : DependencyObject
{
    #region Properties

    /* --------------------------------------------------------------------- */
    ///
    /// CommandParameter
    ///
    /// <summary>
    /// Gets or sets the command parameter.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public TParameter CommandParameter
    {
        get => GetValue(CommandParameterProperty).TryCast<TParameter>();
        set => SetValue(CommandParameterProperty, value);
    }

    /* --------------------------------------------------------------------- */
    ///
    /// CommandParameterProperty
    ///
    /// <summary>
    /// Gets the DependencyProperty object for the CommandParameter
    /// property.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public static readonly DependencyProperty CommandParameterProperty =
        DependencyFactory.Create<CommandBehavior<TView, TParameter>, TParameter>(
            nameof(CommandParameter), (s, e) => s.CommandParameter = e);

    #endregion
}

#endregion
