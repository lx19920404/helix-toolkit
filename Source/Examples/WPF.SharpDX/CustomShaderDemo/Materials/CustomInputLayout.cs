using SharpDX.Direct3D11;
using SharpDX.DXGI;

namespace Baidu.Guoke.Controller
{
    public class CustomInputLayout
    {
        public static readonly InputElement[] CustomVSInput = new InputElement[]
        {
                //               语义       索引 格式                      偏移                        输入槽         
                new InputElement("POSITION", 0, Format.R32G32B32A32_Float, InputElement.AppendAligned, 0),
                //new InputElement("COLOR",    0, Format.R32G32B32A32_Float, InputElement.AppendAligned, 0),
                new InputElement("LABEL",    0, Format.R32_UInt,           InputElement.AppendAligned, 0),
        };

        public static readonly InputElement[] VSDashLineInput = new InputElement[]
        {
                //               语义       索引 格式                      偏移                        输入槽         
                new InputElement("POSITION", 0, Format.R32G32B32A32_Float, InputElement.AppendAligned, 0),
                new InputElement("COLOR",    0, Format.R32G32B32A32_Float, InputElement.AppendAligned, 0),
                //new InputElement("DISTANCE", 0, Format.R32_Float,    InputElement.AppendAligned, 0),
        };
    }
}
