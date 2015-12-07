using System.Runtime.InteropServices;

namespace Cube
{
    /* ----------------------------------------------------------------- */
    ///
    /// POINT
    ///
    /// <summary>
    /// https://msdn.microsoft.com/en-us/library/8kk2sy33.aspx
    /// </summary>
    ///
    /* ----------------------------------------------------------------- */
    [StructLayout(LayoutKind.Sequential)]
    internal struct POINT
    {
        public int x;
        public int y;
    }
}
