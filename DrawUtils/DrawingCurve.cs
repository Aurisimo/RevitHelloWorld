using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawUtils
{
    public class DrawingCurve : IDrawableObject
    {
        private Document _doc;
        private Curve _curve;

        public DrawingCurve(Document doc, Curve curve)
        {
            if (doc == null)
                throw new ArgumentNullException("doc");

            if (curve == null)
                throw new ArgumentNullException("curve");

            if (!curve.IsBound)
                throw new ArgumentException("curve must be Bound");

            _doc = doc;
            _curve = curve;
        }

        public void Draw()
        {
            var points = _curve.Tessellate();

            var prevPoint = points[0];
            for (int i = 1; i < points.Count; i++)
            {
                var currPoint = points[i];
                var line = Line.CreateBound(prevPoint, currPoint);

                SketchPlane sketchPlane = getSketchPlane(points);

                _doc.Create.NewModelCurve(line, sketchPlane);

                prevPoint = currPoint;
            }
        }

        private SketchPlane getSketchPlane(IList<XYZ> points)
        {
            var origin = points[0];
            var endPoint1 = points[1];
            XYZ endPoint2 = null;

            if (points.Count <= 2)
                endPoint2 = points[1];
            else
                endPoint2 = points[2];

            var xVec = endPoint1 - origin;
            var yVec = endPoint2 - origin;

            var plane = new Plane(xVec, yVec, origin);

            return SketchPlane.Create(_doc, plane); ;
        }
    }
}
