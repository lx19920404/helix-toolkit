using HelixToolkit.Wpf.SharpDX;
using HelixToolkit.Wpf.SharpDX.Core.Components;
using HelixToolkit.Wpf.SharpDX.Model;
using HelixToolkit.Wpf.SharpDX.Render;
using HelixToolkit.Wpf.SharpDX.Shaders;
using SharpDX;

namespace Baidu.Guoke.Controller
{
    public class CustomPointMaterialVariable : PointMaterialVariable
    {
        private readonly ConstantBufferComponent customConstantBuffer;

        public CustomPointMaterialVariable(IEffectsManager manager, IRenderTechnique technique, PointMaterialCore materialCore,
            string pointPassName = "CustomPointPass")
            : base(manager, technique, materialCore, pointPassName)
        {
            customConstantBuffer = Collect(new ConstantBufferComponent(new ConstantBufferDescription("CustomBuffer", 16 * 256)));
            customConstantBuffer.Attach(technique);
        }

        protected override void OnInitialPropertyBindings()
        {
            base.OnInitialPropertyBindings();
        }

        public override bool BindMaterialResources(RenderContext context, DeviceContextProxy deviceContext, ShaderPass shaderPass)
        {
            UtilColor3D.Init_Intensity_Colors_Map();
            for(int i = 0; i < 256; i++)
            {
                Color4 color = UtilColor3D.st_Intensity_Colors_Map_Dic[i];
                customConstantBuffer.WriteValue(color.Red, i * 16 + 0);
                customConstantBuffer.WriteValue(color.Green, i * 16 + 4);
                customConstantBuffer.WriteValue(color.Blue, i * 16 + 8);
                customConstantBuffer.WriteValue(color.Alpha, i * 16 + 12);
            }
            customConstantBuffer.Upload(deviceContext);
            return base.BindMaterialResources(context, deviceContext, shaderPass);
        }

        protected override void OnDispose(bool disposeManagedResources)
        {
            customConstantBuffer.Detach();
            base.OnDispose(disposeManagedResources);
        }

        public override void Draw(DeviceContextProxy deviceContext, IAttachableBufferModel bufferModel, int instanceCount)
        {
            if (bufferModel.IndexBuffer.ElementCount == 0)
                DrawPoints(deviceContext, bufferModel.VertexBuffer[0].ElementCount, instanceCount);
            else
                DrawIndexed(deviceContext, bufferModel.IndexBuffer.ElementCount, instanceCount);
        }
    }
}
