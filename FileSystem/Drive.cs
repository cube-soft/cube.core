/* ------------------------------------------------------------------------- */
///
/// Drive.cs
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
using System.IO;
using System.Management;
using System.Collections.Generic;

namespace Cube.FileSystem {
    /* --------------------------------------------------------------------- */
    ///
    /// Cube.FileSystem.Drive
    /// 
    /// <summary>
    /// ドライブに関する情報を保持するためのクラスです。
    /// </summary>
    /// 
    /// <remarks>
    /// 現在、API で取得可能な情報の一部を保持しています。プロパティは
    /// 将来的に増減する可能性があります。
    /// </remarks>
    ///
    /* --------------------------------------------------------------------- */
    public class Drive
    {
        #region Properties

        /* ----------------------------------------------------------------- */
        ///
        /// Index
        ///
        /// <summary>
        /// ドライブ番号を取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public uint Index { get; set; }

        /* ----------------------------------------------------------------- */
        ///
        /// Letter
        ///
        /// <summary>
        /// ドライブレターを取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string Letter { get; set; }

        /* ----------------------------------------------------------------- */
        ///
        /// VolumeLabel
        ///
        /// <summary>
        /// ボリュームラベルを取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string VolumeLabel { get; set; }

        /* ----------------------------------------------------------------- */
        ///
        /// Type
        ///
        /// <summary>
        /// ドライブの種類を取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public DriveType Type { get; set; }

        /* ----------------------------------------------------------------- */
        ///
        /// Format
        ///
        /// <summary>
        /// ドライブのフォーマットを取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string Format { get; set; }

        /* ----------------------------------------------------------------- */
        ///
        /// Model
        ///
        /// <summary>
        /// ディスクのモデル名を取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string Model { get; set; }

        /* ----------------------------------------------------------------- */
        ///
        /// MediaType
        ///
        /// <summary>
        /// ディスクの種類を取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public MediaType MediaType { get; set; }

        /* ----------------------------------------------------------------- */
        ///
        /// InterfaceType
        ///
        /// <summary>
        /// インターフェースの種類を取得または設定します。
        /// </summary>
        /// 
        /// <remarks>
        /// "SCSI", "HDC", "IDE", "USB", "1394" のいずれかの値が設定されます。
        /// </remarks>
        ///
        /* ----------------------------------------------------------------- */
        public string InterfaceType { get; set; }

        /* ----------------------------------------------------------------- */
        ///
        /// FreeSpace
        ///
        /// <summary>
        /// ディスクの空き容量を取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public UInt64 FreeSpace { get; set; }

        /* ----------------------------------------------------------------- */
        ///
        /// Size
        ///
        /// <summary>
        /// ディスク容量を取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public UInt64 Size { get; set; }

        #endregion

        #region Static methods

        /* ----------------------------------------------------------------- */
        ///
        /// GetDrives
        ///
        /// <summary>
        /// 現在のドライブ一覧を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public static Drive[] GetDrives()
        {
            var dest = new List<Drive>();

            using (var searcher = new ManagementObjectSearcher("Select * From Win32_DiskDrive"))
            foreach (ManagementObject device in searcher.Get())
            foreach (ManagementObject partition in device.GetRelated("Win32_DiskPartition"))
            foreach (ManagementObject mapping in partition.GetRelated("Win32_LogicalDisk"))
            {
                var item = GetDriveInfo(device, partition, mapping);
                if (item != null) dest.Add(item);
            }

            return dest.ToArray();
        }

        #endregion

        #region Implementations

        /* ----------------------------------------------------------------- */
        ///
        /// GetDrive
        ///
        /// <summary>
        /// ドライブ情報を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private static Drive GetDriveInfo(ManagementObject device, ManagementObject partition, ManagementObject mapping)
        {
            try
            {
                var dest = new Drive();

                dest.Index = (uint)device["Index"];
                dest.Model = device["Model"] as string;
                dest.InterfaceType = device["InterfaceType"] as string;

                dest.Letter = mapping["Name"] as string;
                dest.VolumeLabel = ToVolumeLabel(mapping["VolumeName"], mapping["Description"]);
                dest.Type = ToDriveType((uint)mapping["DriveType"]);
                dest.Format = mapping["FileSystem"] as string;
                dest.MediaType = ToMediaType((uint)mapping["MediaType"]);
                dest.Size = (UInt64)mapping["Size"];
                dest.FreeSpace = (UInt64)mapping["FreeSpace"];

                return dest;
            }
            catch (Exception /* err */) { return null; }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// ToVolumeLabel
        ///
        /// <summary>
        /// ボリュームラベルを表す文字列に変換します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private static string ToVolumeLabel(object name, object description)
        {
            var s1 = name as string;
            var s2 = description as string;
            return !string.IsNullOrEmpty(s1) ? s1 : s2;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// ToDriveType
        ///
        /// <summary>
        /// DriveType 列挙型に変換します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private static DriveType ToDriveType(uint index)
        {
            foreach (DriveType type in Enum.GetValues(typeof(DriveType)))
            {
                if ((uint)type == index) return type;
            }
            return DriveType.Unknown;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// ToMediaType
        ///
        /// <summary>
        /// MediaType 列挙型に変換します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private static MediaType ToMediaType(uint index)
        {
            switch (index)
            {
                case  0: // Unknown
                    return MediaType.Unknown;
                case  1: // 5 1/4-Inch Floppy Disk - 1.2 MB - 512 bytes/sector
                case  2: // 3 1/2-Inch Floppy Disk - 1.44 MB -512 bytes/sector
                case  3: // 3 1/2-Inch Floppy Disk - 2.88 MB - 512 bytes/sector
                case  4: // 3 1/2-Inch Floppy Disk - 20.8 MB - 512 bytes/sector
                case  5: // 3 1/2-Inch Floppy Disk - 720 KB - 512 bytes/sector
                case  6: // 5 1/4-Inch Floppy Disk - 360 KB - 512 bytes/sector
                case  7: // 5 1/4-Inch Floppy Disk - 320 KB - 512 bytes/sector
                case  8: // 5 1/4-Inch Floppy Disk - 320 KB - 1024 bytes/sector
                case  9: // 5 1/4-Inch Floppy Disk - 180 KB - 512 bytes/sector
                case 10: // 5 1/4-Inch Floppy Disk - 160 KB - 512 bytes/sector
                    return MediaType.FloppyDisk;
                case 11: // Removable media other than floppy
                    return MediaType.RemovableMedia;
                case 12: // Fixed hard disk media
                    return MediaType.HardDisk;
                case 13: // 3 1/2-Inch Floppy Disk - 120 MB - 512 bytes/sector
                case 14: // 3 1/2-Inch Floppy Disk - 640 KB - 512 bytes/sector
                case 15: // 5 1/4-Inch Floppy Disk - 640 KB - 512 bytes/sector
                case 16: // 5 1/4-Inch Floppy Disk - 720 KB - 512 bytes/sector
                case 17: // 3 1/2-Inch Floppy Disk - 1.2 MB - 512 bytes/sector
                case 18: // 3 1/2-Inch Floppy Disk - 1.23 MB - 1024 bytes/sector
                case 19: // 5 1/4-Inch Floppy Disk - 1.23 MB - 1024 bytes/sector
                case 20: // 3 1/2-Inch Floppy Disk - 128 MB - 512 bytes/sector
                case 21: // 3 1/2-Inch Floppy Disk - 230 MB - 512 bytes/sector
                case 22: // 8-Inch Floppy Disk - 256 KB - 128 bytes/sector
                    return MediaType.FloppyDisk;
                default:
                    return MediaType.Unknown;
            }
        }

        #endregion
    }
}
