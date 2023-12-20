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

#region IQuery<TSource, TValue>

/* ------------------------------------------------------------------------- */
///
/// IQuery
///
/// <summary>
/// Represents the query provider.
/// </summary>
///
/* ------------------------------------------------------------------------- */
public interface IQuery<TSource, TValue>
{
    /* --------------------------------------------------------------------- */
    ///
    /// Request
    ///
    /// <summary>
    /// Invokes the request with the specified message.
    /// </summary>
    ///
    /// <param name="message">Message to request.</param>
    ///
    /* --------------------------------------------------------------------- */
    void Request(QueryMessage<TSource, TValue> message);
}

#endregion

#region IQuery<TValue>

/* ------------------------------------------------------------------------- */
///
/// IQuery
///
/// <summary>
/// Represents the query provider.
/// </summary>
///
/* ------------------------------------------------------------------------- */
public interface IQuery<TValue> : IQuery<TValue, TValue> { }

#endregion

#region Query<TSource, TValue>

/* ------------------------------------------------------------------------- */
///
/// Query
///
/// <summary>
/// Represents the IQuery(TSource, TValue) implementation.
/// </summary>
///
/* ------------------------------------------------------------------------- */
public class Query<TSource, TValue> : IQuery<TSource, TValue>
{
    #region Methods

    /* --------------------------------------------------------------------- */
    ///
    /// Query
    ///
    /// <summary>
    /// Initializes a new instance of the Query class with the
    /// specified callback.
    /// </summary>
    ///
    /// <param name="callback">Callback function.</param>
    ///
    /* --------------------------------------------------------------------- */
    public Query(Action<QueryMessage<TSource, TValue>> callback) : this(callback, Dispatcher.Vanilla) { }

    /* --------------------------------------------------------------------- */
    ///
    /// Query
    ///
    /// <summary>
    /// Initializes a new instance of the Query class with the
    /// specified arguments.
    /// </summary>
    ///
    /// <param name="callback">Callback function.</param>
    /// <param name="dispatcher">Dispatcher object.</param>
    ///
    /* --------------------------------------------------------------------- */
    public Query(Action<QueryMessage<TSource, TValue>> callback, Dispatcher dispatcher)
    {
        _dispatcher = dispatcher;
        _callback   = callback ?? throw new ArgumentNullException(nameof(callback));
    }

    /* --------------------------------------------------------------------- */
    ///
    /// Request
    ///
    /// <summary>
    /// Invokes the request with the specified message.
    /// </summary>
    ///
    /// <param name="message">Message to request.</param>
    ///
    /* --------------------------------------------------------------------- */
    public void Request(QueryMessage<TSource, TValue> message) =>
        _dispatcher.Invoke(() => _callback(message));

    #endregion

    #region Fields
    private readonly Dispatcher _dispatcher;
    private readonly Action<QueryMessage<TSource, TValue>> _callback;
    #endregion
}

#endregion

#region Query<TValue>

/* ------------------------------------------------------------------------- */
///
/// Query
///
/// <summary>
/// Represents the IQuery(TValue) implementation.
/// </summary>
///
/* ------------------------------------------------------------------------- */
public class Query<TValue> : Query<TValue, TValue>, IQuery<TValue>
{
    /* --------------------------------------------------------------------- */
    ///
    /// Query
    ///
    /// <summary>
    /// Initializes a new instance of the Query class with the
    /// specified callback.
    /// </summary>
    ///
    /// <param name="callback">Callback function.</param>
    ///
    /* --------------------------------------------------------------------- */
    public Query(Action<QueryMessage<TValue, TValue>> callback) : base(callback) { }

    /* --------------------------------------------------------------------- */
    ///
    /// Query
    ///
    /// <summary>
    /// Initializes a new instance of the Query class with the
    /// specified arguments.
    /// </summary>
    ///
    /// <param name="callback">Callback function.</param>
    /// <param name="dispatcher">Dispatcher object.</param>
    ///
    /* --------------------------------------------------------------------- */
    public Query(Action<QueryMessage<TValue, TValue>> callback, Dispatcher dispatcher) :
        base(callback, dispatcher) { }
}

#endregion

#region OnceQuery<TSource, TValue>

/* ------------------------------------------------------------------------- */
///
/// OnceQuery
///
/// <summary>
/// Represents the IQuery(TSource, TValue) implementation that allows only
/// once.
/// </summary>
///
/* ------------------------------------------------------------------------- */
public class OnceQuery<TSource, TValue> : IQuery<TSource, TValue>
{
    #region Methods

    /* --------------------------------------------------------------------- */
    ///
    /// OnceQuery
    ///
    /// <summary>
    /// Initializes a new instance of the Query class with the
    /// specified callback.
    /// </summary>
    ///
    /// <param name="callback">Callback function.</param>
    ///
    /* --------------------------------------------------------------------- */
    public OnceQuery(Action<QueryMessage<TSource, TValue>> callback) : this(callback, Dispatcher.Vanilla) { }

    /* --------------------------------------------------------------------- */
    ///
    /// OnceQuery
    ///
    /// <summary>
    /// Initializes a new instance of the Query class with the
    /// specified arguments.
    /// </summary>
    ///
    /// <param name="callback">Callback function.</param>
    /// <param name="dispatcher">Dispatcher object.</param>
    ///
    /* --------------------------------------------------------------------- */
    public OnceQuery(Action<QueryMessage<TSource, TValue>> callback, Dispatcher dispatcher)
    {
        _dispatcher = dispatcher;
        _callback  = new(callback) { IgnoreTwice = false };
    }

    /* --------------------------------------------------------------------- */
    ///
    /// Request
    ///
    /// <summary>
    /// Invokes the request with the specified message.
    /// </summary>
    ///
    /// <param name="message">Message to request.</param>
    ///
    /// <exception cref="TwiceException">
    /// Occurs when called twice.
    /// </exception>
    ///
    /* --------------------------------------------------------------------- */
    public void Request(QueryMessage<TSource, TValue> message) =>
        _dispatcher.Invoke(() => _callback.Invoke(message));

    #endregion

    #region Fields
    private readonly Dispatcher _dispatcher;
    private readonly OnceAction<QueryMessage<TSource, TValue>> _callback;
    #endregion
}

#endregion

#region OnceQuery<TValue>

/* ------------------------------------------------------------------------- */
///
/// OnceQuery
///
/// <summary>
/// Represents the IQuery(TValue) implementation that allows only once.
/// </summary>
///
/* ------------------------------------------------------------------------- */
public class OnceQuery<TValue> : OnceQuery<TValue, TValue>, IQuery<TValue>
{
    /* --------------------------------------------------------------------- */
    ///
    /// OnceQuery
    ///
    /// <summary>
    /// Initializes a new instance of the Query class with the
    /// specified callback.
    /// </summary>
    ///
    /// <param name="callback">Callback function.</param>
    ///
    /* --------------------------------------------------------------------- */
    public OnceQuery(Action<QueryMessage<TValue, TValue>> callback) : base(callback) { }

    /* --------------------------------------------------------------------- */
    ///
    /// OnceQuery
    ///
    /// <summary>
    /// Initializes a new instance of the Query class with the
    /// specified arguments.
    /// </summary>
    ///
    /// <param name="callback">Callback function.</param>
    /// <param name="dispatcher">Dispatcher object.</param>
    ///
    /* --------------------------------------------------------------------- */
    public OnceQuery(Action<QueryMessage<TValue, TValue>> callback, Dispatcher dispatcher) :
        base(callback, dispatcher) { }
}

#endregion
