using System;
using System.Runtime.InteropServices;

namespace Cube
{
    /* ----------------------------------------------------------------- */
    ///
    /// SHSTOCKICONINFO
    ///
    /// <summary>
    /// https://msdn.microsoft.com/en-us/library/windows/desktop/bb759805.aspx
    /// </summary>
    ///
    /* ----------------------------------------------------------------- */
    [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    internal struct SHSTOCKICONINFO
    {
        public Int32 cbSize;
        public IntPtr hIcon;
        public Int32 iSysImageIndex;
        public Int32 iIcon;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
        public string szPath;
    }

}
