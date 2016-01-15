/* ------------------------------------------------------------------------- */
///
/// DriveType.cs
/// 
/// Copyright (c) 2010 CubeSoft, Inc.
/// 
/// Licensed under the Apache License, Version 2.0 (the "License");
/// you may not use this file except in compliance with the License.
/// You may obtain a copy of the License at
///
///  http://www.apache.org/licenses/LICENSE-2.0
///
/// Unless required by applicable law or agreed to in writing, software
/// distributed under the License is distributed on an "AS IS" BASIS,
/// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
/// See the License for the specific language governing permissions and
/// limitations under the License.
///
/* ------------------------------------------------------------------------- */
using System;

namespace Cube.FileSystem
{
    /* --------------------------------------------------------------------- */
    ///
    /// Cube.FileSystem.DriveType
    /// 
    /// <summary>
    /// ドライブの種類を定義した列挙型です。
    /// </summary>
    /// 
    /* --------------------------------------------------------------------- */
    public enum DriveType : uint
    {
        Unknown        = 0,
        CD             = 1,
        Dvd            = 2,
        FloppyDisk     = 3,
        HardDisk       = 4, 
        Network        = 5,
        RemovableDisk  = 6
    }
}
