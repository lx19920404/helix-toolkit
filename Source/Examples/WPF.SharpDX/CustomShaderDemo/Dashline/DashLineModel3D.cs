using HelixToolkit.Wpf.SharpDX;
using HelixToolkit.Wpf.SharpDX.Model.Scene;

namespace Baidu.Guoke.Controller
{
    public class DashLineModel3D : LineMaterialGeometryModel3D
    {
        protected override SceneNode OnCreateSceneNode()
        {
            return new DashLineNode();
        }
    }
}
