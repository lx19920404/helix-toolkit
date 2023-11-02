using HelixToolkit.Wpf.SharpDX;
using HelixToolkit.Wpf.SharpDX.Core;
using HelixToolkit.Wpf.SharpDX.Model.Scene;
using System;

namespace Baidu.Guoke.Controller
{
    public class DashLineNode : LineNode
    {
        protected override IAttachableBufferModel OnCreateBufferModel(Guid modelGuid, Geometry3D geometry)
        {
            return geometry != null && geometry.IsDynamic ? EffectsManager.GeometryBufferManager.Register<DynamicDashLineGeometryBufferModel>(modelGuid, geometry)
                    : EffectsManager.GeometryBufferManager.Register<DefaultDashLineGeometryBufferModel>(modelGuid, geometry);
        }
    }
}
