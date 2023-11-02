using HelixToolkit.Wpf.SharpDX;
using HelixToolkit.Wpf.SharpDX.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomShaderDemo.Dashline
{
    public class DashLineMaterialCore : LineMaterialCore
    {
        private float interval = 5f;
        /// <summary>
        /// 
        /// </summary>
        public float Interval
        {
            set
            {
                Set(ref interval, value);
            }
            get
            {
                return interval;
            }
        }
        public override MaterialVariable CreateMaterialVariables(IEffectsManager manager, IRenderTechnique technique)
        {
            return new LineMaterialVariable(manager, technique, this, "DashLinePass");
        }
    }
}
