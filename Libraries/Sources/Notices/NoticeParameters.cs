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
namespace Cube
{
    #region NoticePriority

    /* --------------------------------------------------------------------- */
    ///
    /// NoticePriority
    ///
    /// <summary>
    /// Specifies the priority of a notice.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public enum NoticePriority
    {
        /// <summary>Highest</summary>
        Highest = 40,
        /// <summary>High</summary>
        High = 30,
        /// <summary>Normal</summary>
        Normal = 20,
        /// <summary>Low</summary>
        Low = 10,
        /// <summary>Lowest</summary>
        Lowest = 0,
    }

    #endregion

    #region NoticeLocation

    /* --------------------------------------------------------------------- */
    ///
    /// NoticeLocation
    ///
    /// <summary>
    /// Specifies the location to show the notice window.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public enum NoticeLocation
    {
        /// <summary>Top-left corner of the screen.</summary>
        TopLeft,
        /// <summary>Top-right corner of the screen.</summary>
        TopRight,
        /// <summary>Bottom-left corner of the screen.</summary>
        BottomLeft,
        /// <summary>Bottom-right corner of the screen.</summary>
        BottomRight,
    }

    #endregion

    #region NoticeResult

    /* --------------------------------------------------------------------- */
    ///
    /// NoticeResult
    ///
    /// <summary>
    /// Specifies the result of the displayed notice window.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public enum NoticeResult
    {
        /// <summary>Timeout</summary>
        Timeout,
        /// <summary>Close</summary>
        Close,
        /// <summary>Image</summary>
        Image,
        /// <summary>Title</summary>
        Title,
        /// <summary>Text</summary>
        Text,
    }

    #endregion
}
