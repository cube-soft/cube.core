namespace Cube.FileSystem;

using System;
using System.Runtime.InteropServices;
using Cube.Mixin.String;

/* ------------------------------------------------------------------------- */
///
/// Shell
///
/// <summary>
/// Provides utility methods for Shell32 DLL.
/// </summary>
///
/* ------------------------------------------------------------------------- */
public static class Shell
{
    /* --------------------------------------------------------------------- */
    ///
    /// GetTypeName
    ///
    /// <summary>
    /// Gets a value that represents type of the specified file.
    /// </summary>
    ///
    /// <param name="src">Path of the source file.</param>
    ///
    /// <returns>Type name of the file.</returns>
    ///
    /* --------------------------------------------------------------------- */
    public static string GetTypeName(string src)
    {
        if (!src.HasValue()) return string.Empty;

        var dest   = new ShFileIinfo();
        var status = Shell32.NativeMethods.SHGetFileInfo(
            src,
            0x0080, // FILE_ATTRIBUTE_NORMAL
            ref dest,
            (uint)Marshal.SizeOf(dest),
            0x0410 // SHGFI_TYPENAME | SHGFI_USEFILEATTRIBUTES
        );

        return (status != IntPtr.Zero) ? dest.szTypeName : string.Empty;
    }
}
