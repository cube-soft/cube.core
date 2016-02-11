/* ------------------------------------------------------------------------- */
///
/// FeatureControl.cs
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
using log4net;

namespace Cube.Forms
{
    public partial class WebBrowser
    {
        /* ----------------------------------------------------------------- */
        ///
        /// FeatureControl
        /// 
        /// <summary>
        /// WebBrowser に関するレジストリを修正するためのクラスです。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public static class FeatureControl
        {
            #region Properties

            /* ------------------------------------------------------------- */
            ///
            /// Emulation
            /// 
            /// <summary>
            /// エミュレートされている IE バージョンを取得または設定します。
            /// </summary>
            ///
            /* ------------------------------------------------------------- */
            public static BrowserVersion Emulation
            {
                get { return GetEmulateVersion(); }
                set { SetEmulateVersion(value); }
            }

            /* ------------------------------------------------------------- */
            ///
            /// GpuRendering
            /// 
            /// <summary>
            /// GPU レンダリングモードが有効かどうかを取得または設定します。
            /// </summary>
            ///
            /* ------------------------------------------------------------- */
            public static bool GpuRendering
            {
                get { return GetGpuRendering(); }
                set { SetGpuRendering(value); }
            }

            /* ------------------------------------------------------------- */
            ///
            /// MaxConnections
            /// 
            /// <summary>
            /// 最大同時接続数を取得または設定します。
            /// </summary>
            ///
            /* ------------------------------------------------------------- */
            public static int MaxConnections
            {
                get { return GetMaxConnections(); }
                set { SetMaxConnections(value); }
            }

            #endregion

            #region Implementations

            #region Browser emulation

            /* ------------------------------------------------------------- */
            ///
            /// GetEmulateVersion
            /// 
            /// <summary>
            /// エミュレートされている IE バージョンを取得します。
            /// </summary>
            ///
            /* ------------------------------------------------------------- */
            private static BrowserVersion GetEmulateVersion()
            {
                try
                {
                    using (var root = OpenFeatureControl())
                    using (var subkey = root.OpenSubKey(_RegEmulation, false))
                    {
                        if (subkey == null) return BrowserVersion.IE7;

                        var module = System.Diagnostics.Process.GetCurrentProcess().MainModule;
                        var filename = System.IO.Path.GetFileName(module.FileName);
                        var version = subkey.GetValue(filename);
                        return version != null ? (BrowserVersion)version : BrowserVersion.IE7;
                    }
                }
                catch (Exception err)
                {
                    Log(err);
                    return BrowserVersion.IE7;
                }
            }

            /* ------------------------------------------------------------- */
            ///
            /// SetEmulateVersion
            /// 
            /// <summary>
            /// エミュレートする IE バージョンを設定します。
            /// </summary>
            ///
            /* ------------------------------------------------------------- */
            private static void SetEmulateVersion(BrowserVersion version)
            {
                try
                {
                    using (var root = OpenFeatureControl(true))
                    using (var subkey = root.CreateSubKey(_RegEmulation))
                    {
                        var module = System.Diagnostics.Process.GetCurrentProcess().MainModule;
                        var filename = System.IO.Path.GetFileName(module.FileName);
                        var value = (version == BrowserVersion.Latest) ? GetLatestVersion() : version;
                        subkey.SetValue(filename, (int)value);
                    }
                }
                catch (Exception err) { Log(err); }
            }

            #endregion

            #region GPU rendering

            /* ------------------------------------------------------------- */
            ///
            /// GetGpuRendering
            /// 
            /// <summary>
            /// GPU レンダリングモードが有効かどうかを取得します。
            /// </summary>
            ///
            /* ------------------------------------------------------------- */
            private static bool GetGpuRendering()
            {
                try
                {
                    using (var root = OpenFeatureControl())
                    using (var subkey = root.OpenSubKey(_RegRendering, false))
                    {
                        if (subkey == null) return false;

                        var module = System.Diagnostics.Process.GetCurrentProcess().MainModule;
                        var filename = System.IO.Path.GetFileName(module.FileName);
                        var value = subkey.GetValue(filename);
                        if (value == null) return false;
                        return (int)value == 1;
                    }
                }
                catch (Exception err)
                {
                    Log(err);
                    return false;
                }
            }

            /* ------------------------------------------------------------- */
            ///
            /// SetGpuRendering
            /// 
            /// <summary>
            /// GPU レンダリングモードを有効にするかどうかを設定します。
            /// </summary>
            ///
            /* ------------------------------------------------------------- */
            private static void SetGpuRendering(bool enabled)
            {
                try
                {
                    using (var root = OpenFeatureControl(true))
                    using (var subkey = root.CreateSubKey(_RegRendering))
                    {
                        var module = System.Diagnostics.Process.GetCurrentProcess().MainModule;
                        var filename = System.IO.Path.GetFileName(module.FileName);
                        var value = enabled ? 1 : 0;
                        subkey.SetValue(filename, value);
                    }
                }
                catch (Exception err) { Log(err); }
            }

            #endregion

            #region Max connections

            /* ------------------------------------------------------------- */
            ///
            /// GetMaxConnections
            /// 
            /// <summary>
            /// 最大同時接続数を取得します。
            /// </summary>
            ///
            /* ------------------------------------------------------------- */
            private static int GetMaxConnections()
            {
                const int default_max_connection = 6;

                try
                {
                    using (var root = OpenFeatureControl())
                    using (var subkey = root.OpenSubKey(_RegMaxConnections, false))
                    {
                        if (subkey == null) return default_max_connection;

                        var module = System.Diagnostics.Process.GetCurrentProcess().MainModule;
                        var filename = System.IO.Path.GetFileName(module.FileName);
                        var value = subkey.GetValue(filename);
                        if (value == null) return default_max_connection;
                        return (int)value;
                    }
                }
                catch (Exception err)
                {
                    Log(err);
                    return default_max_connection;
                }
            }

            /* ------------------------------------------------------------- */
            ///
            /// SetMaxConnections
            /// 
            /// <summary>
            /// 最大同時接続数を設定します。
            /// </summary>
            ///
            /* ------------------------------------------------------------- */
            private static void SetMaxConnections(int number)
            {
                if (number < 2 || number > 128) return;
                try
                {
                    using (var root = OpenFeatureControl(true))
                    using (var subkey = root.CreateSubKey(_RegMaxConnections))
                    using (var subkey10 = root.CreateSubKey(_RegMaxConnections10))
                    {
                        var module = System.Diagnostics.Process.GetCurrentProcess().MainModule;
                        var filename = System.IO.Path.GetFileName(module.FileName);
                        subkey.SetValue(filename, number);
                        subkey10.SetValue(filename, number);
                    }
                }
                catch (Exception err) { Log(err); }
            }

            #endregion

            /* ------------------------------------------------------------- */
            ///
            /// OpenFeatureControl
            /// 
            /// <summary>
            /// FeatureControl 直下のサブキーを取得します。
            /// </summary>
            ///
            /* ------------------------------------------------------------- */
            private static Microsoft.Win32.RegistryKey OpenFeatureControl(bool writable = false)
            {
                var name = System.IO.Path.Combine(_RegRoot, @"Main\FeatureControl");
                return writable ?
                       Microsoft.Win32.Registry.CurrentUser.CreateSubKey(name) :
                       Microsoft.Win32.Registry.CurrentUser.OpenSubKey(name, false);
            }

            /* ------------------------------------------------------------- */
            ///
            /// GetLatestVersion
            /// 
            /// <summary>
            /// PC にインストールされた最新の IE バージョンを取得します。
            /// </summary>
            ///
            /* ------------------------------------------------------------- */
            private static BrowserVersion GetLatestVersion()
            {
                try
                {
                    using (var subkey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(_RegRoot))
                    {
                        var value = subkey.GetValue("svcVersion") as string;
                        if (value == null) value = subkey.GetValue("Version") as string;
                        if (value == null) return BrowserVersion.IE7;

                        var version = int.Parse(value.Substring(0, value.IndexOf('.'))) * 1000;
                        return (BrowserVersion)version;
                    }
                }
                catch (Exception err)
                {
                    Log(err);
                    return BrowserVersion.IE7;
                }
            }

            #endregion

            #region Others

            /* ------------------------------------------------------------- */
            ///
            /// Log
            /// 
            /// <summary>
            /// エラー内容を記録します。
            /// </summary>
            ///
            /* ------------------------------------------------------------- */
            private static void Log(Exception err)
                => LogManager.GetLogger(typeof(FeatureControl)).Error(err);

            #endregion

            #region Fields
            private static readonly string _RegRoot = @"Software\Microsoft\Internet Explorer";
            private static readonly string _RegEmulation = "FEATURE_BROWSER_EMULATION";
            private static readonly string _RegRendering = "FEATURE_GPU_RENDERING ";
            private static readonly string _RegMaxConnections = "FEATURE_MAXCONNECTIONSPERSERVER";
            private static readonly string _RegMaxConnections10 = "FEATURE_MAXCONNECTIONSPER1_0SERVER";
            #endregion
        }
    }
}
