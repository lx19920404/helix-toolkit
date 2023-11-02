/*
The MIT License (MIT)
Copyright (c) 2018 Helix Toolkit contributors
*/
using global::SharpDX.Direct3D;
using global::SharpDX.Direct3D11;
using SharpDX;
using System;
using System.Linq;
#if !NETFX_CORE
namespace HelixToolkit.Wpf.SharpDX
#else
#if CORE
namespace HelixToolkit.SharpDX.Core
#else
namespace HelixToolkit.UWP
#endif
#endif
{
    namespace Core
    {
        using Baidu.Guoke.Controller;
        using Render;
        using Utilities;
        /// <summary>
        /// Line Geometry Buffer Model. Used for line rendering
        /// </summary>
        /// <typeparam name="VertexStruct"></typeparam>
        public abstract class DashLineGeometryBufferModel<VertexStruct> : GeometryBufferModel where VertexStruct : struct
        {
            protected static readonly VertexStruct[] emptyVertices = new VertexStruct[0];
            protected static readonly int[] emptyIndices = new int[0];

            /// <summary>
            /// Initializes a new instance of the <see cref="DashLineGeometryBufferModel{VertexStruct}"/> class.
            /// </summary>
            /// <param name="structSize">Size of the structure.</param>
            /// <param name="dynamic">Create dynamic buffer or immutable buffer</param>
            public DashLineGeometryBufferModel(int structSize, bool dynamic = false)
                : base(PrimitiveTopology.LineList,
                dynamic ? new DynamicBufferProxy(structSize, BindFlags.VertexBuffer) : new ImmutableBufferProxy(structSize, BindFlags.VertexBuffer) as IElementsBufferProxy,
                dynamic ? new DynamicBufferProxy(sizeof(int), BindFlags.IndexBuffer) : new ImmutableBufferProxy(sizeof(int), BindFlags.IndexBuffer) as IElementsBufferProxy)
            {
            }

            /// <summary>
            /// Initializes a new instance of the <see cref="DashLineGeometryBufferModel{VertexStruct}"/> class.
            /// </summary>
            /// <param name="vertexBuffer"></param>
            /// <param name="dynamic">Create dynamic buffer or immutable buffer</param> 
            public DashLineGeometryBufferModel(IElementsBufferProxy vertexBuffer, bool dynamic = false)
                : base(PrimitiveTopology.LineList,
                vertexBuffer,
                dynamic ? new DynamicBufferProxy(sizeof(int), BindFlags.IndexBuffer) : new ImmutableBufferProxy(sizeof(int), BindFlags.IndexBuffer) as IElementsBufferProxy)
            {
            }

            /// <summary>
            /// Initializes a new instance of the <see cref="DashLineGeometryBufferModel{VertexStruct}"/> class.
            /// </summary>
            /// <param name="vertexBuffer"></param>
            /// <param name="dynamic">Create dynamic buffer or immutable buffer</param> 
            public DashLineGeometryBufferModel(IElementsBufferProxy[] vertexBuffer, bool dynamic = false)
                : base(PrimitiveTopology.LineList,
                vertexBuffer,
                dynamic ? new DynamicBufferProxy(sizeof(int), BindFlags.IndexBuffer) : new ImmutableBufferProxy(sizeof(int), BindFlags.IndexBuffer) as IElementsBufferProxy)
            {
            }
            /// <summary>
            /// Initializes a new instance of the <see cref="DashLineGeometryBufferModel{VertexStruct}"/> class.
            /// </summary>
            /// <param name="vertexBuffer"></param>
            /// <param name="indexBuffer"></param>
            public DashLineGeometryBufferModel(IElementsBufferProxy vertexBuffer, IElementsBufferProxy indexBuffer)
                : base(PrimitiveTopology.LineList,
                vertexBuffer, indexBuffer)
            {
            }
            /// <summary>
            /// Initializes a new instance of the <see cref="DashLineGeometryBufferModel{VertexStruct}"/> class.
            /// </summary>
            /// <param name="vertexBuffer"></param>
            /// <param name="indexBuffer"></param>
            public DashLineGeometryBufferModel(IElementsBufferProxy[] vertexBuffer, IElementsBufferProxy indexBuffer)
                : base(PrimitiveTopology.LineList,
                vertexBuffer, indexBuffer)
            {
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public class DefaultDashLineGeometryBufferModel : DashLineGeometryBufferModel<DashLineVertex>
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="DefaultDashLineGeometryBufferModel"/> class.
            /// </summary>
            public DefaultDashLineGeometryBufferModel() : base(DashLineVertex.SizeInBytes) { }
            /// <summary>
            /// Initializes a new instance of the <see cref="DefaultDashLineGeometryBufferModel"/> class.
            /// </summary>
            /// <param name="isDynamic"></param>
            public DefaultDashLineGeometryBufferModel(bool isDynamic) : base(DashLineVertex.SizeInBytes, isDynamic) { }

            /// <summary>
            /// Called when [create vertex buffer].
            /// </summary>
            /// <param name="context">The context.</param>
            /// <param name="buffer">The buffer.</param>
            /// <param name="geometry">The geometry.</param>
            /// <param name="deviceResources">The device resources.</param>
            /// <param name="bufferIndex"></param>
            protected override void OnCreateVertexBuffer(DeviceContextProxy context, IElementsBufferProxy buffer, int bufferIndex, Geometry3D geometry, IDeviceResources deviceResources)
            {
                // -- set geometry if given
                if (geometry != null && geometry.Positions != null && geometry.Positions.Count > 0)
                {
                    // --- get geometry
                    var data = OnBuildVertexArray(geometry);
                    buffer.UploadDataToBuffer(context, data, geometry.Positions.Count, 0, geometry.PreDefinedVertexCount);
                }
                else
                {
                    //buffer.DisposeAndClear();
                    buffer.UploadDataToBuffer(context, emptyVertices, 0);
                }
            }

            protected override bool IsVertexBufferChanged(string propertyName, int vertexBufferIndex)
            {
                return base.IsVertexBufferChanged(propertyName, vertexBufferIndex) || propertyName.Equals(nameof(Geometry3D.Colors));
            }
            /// <summary>
            /// Called when [create index buffer].
            /// </summary>
            /// <param name="context">The context.</param>
            /// <param name="buffer">The buffer.</param>
            /// <param name="geometry">The geometry.</param>
            /// <param name="deviceResources">The device resources.</param>
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
            /// <summary>
            /// Called when [build vertex array].
            /// </summary>
            /// <param name="geometry">The geometry.</param>
            /// <returns></returns>
            private DashLineVertex[] OnBuildVertexArray(Geometry3D geometry)
            {
                var positions = geometry.Positions;
                var vertexCount = geometry.Positions.Count;
                var array = ThreadBufferManager<DashLineVertex>.GetBuffer(vertexCount);

                for (var i = 0; i < vertexCount; i++)
                {
                    array[i].Position = new Vector4(positions[i], 1f);
                    if (i % 2 == 0)
                    {
                        array[i].Color = new Color4(0, 0, 0, 0);
                    }
                    else
                    {
                        var a = positions[i] - positions[i - 1];
                        array[i].Color = new Color4(0, 0, 0, (float)Math.Sqrt(a.X * a.X + a.Y * a.Y + a.Z * a.Z) / 10000);
                    }
                }
                return array;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public sealed class DynamicDashLineGeometryBufferModel : DefaultDashLineGeometryBufferModel
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="DynamicDashLineGeometryBufferModel"/> class.
            /// </summary>
            public DynamicDashLineGeometryBufferModel() : base(true) { }
        }
    }

}
