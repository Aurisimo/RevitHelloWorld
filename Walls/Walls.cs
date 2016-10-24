using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Structure;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Extentions;

namespace Walls
{
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    public class Walls : IExternalCommand
    {
        public Result Execute(ExternalCommandData revit,
           ref string message, ElementSet elements)
        {
            var doc = revit.Application.ActiveUIDocument.Document;

            using (var tran = new Transaction(doc, "Walls"))
            {
                try
                {
                    tran.Start();

                    var points = new List<XYZ>()
                    {
                        new XYZ(0, 0, 0),

                        new XYZ(0, 0, 10),
                        new XYZ(30, 0, 10),
                        new XYZ(30, 0, 40),
                        new XYZ(0, 0, 40),

                        new XYZ(0, 0, 50),
                        new XYZ(50, 0, 50),
                        new XYZ(50, 0, 0),
                        new XYZ(0, 0, 0)
                    };

                    var splitPoints = new List<XYZ>()
                    {
                        new XYZ(15, 0, 0)
                    };

                    List<Curve> mainProfile = getCurveLoopFromPoints(points);

                    List<List<Curve>> profiles = getProfilesFromSplitMainProfile(mainProfile, splitPoints);

                    var collector = new FilteredElementCollector(doc);
                    var levelElement = collector.OfClass(typeof(Level)).FirstElement();

                    var level = levelElement as Level;

                    var defaultWallTypeId = doc.GetDefaultFamilyTypeId(new ElementId(BuiltInCategory.OST_Walls));

                    if (level != null && defaultWallTypeId != null)
                    {

                        //foreach (var profle in profiles)
                        //{
                        //    var newWall = Wall.Create(doc, profile, defaultWallTypeId, level.LevelId, structural: false);
                        //}

                        


                    }

                    var line1 = Line.CreateBound(new XYZ(0, 5, 0), new XYZ(40, 5, 0));
                    var curve1 = Arc.Create(new XYZ(0, 0, 0), new XYZ(40, 0, 0), new XYZ(20, 10, 0));

                    SetComparisonResult setComparisonResult;
                    IntersectionResultArray intersectionResultArray = null;

                    setComparisonResult = curve1.Intersect(line1, out intersectionResultArray);

                    tran.Commit();
                }
                catch (Exception e)
                {
                    if (tran.GetStatus() == TransactionStatus.Started)
                    {
                        tran.RollBack();
                    }

                    MessageBox.Show("Error: " + e.Message);
                }

            }

            return Result.Succeeded;
        }

        private List<List<Curve>> getProfilesFromSplitMainProfile(List<Curve> profile, List<XYZ> splitPoints)
        {
            var result = new List<List<Curve>>();
            result.Add(profile);

            if (profile == null || profile.Count < 3)
            {
                return result;
            }

            foreach (var point in splitPoints)
            {
                for (int i = 0; i < result.Count; i++)
                {
                    if (isPointOnProfile(result[i], point))
                    {
                        List<List<Curve>> newProfiles = getProfileSplitByPoint(profile, point);

                        result[i] = newProfiles[0];
                        result.Insert(i + 1, newProfiles[1]);

                        break;
                    }
                }


            }

            return result;
        }

        private List<List<Curve>> getProfileSplitByPoint(List<Curve> profile, XYZ point)
        {
            if (profile == null)
                throw new ArgumentNullException("profile");

            if (point == null)
                throw new ArgumentNullException("point");

            var result = new List<List<Curve>>();

            var newProfile1 = new List<Curve>();
            var newProfile2 = new List<Curve>();


            foreach (var curve in profile)
            {

            }

            result.Add(newProfile1);
            result.Add(newProfile2);

            return result;
        }

        private bool isPointOnProfile(List<Curve> profile, XYZ point)
        {
            //foreach (var curve in profile)
            //{
            //    if (curve.IsIntersectedByPoint())
            //        return true;
            //}

            return false;
        }

        private List<Curve> getCurveLoopFromPoints(List<XYZ> points)
        {
            var result = new List<Curve>();

            if (points == null || points.Count <= 1)
            {
                return result;
            }

            var prev = points[0];
            for (int i = 1; i < points.Count; i++)
            {
                var curr = points[i];
                var newLine = Line.CreateBound(prev, curr);
                result.Add(newLine);

                prev = curr;
            }

            return result;
        }
    }
}
