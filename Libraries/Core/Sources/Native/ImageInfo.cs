﻿/* ------------------------------------------------------------------------- */
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
using System;
using System.Runtime.InteropServices;

namespace Cube
{
    /* ----------------------------------------------------------------- */
    ///
    /// ImageInfo
    ///
    /// <summary>
    /// https://msdn.microsoft.com/en-us/library/windows/desktop/bb761393.aspx
    /// </summary>
    ///
    /* ----------------------------------------------------------------- */
    [StructLayout(LayoutKind.Sequential)]
    internal struct ImageInfo
    {
        public IntPtr hbmImage;
        public IntPtr hbmMask;
        public int Unused1;
        public int Unused2;
        public IntPtr /* RECT */ rcImage;
    }
}
