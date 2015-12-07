using System.Runtime.InteropServices;

namespace Cube
{
    /* ----------------------------------------------------------------- */
    ///
    /// RECT
    ///
    /// <summary>
    /// https://msdn.microsoft.com/en-us/library/a5ch4fda.aspx
    /// </summary>
    ///
    /* ----------------------------------------------------------------- */
    [StructLayout(LayoutKind.Sequential)]
    internal struct RECT
    {
        public int left;
        public int top;
        public int right;
        public int bottom;
    }
}
