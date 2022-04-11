using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MP_ENTPROG_CUELLOSM_PANGILINANJA.Models;

namespace MP_ENTPROG_CUELLOSM_PANGILINANJA.Controllers
{
    public class ClasslistDetailController : Controller
    {
        private ClasslistDetail dtl = new ClasslistDetail();

        public ActionResult List()
        {
            User x = (User)Session["User"];
            string student = x.Firstname + " " + x.Lastname;
            return View(dtl.ViewAllofStudent(student));
        }


    }
}