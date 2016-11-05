using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extentions
{
    public static class FloorExtentions
    {
        public static ModelLine GetSpanDirection(this Floor floor)
        {
            var doc = floor.Document;

            using (var tran = new SubTransaction(doc))
            {
                tran.Start();
                var relatedElementIds = doc.Delete(floor.Id);
                tran.RollBack();

                foreach (var id in relatedElementIds)
                {
                    var element = doc.GetElement(id);

                    if (element is ModelLine && element.Name.Contains("Span"))
                        return element as ModelLine;
                }

            }

            return null;
        }
    }
}
