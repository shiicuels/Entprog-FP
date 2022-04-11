using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MP_ENTPROG_CUELLOSM_PANGILINANJA.Models;

namespace MP_ENTPROG_CUELLOSM_PANGILINANJA.Controllers
{
    public class VenueController : Controller
    {
        private Venue obj = new Venue();
        public ActionResult List()
        {
            return View(obj.ViewAllVenue());
        }

        public ActionResult Add()
        {
            try
            {
                return View(new Venue());
            }
            catch (Exception ex)
            {
                return View("ErrorPage", ex);
            }
        }

        [HttpPost]
        public ActionResult Add(Venue model)
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
                obj = obj.Get(id);
                return View(obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public ActionResult Edit(Venue model)
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
            obj.VenueName = form["VenueName"];
            obj.Building = form["Building"];
            return View(obj.Search());
        }
    }
}