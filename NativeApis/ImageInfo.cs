using System;
using System.Runtime.InteropServices;

namespace Cube
{
    /* ----------------------------------------------------------------- */
    ///
    /// IMAGEINFO
    ///
    /// <summary>
    /// https://msdn.microsoft.com/en-us/library/windows/desktop/bb761393.aspx
    /// </summary>
    ///
    /* ----------------------------------------------------------------- */
    [StructLayout(LayoutKind.Sequential)]
    internal struct IMAGEINFO
    {
        public IntPtr hbmImage;
        public IntPtr hbmMask;
        public int Unused1;
        public int Unused2;
        public RECT rcImage;
    }
}
