using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MP_ENTPROG_CUELLOSM_PANGILINANJA.Models;

namespace MP_ENTPROG_CUELLOSM_PANGILINANJA.Controllers
{
    public class StudentController : Controller
    {
        private Student stu = new Student();
        public ActionResult List()
        {
            return View(stu.ViewAllStudents());
        }

        public ActionResult Add()
        {
            return RedirectToAction("Add", "User");
        }

        public ActionResult Edit(int id)
        {
            Student obj = new Student();
            obj = obj.Get(id);
            return View(obj);
        }

        [HttpPost]
        public ActionResult Edit(Student model)
        {
            model.Edit();
            return RedirectToAction("List");
        }

        [HttpPost]
        public ActionResult Search(FormCollection form)
        {
            stu.Lastname = form["Lastname"];
            stu.Firstname = form["Firstname"];

            return View(stu.Search());
        }

        [HttpPost]
        public ActionResult SearchID(FormCollection form)
        {
            stu.ID = int.Parse(form["ID"]);
            return View(stu.SearchID());
        }

    }
}