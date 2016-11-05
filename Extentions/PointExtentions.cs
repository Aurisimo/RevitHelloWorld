using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extentions
{
    public static class PointExtentions
    {
        public static IEnumerable<Line> ToLines(this IList<XYZ> points)
        {
            if (points == null)
                throw new ArgumentNullException("points");

            if (points.Count <= 1)
            {
                throw new ArgumentException("Points argument must contain 2 or more points");
            }

            var prev = points[0];
            for (int i = 1; i < points.Count; i++)
            {
                var curr = points[i];
                var newLine = Line.CreateBound(prev, curr);

                yield return newLine;

                prev = curr;
            }
        }
    }
}
