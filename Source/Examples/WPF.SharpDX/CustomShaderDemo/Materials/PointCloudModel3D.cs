using HelixToolkit.Wpf.SharpDX;
using HelixToolkit.Wpf.SharpDX.Model.Scene;

namespace Baidu.Guoke.Controller
{
    public class PointCloudModel3D:PointMaterialGeometryModel3D
    {
        protected override SceneNode OnCreateSceneNode()
        {
            return new PointCloudNode();
        }
    }
}
