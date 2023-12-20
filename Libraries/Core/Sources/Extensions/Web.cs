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
namespace Cube.Web.Extensions;

using System;
using System.Collections.Generic;
using Cube.Chrono.Extensions;
using Cube.Collections.Extensions;
using Cube.Reflection.Extensions;
using Cube.Text.Extensions;

/* ------------------------------------------------------------------------- */
///
/// Methods
///
/// <summary>
/// Provides extended methods of th Uri class.
/// </summary>
///
/* ------------------------------------------------------------------------- */
public static class Methods
{
    #region ToUri

    /* --------------------------------------------------------------------- */
    ///
    /// ToUri
    ///
    /// <summary>
    /// Creates a new instance of the Uri class with the specified
    /// string.
    /// </summary>
    ///
    /// <param name="src">String that represents a URL.</param>
    ///
    /// <returns>Uri object.</returns>
    ///
    /* --------------------------------------------------------------------- */
    public static Uri ToUri(this string src) =>
       !src.HasValue()       ? default :
        src.Contains("://")  ? new(src) :
        src.StartsWith("//") ? new("http:" + src) :
        src.StartsWith("/")  ? new("http://localhost" + src) :
                               new("http://" + src);

    #endregion

    #region With

    /* --------------------------------------------------------------------- */
    ///
    /// With
    ///
    /// <summary>
    /// Combines the specified Uri object and queries.
    /// </summary>
    ///
    /// <param name="src">Source URL.</param>
    /// <param name="query">Queries to be combined.</param>
    ///
    /// <returns>Combined Uri object.</returns>
    ///
    /* --------------------------------------------------------------------- */
    public static Uri With(this Uri src, IDictionary<string, string> query)
    {
        if (src is null || query is null || query.Count <= 0) return src;

        var dest = new UriBuilder(src);
        var str  = query.Join("&", e => $"{e.Key}={e.Value}");
        dest.Query = dest.Query.Length > 1 ?
                     $"{dest.Query.Substring(1)}&{str}" :
                     str;
        return dest.Uri;
    }

    /* --------------------------------------------------------------------- */
    ///
    /// With
    ///
    /// <summary>
    /// Combines the specified Uri object and key-value query.
    /// </summary>
    ///
    /// <param name="src">Source URL.</param>
    /// <param name="key">Key of the query.</param>
    /// <param name="value">Value of the query.</param>
    ///
    /// <returns>Combined Uri object.</returns>
    ///
    /* --------------------------------------------------------------------- */
    public static Uri With<T>(this Uri src, string key, T value) =>
        With(src, new Dictionary<string, string> {{ key, value.ToString() }});

    /* --------------------------------------------------------------------- */
    ///
    /// With
    ///
    /// <summary>
    /// Combines the specified Uri object and date time.
    /// </summary>
    ///
    /// <param name="src">Source URL.</param>
    /// <param name="time">Date time value.</param>
    ///
    /// <returns>Combined Uri object.</returns>
    ///
    /* --------------------------------------------------------------------- */
    public static Uri With(this Uri src, DateTime time) =>
        With(src, "ts", time.ToUnixTime());

    /* --------------------------------------------------------------------- */
    ///
    /// With
    ///
    /// <summary>
    /// Combines the specified Uri object and version information.
    /// </summary>
    ///
    /// <param name="src">Source URL.</param>
    /// <param name="version">Version information.</param>
    ///
    /// <returns>Combined Uri object.</returns>
    ///
    /* --------------------------------------------------------------------- */
    public static Uri With(this Uri src, SoftwareVersion version) =>
        With(src, "ver", version.ToString(3, false));

    /* --------------------------------------------------------------------- */
    ///
    /// With
    ///
    /// <summary>
    /// Combines the specified Uri object and version information of
    /// the specified assembly.
    /// </summary>
    ///
    /// <param name="src">Source URL.</param>
    /// <param name="asm">Assembly object.</param>
    ///
    /// <returns>Combined Uri object.</returns>
    ///
    /* --------------------------------------------------------------------- */
    public static Uri With(this Uri src, System.Reflection.Assembly asm) =>
        With(src, asm.GetSoftwareVersion());

    /* --------------------------------------------------------------------- */
    ///
    /// With
    ///
    /// <summary>
    /// Combines the specified Uri object and UTM queries.
    /// </summary>
    ///
    /// <param name="src">Source URL.</param>
    /// <param name="utm">UTM queries.</param>
    ///
    /// <returns>Combined Uri object.</returns>
    ///
    /* --------------------------------------------------------------------- */
    public static Uri With(this Uri src, Utm utm)
    {
        if (utm is null) return src;
        var query = new Dictionary<string, string>();
        if (utm.Source.HasValue())   query.Add("utm_source", utm.Source);
        if (utm.Medium.HasValue())   query.Add("utm_medium", utm.Medium);
        if (utm.Campaign.HasValue()) query.Add("utm_campaign", utm.Campaign);
        if (utm.Term.HasValue())     query.Add("utm_term", utm.Term);
        if (utm.Content.HasValue())  query.Add("utm_content", utm.Content);
        return With(src, query);
    }

    #endregion

    #region WithoutQuery

    /* --------------------------------------------------------------------- */
    ///
    /// WithoutQuery
    ///
    /// <summary>
    /// Gets the Uri object that is removed queries from the specified
    /// one.
    /// </summary>
    ///
    /// <param name="src">Source URL.</param>
    ///
    /// <returns>Uri object that is removed queries.</returns>
    ///
    /* --------------------------------------------------------------------- */
    public static Uri WithoutQuery(this Uri src) => new(src.GetLeftPart(UriPartial.Path));

    #endregion
}
