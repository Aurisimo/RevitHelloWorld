using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Extentions;

namespace FloorSamples
{

    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    public class FloorSamples : IExternalCommand
    {
        public Autodesk.Revit.UI.Result Execute(ExternalCommandData revit,
           ref string message, ElementSet elements)
        {
            try
            {
                var doc = revit.Application.ActiveUIDocument.Document;

                using (var tran = new Transaction(doc, "FloorSamples"))
                {
                    tran.Start();

                    var profile = getSampleProfile();
                    Level level = getLevel1(doc);
                    Line slopeArrow = getSampleSlope(profile);

                    var floor = doc.Create.NewSlab(profile, level, slopeArrow, 0, true);

                    var spanDirectionAngle = Math.PI;

                    floor.SpanDirectionAngle = spanDirectionAngle;

                    var spanDirIds = floor.GetSpanDirectionSymbolIds();
                    var spanDir = floor.GetSpanDirection();


                    tran.Commit();
                }

                return Autodesk.Revit.UI.Result.Succeeded;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);

                return Result.Failed;
            }
        }

        private Line getSampleSlope(CurveArray profile)
        {
            foreach (Line line in profile)
            {
                return line;
            }

            return null;
        }

        private Level getLevel1(Document doc)
        {
            var col = new FilteredElementCollector(doc);

            var element = col.OfClass(typeof(Level)).FirstElement();

            return element as Level;
        }

        private CurveArray getSampleProfile()
        {
            var result = new CurveArray();

            var p0 = new XYZ(0, 0, 0);
            var p1 = new XYZ(40, 0, 0);
            var p2 = new XYZ(40, 40, 0);
            var p3 = new XYZ(0, 40, 0);
            var p4 = new XYZ(0, 0, 0);

            var points = new List<XYZ>() { p0, p1, p2, p3, p4 };

            var lines = points.ToLines();

            foreach (var line in lines)
            {
                result.Append(line);
            }

            return result;
        }
    }
}
