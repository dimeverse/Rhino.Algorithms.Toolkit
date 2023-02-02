﻿using Grasshopper;
using Rhino.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rhino.Algorithms.Toolkit
{
    public static class KMeansCluster
    {
        public static List<List<Brep>> GetClusters(int numberOfClusters, List<Brep> surfaces)
        {
            List<List<Brep>> clusters = Cluster(surfaces, numberOfClusters);

            foreach (var cluster in clusters)
            {
                System.Console.WriteLine("Cluster:");
                foreach (var surface in cluster)
                {
                    System.Console.WriteLine(surface.ToString());
                }
            }
            return clusters;
        }
        public static List<List<Brep>> Cluster(List<Brep> surfaces, int numberOfClusters)
        {
            Point3d[] centroids = new Point3d[numberOfClusters];
            for (int i = 0; i < numberOfClusters; i++)
            {
                centroids[i] = surfaces[i].GetBoundingBox(false).Center;
            }

            List<List<Brep>> clusters = new List<List<Brep>>();
            for (int i = 0; i < numberOfClusters; i++)
            {
                clusters.Add(new List<Brep>());
            }

            int iterations = 100;
            for (int i = 0; i < iterations; i++)
            {
                foreach (var cluster in clusters)
                {
                    cluster.Clear();
                }

                foreach (var surface in surfaces)
                {
                    int closestCentroidIndex = 0;
                    double closestCentroidDistance = double.MaxValue;
                    Point3d surfaceCentroid = surface.GetBoundingBox(false).Center;
                    for (int j = 0; j < numberOfClusters; j++)
                    {
                        double distance = surfaceCentroid.DistanceTo(centroids[j]);
                        if (distance < closestCentroidDistance)
                        {
                            closestCentroidIndex = j;
                            closestCentroidDistance = distance;
                        }
                    }
                    clusters[closestCentroidIndex].Add(surface);
                }

                for (int j = 0; j < numberOfClusters; j++)
                {
                    Point3d newCentroid = Point3d.Origin;
                    foreach (var surface in clusters[j])
                    {
                        newCentroid += surface.GetBoundingBox(false).Center;
                    }
                    newCentroid /= clusters[j].Count;
                    centroids[j] = newCentroid;
                }
            }

            return clusters;
        }
    }
}
