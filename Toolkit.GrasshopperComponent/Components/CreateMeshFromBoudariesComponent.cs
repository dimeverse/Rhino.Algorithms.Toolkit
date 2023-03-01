using Grasshopper.Kernel;
using Rhino.Algorithms.Toolkit;
using Rhino.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toolkit.GrasshopperComponent.Components
{
    public class CreateMeshFromBoudariesComponent : GH_Component
    {
        public CreateMeshFromBoudariesComponent() : base("Create Mesh From Boundaries", "MeshBound",
            "MeshBound",
            "e-verse", "Utilities")
        {
        }
        public override Guid ComponentGuid => new Guid("437CBE83-F551-48BD-B9B5-D1E6A37DD896");

        protected override void RegisterInputParams(GH_InputParamManager pManager)
        {
            pManager.AddCurveParameter("Outer Curves", "oCrvs", "oCrvs", GH_ParamAccess.list);
            pManager.AddCurveParameter("Inner Curves", "iCrvs", "iCrvs", GH_ParamAccess.list);
        }

        protected override void RegisterOutputParams(GH_OutputParamManager pManager)
        {
            pManager.AddMeshParameter("Meshes", "M", "M", GH_ParamAccess.list);
        }

        protected override void SolveInstance(IGH_DataAccess DA)
        {
            List<Curve> oCurves = new List<Curve>();
            List<Curve> iCurves = new List<Curve>();

            if (!DA.GetDataList(0, oCurves)) return;
            if (!DA.GetDataList(1, iCurves)) return;

            CreateMeshFromBoundaries meshCreator = new CreateMeshFromBoundaries(oCurves, iCurves);

            List<Mesh> meshes = meshCreator.CreatePatch();
            DA.SetDataList(0, meshes);
        }
    }
}
