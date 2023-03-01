using Rhino.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rhino.Algorithms.Toolkit
{
    public class CreateMeshFromBoundaries
    {
        private List<Curve> _oCurves;
        private List<Curve> _iCurves;

        public CreateMeshFromBoundaries(List<Curve> outerCurves, List<Curve> innerCurves)
        {
            _oCurves = outerCurves;
            _iCurves = innerCurves;
        }

        public List<Mesh> CreatePatch()
        {
            //iCurves = new List<Curve>();
            //oCurves = new List<Curve>();
            List<Mesh> meshes = new List<Mesh>();

            Polyline pCrv = _oCurves[0].ToPolyline(-1, -1, 0.25, 1, 0, 0.1, 1, 10, true).ToPolyline();
            if (!pCrv.IsClosed) pCrv.Add(pCrv[0]);

            Mesh mesh = Mesh.CreatePatch(pCrv, 0.25, null, _iCurves, null, null, false, 32);
            meshes.Add(mesh);
            return meshes;
        }
    }
}
