using System;
using System.Linq;
using System.Collections.Generic;
using Autodesk.Revit;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Analysis;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using Geometry.Filters;
using DrawUtils;

namespace Geometry
{
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Automatic)]
    [Autodesk.Revit.Attributes.Regeneration(Autodesk.Revit.Attributes.RegenerationOption.Manual)]
    public class Geometry : IExternalCommand
    {
        static AddInId m_appId = new AddInId(new Guid("72DE7C73-2C50-4423-9D36-1A60443B1CED"));
        public Autodesk.Revit.UI.Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            ExternalCommandData cdata = commandData;
            Autodesk.Revit.ApplicationServices.Application app = commandData.Application.Application;
            Document doc = commandData.Application.ActiveUIDocument.Document;
            UIDocument uiDoc = commandData.Application.ActiveUIDocument;


            var line = Line.CreateBound(new XYZ(0,0,0), new XYZ(0, 40, 0));
            var arc = Arc.Create(new XYZ(0, -50, 0), new XYZ(40, -50, 0), new XYZ(50, -60, 0));

            var drawingLine = new DrawingCurve(doc, line);
            drawingLine.Draw();

            var drawingArc = new DrawingCurve(doc, arc);
            drawingArc.Draw();

            return Result.Succeeded;
        }

    }
}
