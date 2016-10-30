using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    public class CurveUtils
    {
        public static List<Curve> SplitCurve(Curve c, XYZ p, double shortCurveTolerance, bool preserveDirection = true)
        {
            //only lines are splited, arcs must implemented
            if (c != null
                && p != null
                //&& c is Line
                && IsPointOnCurve(c, p))
            {
                XYZ cS = GetEndPoint(c, 0);
                XYZ cE = GetEndPoint(c, 1);

                if (!p.IsAlmostEqualTo(cS) && !p.IsAlmostEqualTo(cE)
                    && (p.DistanceTo(cS) > shortCurveTolerance || DoubleUtilities.IsDoublesEqual(p.DistanceTo(cS), shortCurveTolerance))
                    && (p.DistanceTo(cE) > shortCurveTolerance || DoubleUtilities.IsDoublesEqual(p.DistanceTo(cE), shortCurveTolerance)))
                {
                    Curve c1 = null;
                    Curve c2 = null;

                    if (c is Arc)
                    {
                        Arc arc = c as Arc;

                        //XYZ center = arc.Evaluate(0.5, true);

                        XYZ center1 = PushOnCurveByCurve(c, GetEndPoint(c, 0), VectorUtils.GetVectorOfCurve(c), cS.DistanceTo(p) / 2.0);
                        XYZ center2 = PushOnCurveByCurve(c, GetEndPoint(c, 1), VectorUtils.GetVectorOfCurve(c).Negate(), cE.DistanceTo(p) / 2.0);

                        c1 = Arc.Create(cS, p, center1);

                        c2 = Arc.Create(p, cE, center2);
                    }
                    else
                    {
                        if (preserveDirection)
                        {
                            c1 = LineUtils.NewLineBound(cS, p) as Curve;
                            c2 = LineUtils.NewLineBound(p, cE) as Curve;
                        }
                        else
                        {
                            c1 = LineUtils.NewLineBound(p, cS) as Curve;
                            c2 = LineUtils.NewLineBound(p, cE) as Curve;
                        }
                    }

                    if (c1 != null && c2 != null)
                    {
                        return new List<Curve>() { c1, c2 };
                    }
                }
            }

            return null;
        }
    }
}
