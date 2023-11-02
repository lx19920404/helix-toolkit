using SharpDX;
using System.Runtime.InteropServices;

namespace Baidu.Guoke.Controller
{
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct DashLineVertex
    {
        public Vector4 Position;
        public Color4 Color;
        //public float Distance;
        public const int SizeInBytes = 4 * (4 + 4);
    }
}
