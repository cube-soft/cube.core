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

#region IQuery<T, U>

/* ------------------------------------------------------------------------- */
///
/// IQuery(T, U)
///
/// <summary>
/// Represents the query provider.
/// </summary>
///
/* ------------------------------------------------------------------------- */
public interface IQuery<T, U>
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
    void Request(QueryMessage<T, U> message);
}

#endregion

#region IQuery<T>

/* ------------------------------------------------------------------------- */
///
/// IQuery(T)
///
/// <summary>
/// Represents the query provider.
/// </summary>
///
/* ------------------------------------------------------------------------- */
public interface IQuery<T> : IQuery<T, T> { }

#endregion

#region Query<T, U>

/* ------------------------------------------------------------------------- */
///
/// Query(T, U)
///
/// <summary>
/// Represents the IQuery(T, U) implementation.
/// </summary>
///
/* ------------------------------------------------------------------------- */
public class Query<T, U> : IQuery<T, U>
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
    public Query(Action<QueryMessage<T, U>> callback) : this(callback, Dispatcher.Vanilla) { }

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
    public Query(Action<QueryMessage<T, U>> callback, Dispatcher dispatcher)
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
    public void Request(QueryMessage<T, U> message) =>
        _dispatcher.Invoke(() => _callback(message));

    #endregion

    #region Fields
    private readonly Dispatcher _dispatcher;
    private readonly Action<QueryMessage<T, U>> _callback;
    #endregion
}

#endregion

#region Query<T>

/* ------------------------------------------------------------------------- */
///
/// Query(T)
///
/// <summary>
/// Represents the IQuery(T, T) implementation.
/// </summary>
///
/* ------------------------------------------------------------------------- */
public class Query<T> : Query<T, T>, IQuery<T>
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
    public Query(Action<QueryMessage<T, T>> callback) : base(callback) { }

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
    public Query(Action<QueryMessage<T, T>> callback, Dispatcher dispatcher) :
        base(callback, dispatcher) { }
}

#endregion

#region OnceQuery<T, U>

/* ------------------------------------------------------------------------- */
///
/// OnceQuery(T, U)
///
/// <summary>
/// Represents the IQuery(T, U) implementation that allows only once.
/// </summary>
///
/* ------------------------------------------------------------------------- */
public class OnceQuery<T, U> : IQuery<T, U>
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
    public OnceQuery(Action<QueryMessage<T, U>> callback) : this(callback, Dispatcher.Vanilla) { }

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
    public OnceQuery(Action<QueryMessage<T, U>> callback, Dispatcher dispatcher)
    {
        _dispather = dispatcher;
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
    public void Request(QueryMessage<T, U> message) =>
        _dispather.Invoke(() => _callback.Invoke(message));

    #endregion

    #region Fields
    private readonly Dispatcher _dispather;
    private readonly OnceAction<QueryMessage<T, U>> _callback;
    #endregion
}

#endregion

#region OnceQuery<T>

/* ------------------------------------------------------------------------- */
///
/// OnceQuery(T)
///
/// <summary>
/// Represents the IQuery(T) implementation that allows only once.
/// </summary>
///
/* ------------------------------------------------------------------------- */
public class OnceQuery<T> : OnceQuery<T, T>, IQuery<T>
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
    public OnceQuery(Action<QueryMessage<T, T>> callback) : base(callback) { }

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
    public OnceQuery(Action<QueryMessage<T, T>> callback, Dispatcher dispatcher) :
        base(callback, dispatcher) { }
}

#endregion
