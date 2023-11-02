using HelixToolkit.Wpf.SharpDX;
using SharpDX;
using System;
using System.Runtime.Serialization;

namespace Baidu.Guoke.Controller
{
    public class PointCloudGeometry3D : PointGeometry3D
    {
        private Vector3Collection _customPositions;
        public Vector3Collection CustomPositions
        {
            get
            {
                return _customPositions;
            }
            set
            {
                _customPositions = value;
                ClearOctree();
                RaisePropertyChanged("Positions");
                UpdateBounds();
            }
        }
        private IntCollection labels = null;
        
        [DataMember]
        public IntCollection Labels
        {
            get
            {
                return labels;
            }
            set
            {
                Set(ref labels, value);
            }
        }

        public void UpdateLabels()
        {
            RaisePropertyChanged(nameof(Labels));
        }

        protected override void OnAssignTo(Geometry3D target)
        {
            base.OnAssignTo(target);
            (target as PointCloudGeometry3D).Labels = this.Labels;
        }

        protected override void OnClearAllGeometryData()
        {
            base.OnClearAllGeometryData();
            Labels?.Clear();
            Labels?.TrimExcess();
        }

        public void UpdateIndices()
        {
            RaisePropertyChanged(nameof(Indices));
        }

        public override void UpdateBounds()
        {
            if (DisableUpdateBound)
            {
                return;
            }
            else if (_customPositions == null || _customPositions.Count == 0)
            {
                Bound = new BoundingBox();
                BoundingSphere = new BoundingSphere();
            }
            else
            {
                Bound = BoundingBoxExtensions.FromPoints(CustomPositions);
                BoundingSphere = BoundingSphereExtensions.FromPoints(CustomPositions);
            }
            if (Bound.Maximum.IsUndefined() || Bound.Minimum.IsUndefined() || BoundingSphere.Center.IsUndefined()
                || float.IsInfinity(Bound.Center.X) || float.IsInfinity(Bound.Center.Y) || float.IsInfinity(Bound.Center.Z))
            {
                throw new Exception("Position vertex contains invalid value(Example: Float.NaN, Float.Infinity).");
            }
            
            
            if (Bound.Size.LengthSquared() < 1e-1f)
            {
                var off = new Vector3(1f);
                Bound = new BoundingBox(Bound.Minimum - off, Bound.Maximum + off);
            }
            if (BoundingSphere.Radius < 1e-1f)
            {
                BoundingSphere = new BoundingSphere(BoundingSphere.Center, 1f);
            }
        }

    }
}
