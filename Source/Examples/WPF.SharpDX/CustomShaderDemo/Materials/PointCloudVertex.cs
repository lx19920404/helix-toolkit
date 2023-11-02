using SharpDX;
using System.Runtime.InteropServices;

namespace Baidu.Guoke.Controller
{
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct PointCloudVertex
    {
        public Vector4 Position;
        public int Label;
        public const int SizeInBytes = 4 * (4 + 1);
    }
}
