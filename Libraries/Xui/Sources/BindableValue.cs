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

/* ------------------------------------------------------------------------- */
///
/// BindableValue(T)
///
/// <summary>
/// Provides functionality to make a value as bindable.
/// </summary>
///
/* ------------------------------------------------------------------------- */
public class BindableValue<T> : BindableBase, IValue<T>
{
    #region Constructors

    /* --------------------------------------------------------------------- */
    ///
    /// BindableValue
    ///
    /// <summary>
    /// Initializes a new instance of the BindableValue class.
    /// </summary>
    ///
    /// <param name="dispatcher">Dispatcher object.</param>
    ///
    /* --------------------------------------------------------------------- */
    public BindableValue(Dispatcher dispatcher) : this(default(T), dispatcher) { }

    /* --------------------------------------------------------------------- */
    ///
    /// BindableValue
    ///
    /// <summary>
    /// Initializes a new instance of the BindableValue class with the
    /// specified arguments.
    /// </summary>
    ///
    /// <param name="value">Initial value.</param>
    /// <param name="dispatcher">Dispatcher object.</param>
    ///
    /* --------------------------------------------------------------------- */
    public BindableValue(T value, Dispatcher dispatcher) :
        this(new Accessor<T>(value), dispatcher) { }

    /* --------------------------------------------------------------------- */
    ///
    /// BindableValue
    ///
    /// <summary>
    /// Initializes a new instance of the BindableValue class with the
    /// specified arguments.
    /// </summary>
    ///
    /// <param name="getter">Function to get value.</param>
    /// <param name="dispatcher">Dispatcher object.</param>
    ///
    /* --------------------------------------------------------------------- */
    public BindableValue(Getter<T> getter, Dispatcher dispatcher) :
        this(new Accessor<T>(getter), dispatcher) { }

    /* --------------------------------------------------------------------- */
    ///
    /// BindableValue
    ///
    /// <summary>
    /// Initializes a new instance of the BindableValue class with the
    /// specified arguments.
    /// </summary>
    ///
    /// <param name="getter">Function to get value.</param>
    /// <param name="setter">Function to set value.</param>
    /// <param name="dispatcher">Dispatcher object.</param>
    ///
    /* --------------------------------------------------------------------- */
    public BindableValue(Getter<T> getter, Setter<T> setter, Dispatcher dispatcher) :
        this(new Accessor<T>(getter, setter), dispatcher) { }

    /* --------------------------------------------------------------------- */
    ///
    /// BindableValue
    ///
    /// <summary>
    /// Initializes a new instance of the BindableValue class with the
    /// specified arguments.
    /// </summary>
    ///
    /// <param name="accessor">Function to get and set value.</param>
    /// <param name="dispatcher">Dispatcher object.</param>
    ///
    /* --------------------------------------------------------------------- */
    public BindableValue(Accessor<T> accessor, Dispatcher dispatcher) :
        base(dispatcher) => _accessor = accessor;

    #endregion

    #region Properties

    /* --------------------------------------------------------------------- */
    ///
    /// Value
    ///
    /// <summary>
    /// Gets or sets a value.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public T Value
    {
        get => _accessor.Get();
        set { if (_accessor.Set(value)) Refresh(); }
    }

    #endregion

    #region Methods

    /* --------------------------------------------------------------------- */
    ///
    /// Refresh
    ///
    /// <summary>
    /// Raises a PropertyChanged event against the Value property.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public void Refresh() => Refresh(nameof(Value));

    /* --------------------------------------------------------------------- */
    ///
    /// React
    ///
    /// <summary>
    /// Invokes when the PropertyChanged event of an observed object
    /// is fired.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    protected override void React() => Refresh();

    #endregion

    #region Fields
    private readonly Accessor<T> _accessor;
    #endregion
}
