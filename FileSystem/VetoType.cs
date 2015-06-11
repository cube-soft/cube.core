/* ------------------------------------------------------------------------- */
///
/// VetoType.cs
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
    /// Cube.FileSystem.VetoType
    /// 
    /// <summary>
    /// PnP 操作が拒否された理由を表すための列挙型です。
    /// </summary>
    /// 
    /// <remarks>
    /// https://msdn.microsoft.com/en-us/library/windows/hardware/ff549728.aspx
    /// </remarks>
    ///
    /* --------------------------------------------------------------------- */
    public enum VetoType : uint
    {
        Unknown              =  0, // The specified operation was rejected for an unknown reason.
        LegacyDevice         =  1, // The device does not support the specified PnP operation.
        PendingClose         =  2, // The specified operation cannot be completed because of a pending close operation.
        WindowsApp           =  3, // A Microsoft Win32 application vetoed the specified operation.
        WindowsService       =  4, // A Win32 service vetoed the specified operation.
        OutstandingOpen      =  5, // The requested operation was rejected because of outstanding open handles.
        Device               =  6, // The device supports the specified operation, but the device rejected the operation.
        Driver               =  7, // The driver supports the specified operation, but the driver rejected the operation.
        IllegalDeviceRequest =  8, // The device does not support the specified operation.
        InsufficientPower    =  9, // There is insufficient power to perform the requested operation.
        NonDisableable       = 10, // The device cannot be disabled.
        LegacyDriver         = 11, // The driver does not support the specified PnP operation.
        InsufficientRights   = 12  // The caller has insufficient privileges to complete the operation.
    }
}
