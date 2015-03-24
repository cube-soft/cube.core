/* ------------------------------------------------------------------------- */
///
/// NotifyLevel.cs
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
    /// Cube.Forms.NotifyLevel
    /// 
    /// <summary>
    /// 通知した項目の重要度を示す値を定義した列挙体です。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public enum NotifyLevel : int
    {
        None        = 0,
        Information = 1,
        Recommended = 2,
        Important   = 3,
        Warning     = 4,
        Error       = 5,
    }
}
