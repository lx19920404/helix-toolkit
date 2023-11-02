using HelixToolkit.Wpf.SharpDX;
using HelixToolkit.Wpf.SharpDX.Model;

namespace Baidu.Guoke.Controller
{
    public class CustomPointMaterialCore : PointMaterialCore
    {
        public override MaterialVariable CreateMaterialVariables(IEffectsManager manager, IRenderTechnique technique)
        {
            return new CustomPointMaterialVariable(manager, technique, this);
        }
    }
}