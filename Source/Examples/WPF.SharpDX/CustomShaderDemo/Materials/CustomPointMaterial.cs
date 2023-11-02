using HelixToolkit.Wpf.SharpDX;
using HelixToolkit.Wpf.SharpDX.Model;
using System.Windows;

namespace Baidu.Guoke.Controller
{
    public class CustomPointMaterial : PointMaterial
    {
        protected override MaterialCore OnCreateCore()
        {
            return new CustomPointMaterialCore()
            {
                PointColor = Color.ToColor4(),
                Width = (float)Size.Width,
                Height = (float)Size.Height,
                Figure = Figure,
                FigureRatio = (float)FigureRatio,
                Name = Name,
                EnableDistanceFading = EnableDistanceFading,
                FadingNearDistance = (float)FadingNearDistance,
                FadingFarDistance = (float)FadingFarDistance
            };
        }

        protected override Freezable CreateInstanceCore()
        {
            return new CustomPointMaterial()
            {
                Name = Name
            };
        }
    }
}
