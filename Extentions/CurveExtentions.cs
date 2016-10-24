using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extentions
{
    public static class CurveExtentions
    {
        public static bool IsIntersectedByPoint(this Curve curve, XYZ point)
        {
            IntersectionResult intersectionResult = curve.Project(point);

            if (intersectionResult != null && intersectionResult.XYZPoint != null 
                && point.DistanceTo(intersectionResult.XYZPoint) < 0.001)
            {
                return true;
            }

            return false;
        }
    }
}
