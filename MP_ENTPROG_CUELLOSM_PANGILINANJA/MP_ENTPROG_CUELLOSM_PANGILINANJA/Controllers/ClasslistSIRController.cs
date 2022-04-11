using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MP_ENTPROG_CUELLOSM_PANGILINANJA.Models;

namespace MP_ENTPROG_CUELLOSM_PANGILINANJA.Controllers
{
    public class ClasslistSIRController : Controller
    {
        private Classlist head = new Classlist();
        public ActionResult List()
        {
            return View(head.ViewAllWithDetails());
        }
        public ActionResult Add()
        {
            Session["Seminar"] = head;
            return View();
        }

        [HttpPost]
        public ActionResult Add(Classlist model)
        {
            Classlist head = (Classlist)Session["Classlist"];
            head.ClassDays = model.ClassDays;
            head.VenueID = model.VenueID;
            head.SubjectID = model.SubjectID;
            head.FacultyID = model.FacultyID;
            head.Status = model.Status;
            head.Add();

            return RedirectToAction("List");
        }

        [HttpPost]
        public ActionResult AddStudent(FormCollection form)
        {
            Classlist head = (Classlist)Session["Classlist"];

            ClasslistDetail dtl = new ClasslistDetail();
            dtl.ClasslistID = int.Parse(form["ClasslistID"]);
            dtl.StudentID = int.Parse(form["Form"]);

            head.ClasslistItems.Add(dtl);

            Session["Classlist"] = head;

            return PartialView("_AddClasslistDtl", head);
        }
    }
}