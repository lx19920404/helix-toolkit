using HelixToolkit.Wpf.SharpDX;
using HelixToolkit.Wpf.SharpDX.Model.Scene;
using System;

namespace Baidu.Guoke.Controller
{
    public class PointCloudNode:PointNode
    {
        protected override IAttachableBufferModel OnCreateBufferModel(Guid modelGuid, Geometry3D geometry)
        {
            return geometry != null && geometry.IsDynamic ? EffectsManager.GeometryBufferManager.Register<DynamicPointCloudGeometryBufferModel>(modelGuid, geometry)
                    : EffectsManager.GeometryBufferManager.Register<DefaultPointCloudGeometryBufferModel>(modelGuid, geometry);
        }

        protected override bool OnCheckGeometry(Geometry3D geometry)
        {
            return true;
        }
    }
}
