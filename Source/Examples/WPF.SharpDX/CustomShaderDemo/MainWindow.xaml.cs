// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MainWindow.xaml.cs" company="Helix Toolkit">
//   Copyright (c) 2014 Helix Toolkit contributors
// </copyright>
// <summary>
//   Interaction logic for MainWindow.xaml
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using HelixToolkit.Wpf.SharpDX;

namespace CustomShaderDemo
{
    using Baidu.Guoke.Controller;
    using System;
    using System.Windows;
    using System.Windows.Media.Media3D;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();
            Closed += (s, e) => {
                if (DataContext is IDisposable)
                {
                    (DataContext as IDisposable).Dispose();
                }
            };
            view1.EnableRenderFrustum = true;
            view1.MouseMove3D += View1_MouseDown3D; ;
        }

        private void View1_MouseDown3D(object sender, RoutedEventArgs e)
        {
            var indices = model.Geometry.Indices;
            for (int i = 0; i < indices.Count; i++)
            {
                indices[i]++;
            }
            model.Geometry.Indices = indices;
            (model.Geometry as PointCloudGeometry3D).UpdateIndices();
        }

        private void View1_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Point3D eye = view1.Camera.Position;
            var indices = model.Geometry.Indices;
            for (int i = 0; i < indices.Count; i++)
            {
                indices[i]++;
            }
            model.Geometry.Indices = indices;
        }
    }

}