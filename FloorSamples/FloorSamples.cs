using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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
                    Level level = getLevel1();
                    Line slopeArrow = getSampleSlope(profile);

                    doc.Create.NewSlab(profile, level, slopeArrow, 0, false);

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
            Line result = null;


            return result;
        }

        private Level getLevel1()
        {
            Level result = null;


            return result;
        }

        private CurveArray getSampleProfile()
        {
            var result = new CurveArray();

            var p0 = new XYZ();



            return result;
        }



    }
}
