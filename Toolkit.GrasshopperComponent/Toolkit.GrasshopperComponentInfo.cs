using Grasshopper;
using Grasshopper.Kernel;
using System;
using System.Drawing;

namespace Toolkit.GrasshopperComponent
{
    public class Toolkit_GrasshopperComponentInfo : GH_AssemblyInfo
    {
        public override string Name => "Toolkit.GrasshopperComponent";

        //Return a 24x24 pixel bitmap to represent this GHA library.
        public override Bitmap Icon => null;

        //Return a short string describing the purpose of this GHA library.
        public override string Description => "";

        public override Guid Id => new Guid("4AB7E612-0210-4F96-AC41-E60CC97DC77E");

        //Return a string identifying you or your company.
        public override string AuthorName => "";

        //Return a string representing your preferred contact details.
        public override string AuthorContact => "";
    }
}