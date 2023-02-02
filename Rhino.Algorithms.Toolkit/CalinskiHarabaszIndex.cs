using Rhino.Algorithms.Toolkit;
using Rhino.Geometry;
using System.Collections.Generic;

public static class CalinskiHarabaszIndex
{
    public static int Compute(List<Brep> breps, int minClusters, int maxClusters)
    {
        int n = breps.Count;
        int d = 3;  // dimension of the breps
        List<Point3d> centroids = new List<Point3d>();

        int bestClusters = 0;
        for (int k = minClusters; k <= maxClusters; k++)
        {
            // Perform k-means clustering
            List<List<Brep>> clusters = KMeansCluster.Cluster(breps, k);

            // Compute the centroids of the clusters
            centroids.Clear();
            foreach (List<Brep> cluster in clusters)
            {
                Point3d centroid = Point3d.Origin;
                foreach (Brep brep in cluster)
                {
                    centroid += brep.GetBoundingBox(true).Center;
                }
                centroid /= cluster.Count;
                centroids.Add(centroid);
            }

            // Compute the within-cluster variance
            double ssw = 0;
            for (int i = 0; i < k; i++)
            {
                Point3d centroid = centroids[i];
                foreach (Brep brep in clusters[i])
                {
                    Point3d bbCenter = brep.GetBoundingBox(true).Center;
                    ssw += (bbCenter - centroid).SquareLength;
                }
            }
            double bestIndex = 0;
            // Compute the between-cluster variance
            Point3d overallCentroid = Point3d.Origin;
            foreach (Point3d centroid in centroids)
            {
                overallCentroid += centroid;
            }
            overallCentroid /= k;

            double ssb = 0;
            for (int i = 0; i < k; i++)
            {
                ssb += (centroids[i] - overallCentroid).SquareLength * clusters[i].Count;
            }

            // Compute the Calinski-Harabasz Index
            double chIndex = (ssb / (k - 1)) / (ssw / (n - k));

            // Check if the current value of k is optimal
            if (chIndex > bestIndex)
            {
                bestIndex = chIndex;
                bestClusters = k;
            }
        }

        return bestClusters;
    }
}