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
        /// Model
        ///
        /// <summary>
        /// ディスクの種類を取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string MediaType { get; set; }

        /* ----------------------------------------------------------------- */
        ///
        /// InterfaceType
        ///
        /// <summary>
        /// インターフェースの種類を取得または設定します。
        /// </summary>
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
                var item = GetDrive(device, partition, mapping);
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
        private static Drive GetDrive(ManagementObject device, ManagementObject partition, ManagementObject mapping)
        {
            try
            {
                var dest = new Drive();

                dest.Letter = mapping["Name"] as string;
                dest.VolumeLabel = mapping["VolumeName"] as string;
                if (string.IsNullOrEmpty(dest.VolumeLabel)) dest.VolumeLabel = mapping["Description"] as string;
                dest.Size = (UInt64)mapping["Size"];
                dest.FreeSpace = (UInt64)mapping["FreeSpace"];

                dest.Model = device["Model"] as string;
                dest.MediaType = device["MediaType"] as string;
                dest.InterfaceType = device["InterfaceType"] as string;

                return dest;
            }
            catch (Exception /* err */) { return null; }
        }

        #endregion
    }
}
