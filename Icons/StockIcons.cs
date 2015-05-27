/* ------------------------------------------------------------------------- */
///
/// StockIcons.cs
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

namespace Cube
{
    /* --------------------------------------------------------------------- */
    ///
    /// Cube.StockIcons
    /// 
    /// <summary>
    /// システムで用意されているアイコン一覧を定義した列挙型です。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public enum StockIcons : uint
    {
        DocumentNotAssociated       = 0,
        DocumentAssociated          = 1,
        Application                 = 2,
        Folder                      = 3,
        FolderOpen                  = 4,
        Floppy525                   = 5,
        Floppy35                    = 6,
        RemovableDrive              = 7,
        FixedDrive                  = 8,
        NetworkDrive                = 9,
        NetworkDriveDisconnected    = 10,
        CdDrive                     = 11,
        RamDrive                    = 12,
        World                       = 13,
        Server                      = 15,
        Printer                     = 16,
        Network                     = 17,
        Find                        = 22,
        Help                        = 23,
        Share                       = 28,
        Link                        = 29,
        SlowFile                    = 30,
        Recycle                     = 31,
        RecycleFull                 = 32,
        AudioCdMedia                = 40,
        Lock                        = 47,
        AutoList                    = 49,
        NetworkPrinter              = 50,
        ServerShare                 = 51,
        FaxPrinter                  = 52,
        NetworkFaxPrinter           = 53,
        PrintToFile                 = 54,
        Stack                       = 55,
        SvcdMedia                   = 56,
        StuffedFolder               = 57,
        UnknownDrive                = 58,
        DvdDrive                    = 59,
        DvdMedia                    = 60,
        DvdRamMedia                 = 61,
        DvdRwMedia                  = 62,
        DvdRMedia                   = 63,
        DvdRomMedia                 = 64,
        CdPlusMedia                 = 65,
        CdRwMedia                   = 66,
        CdRMedia                    = 67,
        Burning                     = 68,
        BlankCdMedia                = 69,
        CdRomMedia                  = 70,
        AudioFiles                  = 71,
        ImageFiles                  = 72,
        VideoFiles                  = 73,
        MixedFiles                  = 74,
        FolderBack                  = 75,
        FolderFront                 = 76,
        Shield                      = 77,
        Warning                     = 78,
        Information                 = 79,
        Error                       = 80,
        Key                         = 81,
        Software                    = 82,
        Rename                      = 83,
        Delete                      = 84,
        AudioDvdMedia               = 85,
        MovieDvdMedia               = 86,
        EnhancedCdMedia             = 87,
        EnhancedDvdMedia            = 88,
        HdDvdMedia                  = 89,
        BluRayMedia                 = 90,
        VcdMedia                    = 91,
        DvdPlusRMedia               = 92,
        DvdPlusRwMedia              = 93,
        Desktop                     = 94,
        Mobile                      = 95,
        Users                       = 96,
        SmartMedia                  = 97,
        CompactFlash                = 98,
        CellPhone                   = 99,
        Camera                      = 100,
        VideoCamera                 = 101,
        AudioPlayer                 = 102,
        NetworkConnect              = 103,
        Internet                    = 104,
        Zip                         = 105,
        Settings                    = 106,
        MaxIcons                    = 107
    }
}
