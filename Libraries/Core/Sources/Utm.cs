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

/* ------------------------------------------------------------------------- */
///
/// Utm
///
/// <summary>
/// Represents the properties that are used in the Google Analytics
/// service.
/// </summary>
///
/// <seealso href="https://support.google.com/analytics/answer/1033863" />
///
/* ------------------------------------------------------------------------- */
public class Utm
{
    /* --------------------------------------------------------------------- */
    ///
    /// Source
    ///
    /// <summary>
    /// Gets or sets a value to identify the advertiser, site,
    /// publication, etc. that is sending traffic to your property.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public string Source { get; set; }

    /* --------------------------------------------------------------------- */
    ///
    /// Medium
    ///
    /// <summary>
    /// Gets or sets a value of the advertising or marketing medium.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public string Medium { get; set; }

    /* --------------------------------------------------------------------- */
    ///
    /// Campaign
    ///
    /// <summary>
    /// Gets or sets a value of the individual campaign name, slogan,
    /// promo code, etc. for a product.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public string Campaign { get; set; }

    /* --------------------------------------------------------------------- */
    ///
    /// Term
    ///
    /// <summary>
    /// Gets or sets a value to identify paid search keywords.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public string Term { get; set; }

    /* --------------------------------------------------------------------- */
    ///
    /// Content
    ///
    /// <summary>
    /// Gets or sets a value that is used to differentiate similar
    /// content, or links within the same ad.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public string Content { get; set; }
}
