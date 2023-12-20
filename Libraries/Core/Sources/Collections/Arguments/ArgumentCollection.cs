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
namespace Cube.Collections;

using System;
using System.Collections.Generic;
using System.Linq;
using Cube.Collections.Extensions;
using Cube.Text.Extensions;

/* ------------------------------------------------------------------------- */
///
/// ArgumentCollection
///
/// <summary>
/// Provides functionality to parse arguments.
/// </summary>
///
/// <remarks>
/// The class imposes the restriction that each option can only have
/// at most one argument, and all other arguments are stored in their
/// own sequence. If the same option is specified more than once,
/// it will be overwritten by the content specified later.
/// </remarks>
///
/* ------------------------------------------------------------------------- */
public class ArgumentCollection : EnumerableBase<string>, IReadOnlyList<string>
{
    #region Constructors

    /* --------------------------------------------------------------------- */
    ///
    /// ArgumentCollection
    ///
    /// <summary>
    /// Initializes a new instance of the ArgumentCollection class with
    /// the specified arguments.
    /// </summary>
    ///
    /// <param name="src">Source arguments.</param>
    ///
    /* --------------------------------------------------------------------- */
    public ArgumentCollection(IEnumerable<string> src) : this(src, Argument.Windows) { }

    /* --------------------------------------------------------------------- */
    ///
    /// ArgumentCollection
    ///
    /// <summary>
    /// Initializes a new instance of the ArgumentCollection class with
    /// the specified arguments.
    /// </summary>
    ///
    /// <param name="src">Source arguments.</param>
    /// <param name="kind">Prefix kind of optional parameters.</param>
    ///
    /* --------------------------------------------------------------------- */
    public ArgumentCollection(IEnumerable<string> src, Argument kind) : this(src, kind, true) { }

    /* --------------------------------------------------------------------- */
    ///
    /// ArgumentCollection
    ///
    /// <summary>
    /// Initializes a new instance of the ArgumentCollection class with
    /// the specified arguments.
    /// </summary>
    ///
    /// <param name="src">Source arguments.</param>
    /// <param name="kind">Prefix kind of optional parameters.</param>
    /// <param name="ignoreCase">
    /// Value indicating whether to ignore the case of optional keys.
    /// </param>
    ///
    /* --------------------------------------------------------------------- */
    public ArgumentCollection(IEnumerable<string> src, Argument kind, bool ignoreCase) :
        this(src, kind.Get(), ignoreCase) { }

    /* --------------------------------------------------------------------- */
    ///
    /// ArgumentCollection
    ///
    /// <summary>
    /// Initializes a new instance of the ArgumentCollection class with
    /// the specified arguments.
    /// </summary>
    ///
    /// <param name="src">Source arguments.</param>
    ///
    /// <param name="preprocessor">
    /// Object to be invoked before parsing.
    /// </param>
    ///
    /// <param name="ignoreCase">
    /// Value indicating whether to ignore the case of optional keys.
    /// </param>
    ///
    /* --------------------------------------------------------------------- */
    public ArgumentCollection(IEnumerable<string> src, IArgumentPreprocessor preprocessor, bool ignoreCase)
    {
        Preprocessor = preprocessor;
        IgnoreCase   = ignoreCase;
        _options     = ignoreCase ? new(StringComparer.InvariantCultureIgnoreCase) : new();
        Invoke(src);
    }

    #endregion

    #region Properties

    /* --------------------------------------------------------------------- */
    ///
    /// IgnoreCase
    ///
    /// <summary>
    /// Gets the value indicating whether to ignore the case of optional
    /// keys.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public bool IgnoreCase { get; }

    /* --------------------------------------------------------------------- */
    ///
    /// Item(int)
    ///
    /// <summary>
    /// Gets the collection of arguments except for optional parameters.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public string this[int index] => _operands[index];

    /* --------------------------------------------------------------------- */
    ///
    /// Count
    ///
    /// <summary>
    /// Gets the number of arguments except for optional parameters.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public int Count => _operands.Count;

    /* --------------------------------------------------------------------- */
    ///
    /// Options
    ///
    /// <summary>
    /// Gets the collection of optional parameters.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public IReadOnlyDictionary<string, string> Options => _options;

    /* --------------------------------------------------------------------- */
    ///
    /// Preprocessor
    ///
    /// <summary>
    /// Gets a preprocessor that is invoked before parsing.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    internal IArgumentPreprocessor Preprocessor { get; }

    #endregion

    #region Methods

    /* --------------------------------------------------------------------- */
    ///
    /// GetEnumerator
    ///
    /// <summary>
    /// Returns an enumerator that iterates through a collection.
    /// </summary>
    ///
    /// <returns>
    /// Enumerator that can be used to iterate through the collection.
    /// </returns>
    ///
    /* --------------------------------------------------------------------- */
    public override IEnumerator<string> GetEnumerator() => _operands.GetEnumerator();

    #endregion

    #region Implementations

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
        if (disposing)
        {
            _operands.Clear();
            _options.Clear();
        }
    }

    /* --------------------------------------------------------------------- */
    ///
    /// Invoke
    ///
    /// <summary>
    /// Parses the specified arguments.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    private void Invoke(IEnumerable<string> src)
    {
        var key = string.Empty;
        var cvt = Preprocessor.Invoke(src.Where(e => e.HasValue()));

        foreach (var arg in cvt)
        {
            if (arg.Prefix.HasValue())
            {
                if (key.HasValue()) _options.AddOrSet(key, string.Empty);
                key = arg.Value;
            }
            else if (key.HasValue())
            {
                _options.AddOrSet(key, arg.Value);
                key = string.Empty;
            }
            else _operands.Add(arg.Value);
        }

        if (key.HasValue()) _options.AddOrSet(key, string.Empty);
    }

    #endregion

    #region Fields
    private readonly List<string> _operands = new();
    private readonly Dictionary<string, string> _options;
    #endregion
}
