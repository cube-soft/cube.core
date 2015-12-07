using System;
using System.Runtime.InteropServices;

namespace Cube
{
    /* ----------------------------------------------------------------- */
    ///
    /// SHFILEINFO
    ///
    /// <summary>
    /// https://msdn.microsoft.com/en-us/library/windows/desktop/bb759792.aspx
    /// </summary>
    ///
    /* ----------------------------------------------------------------- */
    [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    internal struct SHFILEINFO
    {
        public IntPtr hIcon;
        public int iIcon;
        public uint dwAttributes;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
        public string szDisplayName;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
        public string szTypeName;
    }
}
