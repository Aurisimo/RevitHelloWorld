using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Structure;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FamilySamples
{
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    public class FamilySamples : IExternalCommand
    {
        public Result Execute(ExternalCommandData revit,
           ref string message, ElementSet elements)
        {
            var doc = revit.Application.ActiveUIDocument.Document;

            var familyPath = @"C:\Users\aurelijus\Documents\Revit\RevitAPISamples\Libraries\Metric\HD200.rfa";
            var symbolName = "HD200";

            using (var tran = new Transaction(doc, "FamilySamples"))
            {
                try
                {
                    tran.Start();

                    Family family;

                    if (!doc.LoadFamily(familyPath, out family))
                    {
                        MessageBox.Show("Unable to load family: " + familyPath);
                    }

                    var symbolIds = family.GetFamilySymbolIds();

                    var symbols = new List<Element>();
                    foreach (var id in symbolIds)
                    {
                        symbols.Add(doc.GetElement(id));
                    }

                    var symbol = (FamilySymbol)symbols.Where(s => s.Name == symbolName).First();

                    if (!symbol.IsActive)
                    {
                        symbol.Activate();
                    }


                    var floorTypeId = doc.GetDefaultFamilyTypeId(new ElementId(BuiltInCategory.OST_Floors));
                    var floorType = (FloorType)doc.GetElement(floorTypeId);

                    var profile = new CurveArray();
                    profile.Append(Line.CreateBound(new XYZ(0, 0, 0), new XYZ(0, 50, 0)));
                    profile.Append(Line.CreateBound(new XYZ(0, 50, 0), new XYZ(50, 50, 0)));
                    profile.Append(Line.CreateBound(new XYZ(50, 50, 0), new XYZ(50, 0, 0)));
                    profile.Append(Line.CreateBound(new XYZ(50, 0, 0), new XYZ(0, 0, 0)));

                    var collector = new FilteredElementCollector(doc);

                    var level = (Level)collector.OfClass(typeof(Level)).ToElements().First();

                    var floor = doc.Create.NewFloor(profile, floorType, level, false);

                    //var familyInstance = doc.Create.NewFamilyInstance(XYZ.Zero, symbol, XYZ.BasisY, floor, StructuralType.NonStructural);
                    //var familyInstance = doc.Create.NewFamilyInstance(XYZ.Zero, symbol, XYZ.BasisY.Negate(), floor, StructuralType.NonStructural);
                    //var familyInstance = doc.Create.NewFamilyInstance(new XYZ(0, 50, 0), symbol, XYZ.BasisY.Negate(), floor, StructuralType.NonStructural);
                    //var familyInstance = doc.Create.NewFamilyInstance(new XYZ(0, 50, 0), symbol, XYZ.BasisY.Negate(), floor, StructuralType.NonStructural);
                    //var familyInstance = doc.Create.NewFamilyInstance(new XYZ(0, 50, 0), symbol, XYZ.BasisX, floor, StructuralType.NonStructural);
                    //var familyInstance = doc.Create.NewFamilyInstance(new XYZ(0, 50, 0), symbol, XYZ.BasisX.Negate(), floor, StructuralType.NonStructural);


                    doc.Create.NewFamilyInstance(new XYZ(0, 0, 0), symbol, XYZ.BasisX, floor, StructuralType.NonStructural);
                    doc.Create.NewFamilyInstance(new XYZ(0, 50, 0), symbol, XYZ.BasisY.Negate(), floor, StructuralType.NonStructural);
                    doc.Create.NewFamilyInstance(new XYZ(50, 50, 0), symbol, XYZ.BasisX.Negate(), floor, StructuralType.NonStructural);
                    doc.Create.NewFamilyInstance(new XYZ(50, 0, 0), symbol, XYZ.BasisY, floor, StructuralType.NonStructural);

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
    }
}
