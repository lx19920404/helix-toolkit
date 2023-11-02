using HelixToolkit.Wpf.SharpDX;
using HelixToolkit.Wpf.SharpDX.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CustomShaderDemo.Dashline
{
    public class DashLineMaterial : LineMaterial
    {
        protected override MaterialCore OnCreateCore()
        {
            return new DashLineMaterialCore()
            {
                Name = Name,
                Interval = 5,
                LineColor = Color.ToColor4(),
                Smoothness = (float)Smoothness,
                Thickness = (float)Thickness,
                EnableDistanceFading = EnableDistanceFading,
                FadingNearDistance = (float)FadingNearDistance,
                FadingFarDistance = (float)FadingFarDistance,
                Texture = Texture,
                TextureScale = (float)TextureScale,
                SamplerDescription = SamplerDescription,
                FixedSize = FixedSize
            };
        }

        protected override Freezable CreateInstanceCore()
        {
            return new DashLineMaterial()
            {
                Name = Name
            };
        }
    }
}
