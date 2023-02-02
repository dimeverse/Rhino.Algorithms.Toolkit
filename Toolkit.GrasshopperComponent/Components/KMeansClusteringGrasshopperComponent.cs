using Grasshopper;
using Grasshopper.Kernel;
using Rhino.Algorithms.Toolkit;
using Rhino.Geometry;
using System;
using System.Collections.Generic;

namespace Toolkit.GrasshopperComponent.Components
{
    public class KMeansClusteringGrasshopperComponent : GH_Component
    {
        /// <summary>
        /// Each implementation of GH_Component must provide a public 
        /// constructor without any arguments.
        /// Category represents the Tab in which the component will appear, 
        /// Subcategory the panel. If you use non-existing tab or panel names, 
        /// new tabs/panels will automatically be created.
        /// </summary>
        public KMeansClusteringGrasshopperComponent()
          : base("KMeans Clustering", "KMeCl",
            "KMeCl",
            "e-verse", "Utilities")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_InputParamManager pManager)
        {
            pManager.AddBrepParameter("Surfaces", "Surf", "Surf", GH_ParamAccess.list);
            pManager.AddIntegerParameter("Number of Clusters", "NbrCl", "NbrCl", GH_ParamAccess.item, 5);
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_OutputParamManager pManager)
        {
            pManager.AddBrepParameter("Surfaces", "Surf", "Surf", GH_ParamAccess.list);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object can be used to retrieve data from input parameters and 
        /// to store data in output parameters.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            List<Brep> surfaces = new List<Brep>();
            if (!DA.GetDataList(0, surfaces)) return;
            int numberOfClusters = 0;
            if (!DA.GetData(1, ref numberOfClusters)) return;

            int optimalNumberOfClusters = CalinskiHarabaszIndex.Compute(surfaces, 1, 10);
            List<List<Brep>> clusters = KMeansCluster.GetClusters(optimalNumberOfClusters, surfaces);

            DA.SetDataList(0, clusters);

        }

        /// <summary>
        /// Provides an Icon for every component that will be visible in the User Interface.
        /// Icons need to be 24x24 pixels.
        /// You can add image files to your project resources and access them like this:
        /// return Resources.IconForThisComponent;
        /// </summary>
        protected override System.Drawing.Bitmap Icon => null;

        /// <summary>
        /// Each component must have a unique Guid to identify it. 
        /// It is vital this Guid doesn't change otherwise old ghx files 
        /// that use the old ID will partially fail during loading.
        /// </summary>
        public override Guid ComponentGuid => new Guid("6582CEB1-58D1-4624-9462-3160BB71AFDF");
    }
}