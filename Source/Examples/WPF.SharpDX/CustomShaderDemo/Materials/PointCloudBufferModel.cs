using HelixToolkit.Wpf.SharpDX;
using HelixToolkit.Wpf.SharpDX.Core;
using HelixToolkit.Wpf.SharpDX.Render;
using HelixToolkit.Wpf.SharpDX.Utilities;
using SharpDX;
using SharpDX.Direct3D;
using SharpDX.Direct3D11;
using System.Linq;

namespace Baidu.Guoke.Controller
{
    public abstract class PointCloudGeometryBufferModel<VertexStruct> : GeometryBufferModel where VertexStruct : struct
    {
        protected static readonly VertexStruct[] emptyVerts = new VertexStruct[0];
        protected static readonly int[] emptyIndices = new int[0];

        public PointCloudGeometryBufferModel(int structSize, bool dynamic = false)
            : base(PrimitiveTopology.PointList,
            dynamic ? new DynamicBufferProxy(structSize, BindFlags.VertexBuffer) : new ImmutableBufferProxy(structSize, BindFlags.VertexBuffer) as IElementsBufferProxy,
            dynamic ? new DynamicBufferProxy(sizeof(int), BindFlags.IndexBuffer) : new ImmutableBufferProxy(sizeof(int), BindFlags.IndexBuffer) as IElementsBufferProxy
            )
        {
        }

        public PointCloudGeometryBufferModel(IElementsBufferProxy vertexBuffer) : base(PrimitiveTopology.PointList,
            vertexBuffer, null)
        {
        }

        public PointCloudGeometryBufferModel(IElementsBufferProxy[] vertexBuffer) : base(PrimitiveTopology.PointList,
            vertexBuffer, null)
        {
        }

        protected override void OnCreateIndexBuffer(DeviceContextProxy context, IElementsBufferProxy buffer, Geometry3D geometry, IDeviceResources deviceResources)
        {
            if (geometry != null && geometry.Indices != null && geometry.Indices.Count > 0)
            {
                buffer.UploadDataToBuffer(context, geometry.Indices, geometry.Indices.Count, 0, geometry.PreDefinedIndexCount);
            }
            else
            {
                buffer.UploadDataToBuffer(context, emptyIndices, 0);
            }
        }

        public override bool UpdateBuffers(DeviceContextProxy context, IDeviceResources deviceResources)
        {
            return base.UpdateBuffers(context, deviceResources);
        }
    }

    public class DefaultPointCloudGeometryBufferModel : PointCloudGeometryBufferModel<PointCloudVertex>
    {
        public DefaultPointCloudGeometryBufferModel() : base(PointCloudVertex.SizeInBytes) { }

        public DefaultPointCloudGeometryBufferModel(bool isDynamic) : base(PointCloudVertex.SizeInBytes, isDynamic) { }

        protected override void OnCreateVertexBuffer(DeviceContextProxy context, IElementsBufferProxy buffer, int bufferIndex, Geometry3D geometry, IDeviceResources deviceResources)
        {
            PointCloudGeometry3D pointGeom = geometry as PointCloudGeometry3D;
            if (pointGeom != null && pointGeom.CustomPositions != null && pointGeom.CustomPositions.Count > 0)
            {
                var data = OnBuildVertexArray(pointGeom);
                buffer.UploadDataToBuffer(context, data, pointGeom.CustomPositions.Count, 0, pointGeom.PreDefinedVertexCount);
            }
            else
            {
                buffer.UploadDataToBuffer(context, emptyVerts, 0);
            }
        }


        protected override bool IsVertexBufferChanged(string propertyName, int vertexBufferIndex)
        {
            return base.IsVertexBufferChanged(propertyName, vertexBufferIndex) || propertyName.Equals(nameof(Geometry3D.Colors));
        }

        private PointCloudVertex[] OnBuildVertexArray(Geometry3D geometry)
        {
            PointCloudGeometry3D pointGeom = geometry as PointCloudGeometry3D;
            var positions = pointGeom.CustomPositions;
            var vertexCount = pointGeom.CustomPositions.Count;
            var array = ThreadBufferManager<PointCloudVertex>.GetBuffer(vertexCount);
            var labels = (geometry as PointCloudGeometry3D).Labels != null ? (geometry as PointCloudGeometry3D).Labels.GetEnumerator() : Enumerable.Repeat(0, vertexCount).GetEnumerator();
            for (var i = 0; i < vertexCount; i++)
            {
                labels.MoveNext();
                array[i].Position = new Vector4(positions[i], 1f);
                array[i].Label = labels.Current;
            }
            labels.Dispose();
            return array;
        }
    }

    public sealed class DynamicPointCloudGeometryBufferModel : DefaultPointCloudGeometryBufferModel
    {
        public DynamicPointCloudGeometryBufferModel() : base(true) { }
    }
}
