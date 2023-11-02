using Baidu.Guoke.Controller;
using HelixToolkit.Wpf.SharpDX;
using HelixToolkit.Wpf.SharpDX.Shaders;
using System;
using System.IO;

namespace CustomShaderDemo
{
    public static class ShaderHelper
    {
        public static byte[] LoadShaderCode(string path)
        {
            if (File.Exists(path))
            {
                return File.ReadAllBytes(path);
            }
            else
            {
                throw new ArgumentException($"Shader File not found: {path}");
            }
        }
    }

    public static class CustomVSShaderDescription
    {
        public static ShaderDescription VSCustomPoint = new ShaderDescription(nameof(VSCustomPoint), ShaderStage.Vertex,
            new ShaderReflector(), ShaderHelper.LoadShaderCode(@"Shaders\vsCustomPoint.cso"));
    }

    public class CustomEffectsManager : DefaultEffectsManager
    {
        public CustomEffectsManager()
        {
            LoadCustomTechniqueDescriptions();
        }


        private void LoadCustomTechniqueDescriptions()
        {
            var points = GetTechnique(DefaultRenderTechniqueNames.Points);
            points.AddPass(new ShaderPassDescription("CustomPointPass")
            {
                ShaderList = new[]
                {
                    CustomVSShaderDescription.VSCustomPoint,
                    DefaultGSShaderDescriptions.GSPoint,
                    DefaultPSShaderDescriptions.PSPoint,
                },
                BlendStateDescription = DefaultBlendStateDescriptions.BSAlphaBlend,
                DepthStencilStateDescription = DefaultDepthStencilDescriptions.DSSDepthLessEqual,
                InputLayoutDescription = new InputLayoutDescription(ShaderHelper.LoadShaderCode(@"Shaders\vsCustomPoint.cso"), CustomInputLayout.CustomVSInput),
                
            });
        }
    }
}
