using System;
using Autodesk.Revit.UI;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI.Selection;
using System.Collections.Generic;
using System.Linq;

namespace RevitHelloWorld
{
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    public class RevitHelloWorld : IExternalCommand
    {
        public Autodesk.Revit.UI.Result Execute(ExternalCommandData revit,
           ref string message, ElementSet elements)
        {

            var doc = revit.Application.ActiveUIDocument.Document;

            //var reference = revit.Application.ActiveUIDocument.Selection.PickObject(ObjectType.Element);
            //var element = doc.GetElement(reference);

            //if (element is Wall)




            var defaultType = doc.GetDefaultFamilyTypeId(new ElementId(BuiltInCategory.OST_Walls));
            var level = new FilteredElementCollector(doc).OfClass(typeof(Level)).First();

            {
                var tran = new Transaction(doc);
                tran.SetName("DrawLines");
                tran.Start();

                //var wall = element as Wall;

                var currPosition = 0;
                var step = 30;

                var outerProfile = new List<Line>()
                {
                    Line.CreateBound(new XYZ(0, 0, 0), new XYZ(0,0,20)),
                    Line.CreateBound(new XYZ(0, 0, 20), new XYZ(20,0,20)),
                    Line.CreateBound(new XYZ(20, 0, 20), new XYZ(20,0,0)),
                    Line.CreateBound(new XYZ(20, 0, 0), new XYZ(0,0,0)),
                };

                var innerProfile = new List<Line>()
                {
                    Line.CreateBound(new XYZ(5, 0, 5), new XYZ(5, 0, 15)),
                    Line.CreateBound(new XYZ(5, 0, 15), new XYZ(15, 0, 15)),
                    Line.CreateBound(new XYZ(15, 0, 15), new XYZ(15, 0, 5)),
                    Line.CreateBound(new XYZ(15, 0, 5), new XYZ(5, 0, 5)),
                };

                var innerProfileRectanglEdgeContains2Lines = new List<Line>()
                {
                    Line.CreateBound(new XYZ(5, 0, 5), new XYZ(5, 0, 7.5)),
                    Line.CreateBound(new XYZ(5, 0, 7.5), new XYZ(5, 0, 15)),

                    Line.CreateBound(new XYZ(5, 0, 15), new XYZ(7.5, 0, 15)),
                    Line.CreateBound(new XYZ(7.5, 0, 15), new XYZ(15, 0, 15)),

                    Line.CreateBound(new XYZ(15, 0, 15), new XYZ(15, 0, 7.5)),
                    Line.CreateBound(new XYZ(15, 0, 7.5), new XYZ(15, 0, 5)),

                    Line.CreateBound(new XYZ(15, 0, 5), new XYZ(7.5, 0, 5)),
                    Line.CreateBound(new XYZ(7.5, 0, 5), new XYZ(5, 0, 5)),
                };

                var innerProfileRectanglEdgeContains3Lines = new List<Line>()
                {
                    Line.CreateBound(new XYZ(5, 0, 5), new XYZ(5, 0, 6.5)),
                    Line.CreateBound(new XYZ(5, 0, 6.5), new XYZ(5, 0, 7.5)),
                    Line.CreateBound(new XYZ(5, 0, 7.5), new XYZ(5, 0, 15)),

                    Line.CreateBound(new XYZ(5, 0, 15), new XYZ(6.5, 0, 15)),
                    Line.CreateBound(new XYZ(6.5, 0, 15), new XYZ(7.5, 0, 15)),
                    Line.CreateBound(new XYZ(7.5, 0, 15), new XYZ(15, 0, 15)),

                    Line.CreateBound(new XYZ(15, 0, 15), new XYZ(15, 0, 6.5)),
                    Line.CreateBound(new XYZ(15, 0, 6.5), new XYZ(15, 0, 7.5)),
                    Line.CreateBound(new XYZ(15, 0, 7.5), new XYZ(15, 0, 5)),

                    Line.CreateBound(new XYZ(15, 0, 5), new XYZ(6.5, 0, 5)),
                    Line.CreateBound(new XYZ(6.5, 0, 5), new XYZ(7.5, 0, 5)),
                    Line.CreateBound(new XYZ(7.5, 0, 5), new XYZ(5, 0, 5)),
                };

                var innerProfileTriangle = new List<Line>()
                {
                    Line.CreateBound(new XYZ(5, 0, 5), new XYZ(10, 0, 15)),
                    Line.CreateBound(new XYZ(10, 0, 15), new XYZ(15, 0, 5)),
                    Line.CreateBound(new XYZ(15, 0, 5), new XYZ(5, 0, 5)),
                };

                var innerProfileCoplexWithHorizontalLines = new List<Line>()
                {
                    Line.CreateBound(new XYZ(5, 0, 5), new XYZ(7, 0, 15)),
                    Line.CreateBound(new XYZ(7, 0, 15), new XYZ(13, 0, 15)),
                    Line.CreateBound(new XYZ(13, 0, 15), new XYZ(15, 0, 10)),
                    Line.CreateBound(new XYZ(15, 0, 10), new XYZ(9, 0, 10)),
                    Line.CreateBound(new XYZ(9, 0, 10), new XYZ(9, 0, 5)),
                    Line.CreateBound(new XYZ(9, 0, 5), new XYZ(5, 0, 5))
                };

                var innerProfileCoplexWithoutHorizontalLines = new List<Line>()
                {
                    Line.CreateBound(new XYZ(5, 0, 5), new XYZ(7, 0, 14)),
                    Line.CreateBound(new XYZ(7, 0, 14), new XYZ(13, 0, 15)),
                    Line.CreateBound(new XYZ(13, 0, 15), new XYZ(15, 0, 10)),
                    Line.CreateBound(new XYZ(15, 0, 10), new XYZ(11, 0, 9)),
                    Line.CreateBound(new XYZ(11, 0, 9), new XYZ(8, 0, 6)),
                    Line.CreateBound(new XYZ(8, 0, 6), new XYZ(5, 0, 5))
                };

                var wholeProfile = new List<Line>();
                wholeProfile.AddRange(outerProfile);
                wholeProfile.AddRange(getSequenceStartingWithNotHorizontalLine(innerProfile));

                drawLines(doc, wholeProfile);

                var newWall = Wall.Create(doc,
                    wholeProfile.Select(l => (Curve)l).ToList(),
                    defaultType,
                    level.Id,
                    false,
                    XYZ.BasisY);

                var flipedWallWholeProfile = getMovedLinesAlongX(wholeProfile, currPosition += step);

                drawLines(doc, flipedWallWholeProfile);

                Wall.Create(doc,
                    flipedWallWholeProfile.Select(l => (Curve)l).ToList(),
                    defaultType,
                    level.Id,
                    false,
                    XYZ.BasisY.Negate());

                var wholeProfile2 = getReversedProfile(wholeProfile);
                var movedWholeProfile2 = getMovedLinesAlongX(wholeProfile2, currPosition += step);

                drawLines(doc, movedWholeProfile2);

                var newWall2 = Wall.Create(doc,
                    movedWholeProfile2.Select(l => (Curve)l).ToList(),
                    defaultType,
                    level.Id,
                    false,
                    XYZ.BasisY);

                var flipedWallWholeProfile2 = getMovedLinesAlongX(wholeProfile2, currPosition += step);

                drawLines(doc, flipedWallWholeProfile2);

                Wall.Create(doc,
                    flipedWallWholeProfile2.Select(l => (Curve)l).ToList(),
                    defaultType,
                    level.Id,
                    false,
                    XYZ.BasisY.Negate());

                var wholeProfile3 = new List<Line>();
                wholeProfile3.AddRange(outerProfile);
                wholeProfile3.AddRange(getSequenceStartingWithNotHorizontalLine(getReversedProfile(innerProfile)));

                var movedWholeProfile3 = getMovedLinesAlongX(wholeProfile3, currPosition += step);
                drawLines(doc, movedWholeProfile3);

                var newWall3 = Wall.Create(doc,
                    movedWholeProfile3.Select(l => (Curve)l).ToList(),
                    defaultType,
                    level.Id,
                    false,
                    XYZ.BasisY);

                var flipedWallWholeProfile3 = getMovedLinesAlongX(wholeProfile3, currPosition += step);

                drawLines(doc, flipedWallWholeProfile3);

                Wall.Create(doc,
                    flipedWallWholeProfile3.Select(l => (Curve)l).ToList(),
                    defaultType,
                    level.Id,
                    false,
                    XYZ.BasisY.Negate());

                var wholeProfile4 = new List<Line>();
                wholeProfile4.AddRange(getReversedProfile(outerProfile));
                wholeProfile4.AddRange(getSequenceStartingWithNotHorizontalLine(innerProfile));

                var movedWholeProfile4 = getMovedLinesAlongX(wholeProfile4, currPosition += step);
                drawLines(doc, movedWholeProfile4);

                var newWall4 = Wall.Create(doc,
                    movedWholeProfile4.Select(l => (Curve)l).ToList(),
                    defaultType,
                    level.Id,
                    false,
                    XYZ.BasisY);

                var flipedWallWholeProfile4 = getMovedLinesAlongX(wholeProfile4, currPosition += step);

                drawLines(doc, flipedWallWholeProfile4);

                Wall.Create(doc,
                    flipedWallWholeProfile4.Select(l => (Curve)l).ToList(),
                    defaultType,
                    level.Id,
                    false,
                    XYZ.BasisY.Negate());

                var wholeProfile5 = new List<Line>();
                wholeProfile5.AddRange(outerProfile);
                wholeProfile5.AddRange(getSequenceStartingWithNotHorizontalLine(getMovedLinesAlongY(innerProfile, 10)));
                var movedWholeProfile5 = getMovedLinesAlongX(wholeProfile5, currPosition += step);

                drawLines(doc, wholeProfile5);

                var newWall5 = Wall.Create(doc,
                    movedWholeProfile5.Select(l => (Curve)l).ToList(),
                    defaultType,
                    level.Id,
                    false,
                    XYZ.BasisY);

                var wholeProfile6 = new List<Line>();
                wholeProfile6.AddRange(outerProfile);
                wholeProfile6.AddRange(getSequenceStartingWithNotHorizontalLine(
                    getLineSequenceWithChangedStart(innerProfile, 1)));

                var movedWholeProfile6 = getMovedLinesAlongX(wholeProfile6, currPosition += step);
                drawLines(doc, movedWholeProfile6);

                try
                {
                    var newWall6 = Wall.Create(doc,
                        movedWholeProfile6.Select(l => (Curve)l).ToList(),
                        defaultType,
                        level.Id,
                        false,
                        XYZ.BasisY);
                }
                catch (Exception)
                {
                }

                var wholeProfile7 = new List<Line>();
                wholeProfile7.AddRange(outerProfile);
                wholeProfile7.AddRange(getSequenceStartingWithNotHorizontalLine(
                    getLineSequenceWithChangedStart(innerProfile, 2)));

                var movedWholeProfile7 = getMovedLinesAlongX(wholeProfile7, currPosition += step);
                drawLines(doc, movedWholeProfile7);

                try
                {
                    Wall.Create(doc,
                       movedWholeProfile7.Select(l => (Curve)l).ToList(),
                       defaultType,
                       level.Id,
                       false,
                       XYZ.BasisY);
                }
                catch (Exception)
                {
                }

                var wholeProfile8 = new List<Line>();
                wholeProfile8.AddRange(outerProfile);
                wholeProfile8.AddRange(getSequenceStartingWithNotHorizontalLine(
                    getLineSequenceWithChangedStart(innerProfile, 3)));

                var movedWholeProfile8 = getMovedLinesAlongX(wholeProfile8, currPosition += step);
                drawLines(doc, movedWholeProfile8);

                try
                {
                    Wall.Create(doc,
                        movedWholeProfile8.Select(l => (Curve)l).ToList(),
                        defaultType,
                        level.Id,
                        false,
                        XYZ.BasisY);
                }
                catch (Exception)
                {
                }

                var wholeProfile9 = new List<Line>();
                var innerProfile9 = innerProfile.Select(l => (Line)l.Clone()).ToList();

                innerProfile9 = getLinesRotated(innerProfile9, Math.PI / 4);

                wholeProfile9.AddRange(outerProfile);
                wholeProfile9.AddRange(getSequenceStartingWithNotHorizontalLine(innerProfile9));

                var movedWholeProfile9 = getMovedLinesAlongX(wholeProfile9, currPosition += step);
                drawLines(doc, movedWholeProfile9);

                try
                {
                    Wall.Create(doc,
                        movedWholeProfile9.Select(l => (Curve)l).ToList(),
                        defaultType,
                        level.Id,
                        false,
                        XYZ.BasisY);
                }
                catch (Exception)
                {
                }

                var wholeProfile10 = new List<Line>();
                var innerProfile10 = innerProfile.Select(l => (Line)l.Clone()).ToList();

                innerProfile10 = getLinesRotated(innerProfile10, Math.PI / 4);

                wholeProfile10.AddRange(outerProfile);
                wholeProfile10.AddRange(getSequenceStartingWithNotHorizontalLine(
                    getLineSequenceWithChangedStart(innerProfile10, 1)));

                var movedWholeProfile10 = getMovedLinesAlongX(wholeProfile10, currPosition += step);
                drawLines(doc, movedWholeProfile10);

                try
                {
                    Wall.Create(doc,
                        movedWholeProfile10.Select(l => (Curve)l).ToList(),
                        defaultType,
                        level.Id,
                        false,
                        XYZ.BasisY);
                }
                catch (Exception)
                {
                }

                var wholeProfile11 = new List<Line>();
                var innerProfile11 = innerProfile.Select(l => (Line)l.Clone()).ToList();

                innerProfile11 = getLinesRotated(innerProfile11, Math.PI / 4);

                wholeProfile11.AddRange(outerProfile);
                wholeProfile11.AddRange(getSequenceStartingWithNotHorizontalLine(
                    getLineSequenceWithChangedStart(innerProfile11, 2)));

                var movedWholeProfile11 = getMovedLinesAlongX(wholeProfile11, currPosition += step);
                drawLines(doc, movedWholeProfile11);

                try
                {
                    Wall.Create(doc,
                        movedWholeProfile11.Select(l => (Curve)l).ToList(),
                        defaultType,
                        level.Id,
                        false,
                        XYZ.BasisY);
                }
                catch (Exception)
                {
                }

                var wholeProfile12 = new List<Line>();
                var innerProfile12 = innerProfile.Select(l => (Line)l.Clone()).ToList();

                innerProfile12 = getLinesRotated(innerProfile12, Math.PI / 4);

                wholeProfile12.AddRange(outerProfile);
                wholeProfile12.AddRange(getSequenceStartingWithNotHorizontalLine(
                    getLineSequenceWithChangedStart(innerProfile12, 3)));

                var movedWholeProfile12 = getMovedLinesAlongX(wholeProfile12, currPosition += step);
                drawLines(doc, movedWholeProfile12);

                try
                {
                    Wall.Create(doc,
                        movedWholeProfile12.Select(l => (Curve)l).ToList(),
                        defaultType,
                        level.Id,
                        false,
                        XYZ.BasisY);
                }
                catch (Exception)
                {
                }


                var wholeProfile13 = new List<Line>();
                var innerProfile13 = innerProfile.Select(l => (Line)l.Clone()).ToList();

                innerProfile13 = getLinesRotated(innerProfile13, Math.PI / 4);
                innerProfile13 = getReversedProfile(innerProfile13);

                wholeProfile13.AddRange(outerProfile);
                wholeProfile13.AddRange(getSequenceStartingWithNotHorizontalLine(
                    getLineSequenceWithChangedStart(innerProfile13, 0)));

                var movedWholeProfile13 = getMovedLinesAlongX(wholeProfile13, currPosition += step);
                drawLines(doc, movedWholeProfile13);

                try
                {
                    Wall.Create(doc,
                        movedWholeProfile13.Select(l => (Curve)l).ToList(),
                        defaultType,
                        level.Id,
                        false,
                        XYZ.BasisY);
                }
                catch (Exception)
                {
                }

                var wholeProfile14 = new List<Line>();
                var innerProfile14 = innerProfile.Select(l => (Line)l.Clone()).ToList();

                innerProfile14 = getLinesRotated(innerProfile14, Math.PI / 4);
                innerProfile14 = getReversedProfile(innerProfile14);

                wholeProfile14.AddRange(outerProfile);
                wholeProfile14.AddRange(getSequenceStartingWithNotHorizontalLine(
                    getLineSequenceWithChangedStart(innerProfile14, 1)));

                var movedWholeProfile14 = getMovedLinesAlongX(wholeProfile14, currPosition += step);
                drawLines(doc, movedWholeProfile14);

                try
                {
                    Wall.Create(doc,
                        movedWholeProfile14.Select(l => (Curve)l).ToList(),
                        defaultType,
                        level.Id,
                        false,
                        XYZ.BasisY);
                }
                catch (Exception)
                {
                }

                var wholeProfile15 = new List<Line>();
                var innerProfile15 = innerProfile.Select(l => (Line)l.Clone()).ToList();

                innerProfile15 = getLinesRotated(innerProfile15, Math.PI / 4);
                innerProfile15 = getReversedProfile(innerProfile15);

                wholeProfile15.AddRange(outerProfile);
                wholeProfile15.AddRange(getSequenceStartingWithNotHorizontalLine(
                    getLineSequenceWithChangedStart(innerProfile15, 2)));

                var movedWholeProfile15 = getMovedLinesAlongX(wholeProfile15, currPosition += step);
                drawLines(doc, movedWholeProfile15);

                try
                {
                    Wall.Create(doc,
                        movedWholeProfile15.Select(l => (Curve)l).ToList(),
                        defaultType,
                        level.Id,
                        false,
                        XYZ.BasisY);
                }
                catch (Exception)
                {
                }

                var wholeProfile16 = new List<Line>();
                var innerProfile16 = innerProfile.Select(l => (Line)l.Clone()).ToList();

                innerProfile16 = getLinesRotated(innerProfile16, Math.PI / 4);
                innerProfile16 = getReversedProfile(innerProfile16);

                wholeProfile16.AddRange(outerProfile);
                wholeProfile16.AddRange(getSequenceStartingWithNotHorizontalLine(
                    getLineSequenceWithChangedStart(innerProfile16, 3)));

                var movedWholeProfile16 = getMovedLinesAlongX(wholeProfile16, currPosition += step);
                drawLines(doc, movedWholeProfile16);

                try
                {
                    Wall.Create(doc,
                        movedWholeProfile16.Select(l => (Curve)l).ToList(),
                        defaultType,
                        level.Id,
                        false,
                        XYZ.BasisY);
                }
                catch (Exception)
                {
                }

                var wholeProfile17 = new List<Line>();

                wholeProfile17.AddRange(outerProfile);
                wholeProfile17.AddRange(getSequenceStartingWithNotHorizontalLine(
                    getLineSequenceWithChangedStart(innerProfileTriangle, 0)));

                var movedWholeProfile17 = getMovedLinesAlongX(wholeProfile17, currPosition += step);
                drawLines(doc, movedWholeProfile17);

                try
                {
                    Wall.Create(doc,
                        movedWholeProfile17.Select(l => (Curve)l).ToList(),
                        defaultType,
                        level.Id,
                        false,
                        XYZ.BasisY);
                }
                catch (Exception)
                {
                }

                var wholeProfile18 = new List<Line>();

                wholeProfile18.AddRange(outerProfile);
                wholeProfile18.AddRange(getSequenceStartingWithNotHorizontalLine(
                    getLineSequenceWithChangedStart(innerProfileTriangle, 1)));

                var movedWholeProfile18 = getMovedLinesAlongX(wholeProfile18, currPosition += step);
                drawLines(doc, movedWholeProfile18);

                try
                {
                    Wall.Create(doc,
                        movedWholeProfile18.Select(l => (Curve)l).ToList(),
                        defaultType,
                        level.Id,
                        false,
                        XYZ.BasisY);
                }
                catch (Exception)
                {
                }

                var wholeProfile19 = new List<Line>();

                wholeProfile19.AddRange(outerProfile);
                wholeProfile19.AddRange(getSequenceStartingWithNotHorizontalLine(
                    getLineSequenceWithChangedStart(innerProfileTriangle, 2)));

                var movedWholeProfile19 = getMovedLinesAlongX(wholeProfile19, currPosition += step);
                drawLines(doc, movedWholeProfile19);

                try
                {
                    Wall.Create(doc,
                        movedWholeProfile19.Select(l => (Curve)l).ToList(),
                        defaultType,
                        level.Id,
                        false,
                        XYZ.BasisY);
                }
                catch (Exception)
                {
                }

                var wholeProfile20 = new List<Line>();

                wholeProfile20.AddRange(outerProfile);
                wholeProfile20.AddRange(getSequenceStartingWithNotHorizontalLine(
                    getLineSequenceWithChangedStart(innerProfileTriangle, 3)));

                var movedWholeProfile20 = getMovedLinesAlongX(wholeProfile20, currPosition += step);
                drawLines(doc, movedWholeProfile20);

                try
                {
                    Wall.Create(doc,
                        movedWholeProfile20.Select(l => (Curve)l).ToList(),
                        defaultType,
                        level.Id,
                        false,
                        XYZ.BasisY);
                }
                catch (Exception)
                {
                }

                for (int i = 0; i < innerProfileCoplexWithHorizontalLines.Count; i++)
                {
                    var wholeProfile21 = new List<Line>();

                    wholeProfile21.AddRange(outerProfile);
                    wholeProfile21.AddRange(getSequenceStartingWithNotHorizontalLine(
                        getLineSequenceWithChangedStart(innerProfileCoplexWithHorizontalLines, i)));

                    var movedWholeProfile21 = getMovedLinesAlongX(wholeProfile21, currPosition += step);
                    drawLines(doc, movedWholeProfile21);

                    try
                    {
                        Wall.Create(doc,
                            movedWholeProfile21.Select(l => (Curve)l).ToList(),
                            defaultType,
                            level.Id,
                            false,
                            XYZ.BasisY);
                    }
                    catch (Exception)
                    {
                    }
                }

                for (int i = 0; i < innerProfileCoplexWithHorizontalLines.Count; i++)
                {
                    var wholeProfile22 = new List<Line>();

                    wholeProfile22.AddRange(outerProfile);
                    wholeProfile22.AddRange(getSequenceStartingWithNotHorizontalLine(
                        getLineSequenceWithChangedStart(innerProfileCoplexWithoutHorizontalLines, i)));

                    var movedWholeProfile22 = getMovedLinesAlongX(wholeProfile22, currPosition += step);
                    drawLines(doc, movedWholeProfile22);

                    try
                    {
                        Wall.Create(doc,
                            movedWholeProfile22.Select(l => (Curve)l).ToList(),
                            defaultType,
                            level.Id,
                            false,
                            XYZ.BasisY);
                    }
                    catch (Exception)
                    {
                    }
                }

                for (int i = 0; i < innerProfileCoplexWithHorizontalLines.Count; i++)
                {
                    var wholeProfile22 = new List<Line>();

                    wholeProfile22.AddRange(getReversedProfile(outerProfile));
                    wholeProfile22.AddRange(getSequenceStartingWithNotHorizontalLine(
                        getLineSequenceWithChangedStart(innerProfileCoplexWithoutHorizontalLines, i)));

                    var movedWholeProfile22 = getMovedLinesAlongX(wholeProfile22, currPosition += step);
                    drawLines(doc, movedWholeProfile22);

                    try
                    {
                        Wall.Create(doc,
                            movedWholeProfile22.Select(l => (Curve)l).ToList(),
                            defaultType,
                            level.Id,
                            false,
                            XYZ.BasisY);
                    }
                    catch (Exception)
                    {
                    }
                }

                for (int i = 0; i < innerProfileRectanglEdgeContains2Lines.Count; i++)
                {
                    var wholeProfile23 = new List<Line>();

                    wholeProfile23.AddRange(outerProfile);
                    wholeProfile23.AddRange(getSequenceStartingWithNotHorizontalLine(
                        getLineSequenceWithChangedStart(innerProfileRectanglEdgeContains2Lines, i)));

                    var movedWholeProfile23 = getMovedLinesAlongX(wholeProfile23, currPosition += step);
                    drawLines(doc, movedWholeProfile23);

                    try
                    {
                        Wall.Create(doc,
                            movedWholeProfile23.Select(l => (Curve)l).ToList(),
                            defaultType,
                            level.Id,
                            false,
                            XYZ.BasisY);
                    }
                    catch (Exception)
                    {
                    }
                }

                for (int i = 0; i < innerProfileRectanglEdgeContains3Lines.Count; i++)
                {
                    var wholeProfile23 = new List<Line>();

                    wholeProfile23.AddRange(outerProfile);
                    var innerProfile23 = getLineSequenceWithChangedStart(innerProfileRectanglEdgeContains3Lines, i);
                    innerProfile23 = getSequenceStartingWithNotHorizontalLine(innerProfile23);
                    wholeProfile23.AddRange(innerProfile23);

                    var movedWholeProfile23 = getMovedLinesAlongX(wholeProfile23, currPosition += step);
                    drawLines(doc, movedWholeProfile23);

                    try
                    {
                        Wall.Create(doc,
                            movedWholeProfile23.Select(l => (Curve)l).ToList(),
                            defaultType,
                            level.Id,
                            false,
                            XYZ.BasisY);
                    }
                    catch (Exception)
                    {
                    }
                }
                tran.Commit();

            }

            return Autodesk.Revit.UI.Result.Succeeded;
        }

        private List<Line> getSequenceStartingWithNotHorizontalLine(List<Line> lines)
        {
            var result = new List<Line>();

            var isPrevHorizontal = false;
            var idx = int.MinValue;
            for (int i = 0; i < lines.Count; i++)
            {
                var line = lines[i];

                if ((line.Direction.IsAlmostEqualTo(XYZ.BasisX)
                    || line.Direction.IsAlmostEqualTo(XYZ.BasisX.Negate())))
                {
                    isPrevHorizontal = true;
                }
                else
                {
                    if (isPrevHorizontal)
                    {
                        idx = i;
                        break;
                    }
                }
            }

            if (idx != int.MinValue)
            {
                result = getLineSequenceWithChangedStart(lines, idx);
            }
            else
            {
                result.AddRange(lines);
            }

            return result;
        }

        private List<Line> getLinesRotated(List<Line> innerProfile9, double angle)
        {
            var result = new List<Line>();

            var startPoints = innerProfile9.Select(l => l.GetEndPoint(0)).ToList();
            var endPoints = innerProfile9.Select(l => l.GetEndPoint(1)).ToList();
            var allPoints = new List<XYZ>();
            allPoints.AddRange(startPoints);
            allPoints.AddRange(endPoints);

            var minX = allPoints.Min(p => p.X);
            var maxX = allPoints.Max(p => p.X);
            var minZ = allPoints.Min(p => p.Z);
            var maxZ = allPoints.Max(p => p.Z);

            var middleX = (maxX - minX) / 2 + minX;
            var middleZ = (maxZ - minZ) / 2 + minZ;

            var transform = Transform.CreateRotationAtPoint(XYZ.BasisY,
                angle,
                new XYZ(middleX, innerProfile9.First().GetEndPoint(0).Y, middleZ));

            foreach (var line in innerProfile9)
            {
                var lineTransformed = (Line)line.CreateTransformed(transform);

                result.Add(lineTransformed);
            }

            return result;
        }

        private Line getProfileOutline(List<Line> innerProfile9)
        {
            throw new NotImplementedException();
        }

        private List<Line> getLineSequenceWithChangedStart(List<Line> lines, int newStartLineIndex)
        {
            var result = new List<Line>();
            var nextLines = new Queue<Line>();

            for (int i = 0; i < lines.Count; i++)
            {
                if (i >= newStartLineIndex)
                {
                    result.Add(lines[i]);
                }
                else
                {
                    nextLines.Enqueue(lines[i]);
                }
            }

            while (nextLines.Count > 0)
            {
                result.Add(nextLines.Dequeue());
            }

            return result;
        }

        private List<Line> getReversedProfile(List<Line> profile)
        {
            var result = getReversedLines(profile);
            result = getReversedLinesBySequence(result);

            return result;
        }

        private List<Line> getReversedLinesBySequence(List<Line> lines)
        {
            var result = new List<Line>();

            result.Add(lines[0]);

            for (int i = 1; i < lines.Count; i++)
                result.Insert(1, lines[i]);

            return result;
        }

        private void drawLines(Document doc, List<Line> lines)
        {
            var defaultNoteTypeId = doc.GetDefaultElementTypeId(ElementTypeGroup.TextNoteType);

            bool isReset = false;
            var counter = 0;
            for (int i = 0; i < lines.Count; i++)
            {
                var line = lines[i];

                var plane = new Plane(XYZ.BasisY.Negate(), line.GetEndPoint(0));
                var sketchPlane = SketchPlane.Create(doc, plane);

                var noteOptions = new TextNoteOptions()
                {
                    HorizontalAlignment = HorizontalTextAlignment.Center,
                    TypeId = defaultNoteTypeId
                };

                var no = TextNote.Create(doc,
                    doc.ActiveView.Id,
                    line.GetEndPoint(0) + line.Direction * line.Length / 2,
                    counter.ToString(),
                    noteOptions);

                no.Width = no.Width / 2;

                var direction = TextNote.Create(doc,
                    doc.ActiveView.Id,
                    line.GetEndPoint(1) - line.Direction * line.Length * 0.05,
                    "X",
                    noteOptions);

                doc.Create.NewModelCurve(line, sketchPlane);

                if (counter == 3 && !isReset)
                {
                    counter = 0;
                    isReset = true;
                }
                else
                    counter++;
            }
        }

        private List<Line> getReversedLines(List<Line> lines)
        {
            var result = new List<Line>();
            foreach (var line in lines)
            {
                result.Add((Line)line.CreateReversed());
            }

            return result;
        }

        private List<Line> getMovedLinesAlongX(List<Line> lines, double step)
        {
            return getMovedLines(lines, new XYZ(step, 0, 0));
        }

        private List<Line> getMovedLinesAlongY(List<Line> lines, double step)
        {
            return getMovedLines(lines, new XYZ(0, step, 0));
        }

        private List<Line> getMovedLines(List<Line> lines, XYZ step)
        {
            var result = new List<Line>();
            foreach (var line in lines)
            {
                var movedLine = getMovedLine(line, step);
                result.Add(movedLine);
            }

            return result;
        }

        private Line getMovedLine(Line line, XYZ step)
        {
            return Line.CreateBound(line.GetEndPoint(0) + step, line.GetEndPoint(1) + step);
        }



    }
}
