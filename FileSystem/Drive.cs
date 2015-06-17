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
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// Drive
        ///
        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        /// 
        /// <remarks>
        /// 引数には、C: のようにコロン付ドライブレターを指定します。
        /// </remarks>
        ///
        /* ----------------------------------------------------------------- */
        public Drive(string letter)
        {
            GetInfo(letter);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Drive
        ///
        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        /// 
        /// <remarks>
        /// 引数には、Win32_LogicalDisk から取得したオブジェクトを指定
        /// します。
        /// </remarks>
        ///
        /* ----------------------------------------------------------------- */
        public Drive(object obj)
        {
            var drive = obj as ManagementObject;
            if (drive == null) throw new ArgumentException("Invalid object");
            GetInfo(drive);
        }

        #endregion

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
        public uint Index { get; private set; }

        /* ----------------------------------------------------------------- */
        ///
        /// Letter
        ///
        /// <summary>
        /// ドライブレターを取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string Letter { get; private set; }

        /* ----------------------------------------------------------------- */
        ///
        /// VolumeLabel
        ///
        /// <summary>
        /// ボリュームラベルを取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string VolumeLabel { get; private set; }

        /* ----------------------------------------------------------------- */
        ///
        /// Type
        ///
        /// <summary>
        /// ドライブの種類を取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public DriveType Type { get; private set; }

        /* ----------------------------------------------------------------- */
        ///
        /// Format
        ///
        /// <summary>
        /// ドライブのフォーマットを取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string Format { get; private set; }

        /* ----------------------------------------------------------------- */
        ///
        /// Model
        ///
        /// <summary>
        /// ディスクのモデル名を取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string Model { get; private set; }

        /* ----------------------------------------------------------------- */
        ///
        /// MediaType
        ///
        /// <summary>
        /// ディスクの種類を取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public MediaType MediaType { get; private set; }

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
        public string InterfaceType { get; private set; }

        /* ----------------------------------------------------------------- */
        ///
        /// FreeSpace
        ///
        /// <summary>
        /// ディスクの空き容量を取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public UInt64 FreeSpace { get; private set; }

        /* ----------------------------------------------------------------- */
        ///
        /// Size
        ///
        /// <summary>
        /// ディスク容量を取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public UInt64 Size { get; private set; }

        #endregion

        #region Methods

        /* ----------------------------------------------------------------- */
        ///
        /// Detach
        ///
        /// <summary>
        /// ドライブを取り外します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public void Detach()
        {
            var device = new Device(this);
            device.Detach();
        }

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

            using (var searcher = new ManagementObjectSearcher("Select * From Win32_LogicalDisk"))
            foreach (ManagementObject obj in searcher.Get())
            {
                var drive = new Drive(obj);
                if (!string.IsNullOrEmpty(drive.Letter) && !string.IsNullOrEmpty(drive.Model)) dest.Add(drive);
            }

            return dest.ToArray();
        }

        #endregion

        #region Initialize methods

        /* ----------------------------------------------------------------- */
        ///
        /// GetInfo
        ///
        /// <summary>
        /// 必要な情報を取得して、プロパティを初期化します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void GetInfo(string letter)
        {
            var query = string.Format("Select * From Win32_LogicalDisk Where DeviceID = '{0}'", letter);
            using (var searcher = new ManagementObjectSearcher(query))
            foreach (ManagementObject drive in searcher.Get())
            {
                GetInfo(drive);
                break;
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// GetInfo
        ///
        /// <summary>
        /// 必要な情報を取得して、プロパティを初期化します。
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        private void GetInfo(ManagementObject drive)
        {
            GetDriveInfo(drive);
            GetDeviceInfo(drive);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// GetDriveInfo
        ///
        /// <summary>
        /// ドライブに関する情報を取得して、プロパティを初期化します。
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        private void GetDriveInfo(ManagementObject drive)
        {
            Letter      = drive["Name"] as string;
            VolumeLabel = ToVolumeLabel(drive["VolumeName"], drive["Description"]);
            Type        = ToDriveType(drive["DriveType"]);
            Format      = drive["FileSystem"] as string;
            MediaType   = ToMediaType(drive["MediaType"]);
            Size        = TryCast<UInt64>(drive["Size"]);
            FreeSpace   = TryCast<UInt64>(drive["FreeSpace"]);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// GetDeviceInfo
        ///
        /// <summary>
        /// デバイスに関する情報を取得して、プロパティを初期化します。
        /// </summary>
        /// 
        /// <remarks>
        /// TODO: HardDisk 以外のデバイス情報の取得方法を実装する。
        /// </remarks>
        /// 
        /* ----------------------------------------------------------------- */
        private void GetDeviceInfo(ManagementObject drive)
        {
            switch (MediaType)
            {
                case FileSystem.MediaType.FloppyDisk:
                    // GetFloppyDiskInfo(drive);
                    break;
                case FileSystem.MediaType.HardDisk:
                    GetHardDiskInfo(drive);
                    break;
                case FileSystem.MediaType.RemovableMedia:
                    // GetRemovableMediaInfo(drive);
                    break;
                case FileSystem.MediaType.Unknown:
                    break;
                default:
                    break;
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// GetDeviceInfo
        ///
        /// <summary>
        /// HDD に関する情報を取得して、プロパティを初期化します。
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        private void GetHardDiskInfo(ManagementObject drive)
        {
            foreach (ManagementObject partition in drive.GetRelated("Win32_DiskPartition"))
            foreach (ManagementObject device in partition.GetRelated("Win32_DiskDrive"))
            {
                Index         = TryCast<uint>(device["Index"], uint.MaxValue);
                Model         = device["Model"] as string;
                InterfaceType = device["InterfaceType"] as string;
                break;
            }
        }

        #endregion

        #region Other private methods

        /* ----------------------------------------------------------------- */
        ///
        /// ToVolumeLabel
        ///
        /// <summary>
        /// ボリュームラベルを表す文字列に変換します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private string ToVolumeLabel(object name, object description)
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
        private DriveType ToDriveType(object obj)
        {
            var index = TryCast<uint>(obj, 0);
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
        private MediaType ToMediaType(object obj)
        {
            var index = TryCast<uint>(obj, 0);

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

        /* ----------------------------------------------------------------- */
        ///
        /// TryCast
        ///
        /// <summary>
        /// オブジェクトを T 型にキャストします。
        /// </summary>
        /// 
        /// <remarks>
        /// 失敗した場合、デフォルト値が返ります。
        /// </remarks>
        ///
        /* ----------------------------------------------------------------- */
        private T TryCast<T>(object src)
        {
            return TryCast<T>(src, default(T));
        }

        /* ----------------------------------------------------------------- */
        ///
        /// TryCast
        ///
        /// <summary>
        /// オブジェクトを T 型にキャストします。
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        private T TryCast<T>(object src, T alternative)
        {
            try { return (T)src; }
            catch (Exception /* err */) { return alternative; }
        }

        #endregion
    }
}
