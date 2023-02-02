using Rhino.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rhino.Algorithms.Toolkit
{
    public class Cluster
    {
        public List<Brep> Breps { get; set; }

        public Cluster()
        {
            Breps = new List<Brep>();
        }

        public void AddBrep(Brep brep)
        {
            Breps.Add(brep);
        }
        public void AddBreps(List<Brep> breps)
        {
            Breps.AddRange(breps);
        }
    }
}
