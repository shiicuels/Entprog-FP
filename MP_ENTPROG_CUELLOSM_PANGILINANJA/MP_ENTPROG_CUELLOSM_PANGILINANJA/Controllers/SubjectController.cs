using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MP_ENTPROG_CUELLOSM_PANGILINANJA.Models;

namespace MP_ENTPROG_CUELLOSM_PANGILINANJA.Controllers
{
    public class SubjectController : Controller
    {
        private Subject obj = new Subject();
        public ActionResult List()
        {
            return View(obj.ViewAllSubject());
        }

        public ActionResult Add()
        {
            try
            {
                return View(new Subject());
            }
            catch (Exception ex)
            {
                return View("ErrorPage", ex);
            }

        }

        [HttpPost]
        public ActionResult Add(Subject model)
        {
            try
            {
                model.Add();
                return RedirectToAction("List");
            }
            catch (Exception ex)
            {
                return View("ErrorPage", ex);
            }
        }

        public ActionResult Edit(int id)
        {
            try
            {
                Subject obj = new Subject();
                obj = obj.Get(id);
                return View(obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public ActionResult Edit(Subject model)
        {
            try
            {
                model.Edit();
                return RedirectToAction("List");
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        [HttpPost]
        public ActionResult Search(FormCollection form)
        {
            obj.SubjectCode = form["SubjectCode"];
            return View(obj.Search());
        }


    }
}