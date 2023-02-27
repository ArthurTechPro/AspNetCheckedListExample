using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using AspNetCheckedListExample.Models;

namespace AspNetCheckedListExample.Controllers
{
    public class PeopleController : Controller
    {
        TestData Db = new TestData();
        IEnumerable<int> items = new List<int>();

        public ActionResult Index()
        {
            var model = new PeopleSelectionViewModel();
            foreach(var person in Db.People)
            {
                var editorViewModel = new SelectPersonEditorViewModel()
                {
                    Id = person.Id,
                    Name = string.Format("{0} {1}", person.firstName, person.LastName),
                    Selected = true
                };
                model.People.Add(editorViewModel);
            }
            return View(model);
        }


        [HttpPost]
        public ActionResult SubmitSelected(PeopleSelectionViewModel model, string comentarioR)
        {
            // get the ids of the items selected:
            var selectedIds = model.getSelectedIds();

            items = selectedIds;
            // Use the ids to retrieve the records for the selected people
            // from the database:
            var selectedPeople = from x in Db.People
                                 where selectedIds.Contains(x.Id)
                                 select x;

            // Process according to your requirements:
            foreach(var person in selectedPeople)
            {
                System.Diagnostics.Debug.WriteLine(
                    string.Format("{0} {1}", person.firstName, person.LastName));
            }

            // Redirect somewhere meaningful (probably to somewhere showing 
            // the results of your processing):
            return RedirectToAction("Index");
            //return PartialView("_EditarPartial");
        }

        public ActionResult Editar()
        {
            var persona = items;
            return PartialView("_editar");
        }
    }
}