using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extentions
{
    public static class ProfileExtentions
    {
        public static List<List<Curve>> GetSplitByVerticalLine(this List<Curve> profile, Line splitLine)
        {
            if (profile == null)
                throw new ArgumentNullException("profile");

            if (splitLine == null)
                throw new ArgumentNullException("splitLine");

            if (!(splitLine.Direction.IsAlmostEqualTo(XYZ.BasisZ) || splitLine.Direction.IsAlmostEqualTo(XYZ.BasisZ.Negate())))
                throw new ArgumentException("Line must be vertical");

            var result = new List<List<Curve>>();

            var profile1 = new List<Curve>();
            var profile2 = new List<Curve>();

            foreach (var curve in profile)
            {
                IntersectionResultArray intersectionResultArray;
                SetComparisonResult setComparisonResult = curve.Intersect(splitLine, out intersectionResultArray);

                //curve.




            }

            return result;
        }
    }
}
