using Rhino.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rhino.Algorithms.Toolkit
{
    class Cluster
    {
        public Point3d Centroid { get; set; }
        public List<Point3d> Points { get; set; }

        public Cluster(Point3d centroid)
        {
            Centroid = centroid;
            Points = new List<Point3d>();
        }

        public void AddPoint(Point3d point)
        {
            Points.Add(point);
        }
    }
}
