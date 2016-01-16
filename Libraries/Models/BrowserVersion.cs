/* ------------------------------------------------------------------------- */
///
/// BrowserVersion.cs
/// 
/// Copyright (c) 2010 CubeSoft, Inc.
/// 
/// This is distributed under the Microsoft Public License (Ms-PL).
/// See http://www.opensource.org/licenses/ms-pl.html
///
/* ------------------------------------------------------------------------- */
using System;

namespace Cube.Forms
{
    /* --------------------------------------------------------------------- */
    ///
    /// Cube.Forms.BrowserVersion
    /// 
    /// <summary>
    /// ブラウザのバージョンを定義した列挙型です。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public enum BrowserVersion : int
    {
        IE7     =  7000,
        IE8     =  8000,
        IE9     =  9000,
        IE10    = 10000,
        IE11    = 11000,
        Latest  =    -1
    }
}
