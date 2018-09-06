/* ------------------------------------------------------------------------- */
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
using Cube.Generics;
using IWshRuntimeLibrary;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

namespace Cube.FileSystem
{
    /* --------------------------------------------------------------------- */
    ///
    /// Shortcut
    ///
    /// <summary>
    /// Provides functionality to get, create, or delete a shortcut.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class Shortcut
    {
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// Shortcut
        ///
        /// <summary>
        /// Initializes a new instance of the Shortcut class.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public Shortcut() : this(new IO()) { }

        /* ----------------------------------------------------------------- */
        ///
        /// Shortcut
        ///
        /// <summary>
        /// Initializes a new instance of the Shortcut class with the
        /// specified arguments.
        /// </summary>
        ///
        /// <param name="io">I/O handler.</param>
        ///
        /* ----------------------------------------------------------------- */
        public Shortcut(IO io)
        {
            _io = io;
        }

        #endregion

        #region Properties

        /* ----------------------------------------------------------------- */
        ///
        /// FullName
        ///
        /// <summary>
        /// Gets or sets the path of the shortcut.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string FullName
        {
            get => _path;
            set => _path = Normalize(value);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Target
        ///
        /// <summary>
        /// Gets or sets the target path of the shrotcut.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string Target { get; set; }

        /* ----------------------------------------------------------------- */
        ///
        /// Arguments
        ///
        /// <summary>
        /// Gets or sets the arguments of the shortcut.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public IEnumerable<string> Arguments { get; set; }

        /* ----------------------------------------------------------------- */
        ///
        /// IconLocation
        ///
        /// <summary>
        /// Gets or sets the icon location of the shortcut.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string IconLocation { get; set; }

        /* ----------------------------------------------------------------- */
        ///
        /// Exists
        ///
        /// <summary>
        /// ショートカットが存在するかどうかを示す値を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public bool Exists => FullName.HasValue() && _io.Exists(FullName);

        #endregion

        #region Methods

        /* ----------------------------------------------------------------- */
        ///
        /// Resolve
        ///
        /// <summary>
        /// Creates the Shortcut object with the specified link path.
        /// </summary>
        ///
        /// <param name="link">Link path.</param>
        ///
        /// <returns>Shortcut object.</returns>
        ///
        /* ----------------------------------------------------------------- */
        public static Shortcut Resolve(string link) => Resolve(link, new IO());

        /* ----------------------------------------------------------------- */
        ///
        /// Resolve
        ///
        /// <summary>
        /// Creates the Shortcut object with the specified link path.
        /// </summary>
        ///
        /// <param name="link">Link path.</param>
        /// <param name="io">I/O object.</param>
        ///
        /// <returns>Shortcut object.</returns>
        ///
        /* ----------------------------------------------------------------- */
        public static Shortcut Resolve(string link, IO io)
        {
            var cvt = Normalize(link);
            if (cvt.HasValue() && io.Exists(cvt))
            {
                var sh = new WshShell();
                if (sh.CreateShortcut(cvt) is IWshShortcut dest) return new Shortcut(io)
                {
                    Target       = dest.TargetPath,
                    IconLocation = dest.IconLocation,
                };
            }
            return null;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Create
        ///
        /// <summary>
        /// ショートカットを作成します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public void Create() => Invoke(sh =>
        {
            if (string.IsNullOrEmpty(Target) || !_io.Exists(Target)) return;

            var args = Arguments != null && Arguments.Count() > 0 ?
                       Arguments.Aggregate((s, o) => s + $" {o.Quote()}").Trim() :
                       string.Empty;

            sh.SetPath(Target);
            sh.SetArguments(args);
            sh.SetShowCmd(1); // SW_SHOWNORMAL
            sh.SetIconLocation(GetIconFileName(), GetIconIndex());

            Debug.Assert(sh is IPersistFile);
            ((IPersistFile)sh).Save(FullName, true);
        });

        /* ----------------------------------------------------------------- */
        ///
        /// Delete
        ///
        /// <summary>
        /// ショートカットを削除します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public void Delete()
        {
            if (Exists) _io.Delete(FullName);
        }

        #endregion

        #region Implementations

        /* ----------------------------------------------------------------- */
        ///
        /// Normalize
        ///
        /// <summary>
        /// Normalizes the link path
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private static string Normalize(string src) =>
            src.HasValue() && !src.EndsWith(".lnk") ? src + ".lnk" : src;

        /* ----------------------------------------------------------------- */
        ///
        /// Invoke
        ///
        /// <summary>
        /// Invokes the specified action.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void Invoke(Action<IShellLink> action)
        {
            var guid = new Guid("00021401-0000-0000-C000-000000000046");
            var type = Type.GetTypeFromCLSID(guid);
            var src  = Activator.CreateInstance(type) as IShellLink;

            Debug.Assert(src != null);

            try { action(src); }
            finally { Marshal.ReleaseComObject(src); }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// GetIconFileName
        ///
        /// <summary>
        /// アイコンのファイル名部分を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private string GetIconFileName()
        {
            var index = IconLocation.LastIndexOf(',');
            return (index > 0) ? IconLocation.Substring(0, index) : IconLocation;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// GetIconIndex
        ///
        /// <summary>
        /// アイコンのインデックス部分を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private int GetIconIndex()
        {
            var index = IconLocation.LastIndexOf(',');
            if (index > 0 && index < IconLocation.Length - 1)
            {
                int.TryParse(IconLocation.Substring(index + 1), out int dest);
                return dest;
            }
            else return 0;
        }

        #endregion

        #region Fields
        private readonly IO _io;
        private string _path;
        #endregion
    }
}
