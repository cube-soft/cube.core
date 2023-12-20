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

using System.Threading;
using System.Windows;
using Cube.Text.Extensions;
using Cube.Xui.Commands.Extensions;

/* ------------------------------------------------------------------------- */
///
/// EventToCommand
///
/// <summary>
/// Provides functionality to invoke the specified command when the
/// specified event is fired.
/// </summary>
///
/* ------------------------------------------------------------------------- */
public class EventToCommand : CommandBehavior<FrameworkElement>
{
    #region Constructors

    /* --------------------------------------------------------------------- */
    ///
    /// EventToCommand
    ///
    /// <summary>
    /// Initializes a new instance of the EventToCommand class.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public EventToCommand()
    {
        _handler = (_, _) => {
            if (Command?.CanExecute() ?? false) Command.Execute();
        };
    }

    #endregion

    #region Properties

    /* --------------------------------------------------------------------- */
    ///
    /// Event
    ///
    /// <summary>
    /// Gets or sets the target event name.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public string Event
    {
        get => GetValue(EventProperty) as string;
        set
        {
            var e = GetValue(EventProperty) as string;
            if (e == value) return;
            SetValue(EventProperty, value);
            Exchange(value);
        }
    }

    /* --------------------------------------------------------------------- */
    ///
    /// EventProperty
    ///
    /// <summary>
    /// Gets the DependencyProperty object for the Event property.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public static readonly DependencyProperty EventProperty =
        DependencyFactory.Create<EventToCommand, string>(
            nameof(Event), (s, e) => s.Event = e);

    #endregion

    #region Methods

    /* --------------------------------------------------------------------- */
    ///
    /// OnAttached
    ///
    /// <summary>
    /// Occurs when the instance is attached to the FrameworkElement.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    protected override void OnAttached()
    {
        base.OnAttached();
        if (Event.HasValue() && _behavior is null) Exchange(Event);
    }

    /* --------------------------------------------------------------------- */
    ///
    /// OnDetaching
    ///
    /// <summary>
    /// Occurs when the instance is detaching from the FrameworkElement.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    protected override void OnDetaching()
    {
        if (Event.HasValue()) Exchange(string.Empty);
        base.OnDetaching();
    }

    #endregion

    #region Implementations

    /* --------------------------------------------------------------------- */
    ///
    /// Exchange
    ///
    /// <summary>
    /// Updates the inner field with the specified object.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    private void Exchange(string name)
    {
        if (AssociatedObject is null) return;

        var src = name.HasValue() ?
                  new EventBehavior(AssociatedObject, name, _handler) :
                  default;
        Interlocked.Exchange(ref _behavior, src)?.Dispose();
    }

    #endregion

    #region Fields
    private readonly RoutedEventHandler _handler;
    private EventBehavior _behavior;
    #endregion
}
