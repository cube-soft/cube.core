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
namespace Cube.FileSystem;

using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using Cube.Text.Extensions;

/* ------------------------------------------------------------------------- */
///
/// Shortcut
///
/// <summary>
/// Provides functionality to get, create, or delete a shortcut.
/// </summary>
///
/* ------------------------------------------------------------------------- */
public class Shortcut
{
    #region Properties

    /* --------------------------------------------------------------------- */
    ///
    /// FullName
    ///
    /// <summary>
    /// Gets or sets the path of the shortcut.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public string FullName
    {
        get => _path;
        set => _path = Normalize(value);
    }

    /* --------------------------------------------------------------------- */
    ///
    /// Target
    ///
    /// <summary>
    /// Gets or sets the target path of the shortcut.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public string Target { get; set; }

    /* --------------------------------------------------------------------- */
    ///
    /// Arguments
    ///
    /// <summary>
    /// Gets or sets the arguments of the shortcut.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public string Arguments { get; set; }

    /* --------------------------------------------------------------------- */
    ///
    /// IconLocation
    ///
    /// <summary>
    /// Gets or sets the icon location of the shortcut.
    /// </summary>
    ///
    /// <remarks>
    /// The format of IconLocation is IconFileName[,IconIndex].
    /// </remarks>
    ///
    /* --------------------------------------------------------------------- */
    public string IconLocation { get; set; }

    /* --------------------------------------------------------------------- */
    ///
    /// IconFileName
    ///
    /// <summary>
    /// Gets the icon path of the shortcut.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public string IconFileName
    {
        get
        {
            var index = IconLocation?.LastIndexOf(',') ?? 0;
            return index > 0 ? IconLocation.Substring(0, index) : IconLocation;
        }
    }

    /* --------------------------------------------------------------------- */
    ///
    /// IconIndex
    ///
    /// <summary>
    /// Gets the icon index of the shortcut.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public int IconIndex
    {
        get
        {
            var index = IconLocation?.LastIndexOf(',') ?? 0;
            if (index > 0 && index < IconLocation.Length - 1)
            {
                return int.TryParse(IconLocation.Substring(index + 1), out int dest) ? dest : 0;
            }
            return 0;
        }
    }

    /* --------------------------------------------------------------------- */
    ///
    /// Exists
    ///
    /// <summary>
    /// Gets a value indicating whether the shortcut exists.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public bool Exists => FullName.HasValue() && Io.Exists(FullName);

    #endregion

    #region Methods

    /* --------------------------------------------------------------------- */
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
    /* --------------------------------------------------------------------- */
    public static Shortcut Resolve(string link)
    {
        var cvt = Normalize(link);
        if (cvt.HasValue() && Io.Exists(cvt))
        {
            var dest = new Shortcut();
            Invoke(sh =>
            {
                GetPersistFile(sh).Load(cvt, 0);

                dest.FullName     = cvt;
                dest.Target       = GetTarget(sh);
                dest.Arguments    = GetArguments(sh);
                dest.IconLocation = GetIconLocation(sh);
            });
            return dest;
        }
        return null;
    }

    /* --------------------------------------------------------------------- */
    ///
    /// Create
    ///
    /// <summary>
    /// Creates a new shortcut with the current settings.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public void Create() => Invoke(sh =>
    {
        if (!Io.Exists(Target)) return;

        sh.SetPath(Target);
        sh.SetArguments(Arguments);
        sh.SetShowCmd(1); // SW_SHOWNORMAL
        sh.SetIconLocation(IconFileName, IconIndex);

        GetPersistFile(sh).Save(FullName, true);
    });

    /* --------------------------------------------------------------------- */
    ///
    /// Delete
    ///
    /// <summary>
    /// Delete the shortcut.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public void Delete()
    {
        if (Exists) Io.Delete(FullName);
    }

    #endregion

    #region Implementations

    /* --------------------------------------------------------------------- */
    ///
    /// Normalize
    ///
    /// <summary>
    /// Normalizes the link path
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    private static string Normalize(string src) =>
        src.HasValue() && !src.FuzzyEndsWith(".lnk") ? src + ".lnk" : src;

    /* --------------------------------------------------------------------- */
    ///
    /// Invoke
    ///
    /// <summary>
    /// Invokes the specified action.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    private static void Invoke(Action<IShellLink> action)
    {
        var guid = new Guid("00021401-0000-0000-C000-000000000046");
        var type = Type.GetTypeFromCLSID(guid);

        if (Activator.CreateInstance(type) is IShellLink sh)
        {
            try { action(sh); }
            finally { _ = Marshal.ReleaseComObject(sh); }
        }
    }

    /* --------------------------------------------------------------------- */
    ///
    /// GetTarget
    ///
    /// <summary>
    /// Gets the target path of the specified link.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    private static string GetTarget(IShellLink src)
    {
        var dest = GetBuffer();
        src.GetPath(dest, dest.Capacity, IntPtr.Zero, 0x0004);
        return dest.ToString();
    }

    /* --------------------------------------------------------------------- */
    ///
    /// GetArguments
    ///
    /// <summary>
    /// Gets the arguments of the specified link.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    private static string GetArguments(IShellLink src)
    {
        var dest = GetBuffer();
        src.GetArguments(dest, dest.Capacity);
        return dest.ToString();
    }

    /* --------------------------------------------------------------------- */
    ///
    /// GetIconLocation
    ///
    /// <summary>
    /// Gets the icon location of the specified link.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    private static string GetIconLocation(IShellLink src)
    {
        var path = GetBuffer();
        src.GetIconLocation(path, path.Capacity, out var index);
        if (index > 0) _ = path.Append($",{index}");
        return path.ToString();
    }

    /* --------------------------------------------------------------------- */
    ///
    /// GetPersistFile
    ///
    /// <summary>
    /// Gets the IPersistFile object from the specified object.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    private static IPersistFile GetPersistFile(IShellLink src) => src as IPersistFile;

    /* --------------------------------------------------------------------- */
    ///
    /// GetBuffer
    ///
    /// <summary>
    /// Creates a new instance of the StringBuilder class with a
    /// specific capacity.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    private static StringBuilder GetBuffer() => new(65536);

    #endregion

    #region Fields
    private string _path;
    #endregion
}
