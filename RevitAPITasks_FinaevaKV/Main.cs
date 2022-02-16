using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Mechanical;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitAPITasks_FinaevaKV
{
    [Transaction(TransactionMode.Manual)]
    public class Main : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document doc = uidoc.Document;

            ElementCategoryFilter ductsCategoryFilter = new ElementCategoryFilter(BuiltInCategory.OST_DuctCurves);
            ElementClassFilter ductsClassFilter = new ElementClassFilter(typeof(Duct));

            LogicalAndFilter ductsFilter = new LogicalAndFilter(ductsCategoryFilter, ductsClassFilter);

            var ducts = new FilteredElementCollector(doc)
                .WherePasses(ductsFilter)
                .Cast<Duct>()
                .ToList();

            TaskDialog.Show("DuctsCount", ducts.Count.ToString());

            return Result.Succeeded;
        }
    }
}
