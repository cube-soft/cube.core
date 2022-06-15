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
namespace Cube;

using System;
using System.Threading;

/* ------------------------------------------------------------------------- */
///
/// PresentableBase(TModel)
///
/// <summary>
/// Represents the base presentable class with a model object, which is
/// the facade of other models.
/// </summary>
///
/* ------------------------------------------------------------------------- */
public abstract class PresentableBase<TModel> : PresentableBase
{
    #region Constructors

    /* --------------------------------------------------------------------- */
    ///
    /// PresentableBase
    ///
    /// <summary>
    /// Initializes a new instance of the PresentableBase class with
    /// the specified model.
    /// </summary>
    ///
    /// <param name="facade">Model object.</param>
    ///
    /* --------------------------------------------------------------------- */
    protected PresentableBase(TModel facade) : this(facade, new()) { }

    /* --------------------------------------------------------------------- */
    ///
    /// Presentable
    ///
    /// <summary>
    /// Initializes a new instance of the Presentable class with the
    /// specified arguments.
    /// </summary>
    ///
    /// <param name="facade">Model object.</param>
    /// <param name="aggregator">Message aggregator.</param>
    ///
    /* --------------------------------------------------------------------- */
    protected PresentableBase(TModel facade, Aggregator aggregator) :
        this(facade, aggregator, SynchronizationContext.Current) { }

    /* --------------------------------------------------------------------- */
    ///
    /// Presentable
    ///
    /// <summary>
    /// Initializes a new instance of the Presentable class with the
    /// specified arguments.
    /// </summary>
    ///
    /// <param name="facade">Model object.</param>
    /// <param name="aggregator">Message aggregator.</param>
    /// <param name="context">Synchronization context.</param>
    ///
    /* --------------------------------------------------------------------- */
    protected PresentableBase(TModel facade, Aggregator aggregator, SynchronizationContext context) :
        base(aggregator, context) => Facade = facade;

    #endregion

    #region Properties

    /* --------------------------------------------------------------------- */
    ///
    /// Facade
    ///
    /// <summary>
    /// Gets the facade of model objects.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    protected TModel Facade { get; }

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
    protected override void Dispose(bool disposing)
    {
        try { if (disposing && Facade is IDisposable obj) obj.Dispose(); }
        finally { base.Dispose(disposing); }
    }

    #endregion
}
