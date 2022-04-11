using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MP_ENTPROG_CUELLOSM_PANGILINANJA.Models;

namespace MP_ENTPROG_CUELLOSM_PANGILINANJA.Controllers
{
    public class FacultyController : Controller
    {
        private Faculty obj = new Faculty();
        public ActionResult List()
        {
            return View(obj.ViewAllFaculties());
        }

        public ActionResult Add()
        {
            return RedirectToAction("Add", "User");
        }

        public ActionResult Edit(int id)
        {
            obj = obj.Get(id);
            return View(obj);
        }
        [HttpPost]
        public ActionResult Edit(Faculty model)
        {
            model.Edit();
            return RedirectToAction("List");
        }

        [HttpPost]
        public ActionResult Search(FormCollection form)
        {
            obj.Lastname = form["Lastname"];
            obj.Firstname = form["Firstname"];

            return View(obj.Search());
        }
        [HttpPost]
        public ActionResult SearchID(FormCollection form)
        {
            obj.ID = int.Parse(form["ID"]);
            return View(obj.SearchID());
        }
    }
}