using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;

namespace Geometry.Filters
{
    class WallSelectionFilter : ISelectionFilter
    {
        private Document _doc;

        public WallSelectionFilter(Document doc)
        {
            if (doc == null)
                throw new ArgumentNullException("doc");

            _doc = doc;
        }

        public bool AllowElement(Element elem)
        {
            if (elem is Wall)
                return true;

            return false;
        }

        public bool AllowReference(Reference reference, XYZ position)
        {
            var element = _doc.GetElement(reference);

            if (element is Wall)
                return true;

            return false;
        }
    }
}
