using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MP_ENTPROG_CUELLOSM_PANGILINANJA.Models;

namespace MP_ENTPROG_CUELLOSM_PANGILINANJA.Controllers
{
    public class AttendanceController : Controller
    {
        private Attendance obj = new Attendance();
        public ActionResult AddAttendance()
        {
            try
            {
                if (Session["User"] == null)
                {
                    return RedirectToAction("Index", "User");
                }

                User x = (User)Session["User"];
                if (x.UserLevel != "Faculty")
                {
                    Exception ex = new Exception("Only faculty can add an attendance record");
                    ex.Source = "System User";
                    throw ex;
                }

                Attendance a = new Attendance();
                Session["At"] = a;

                return View(new Attendance());
            }
            catch (Exception ex)
            {
                return View("ErrorPage", ex);
            }
        }
        [HttpPost]
        public PartialViewResult AddDetail(FormCollection form)
        {
            Attendance a = (Attendance)Session["At"];
            List<ClasslistDetail> list = (List<ClasslistDetail>)Session["Attendance"];
            AttendanceDetail con = new AttendanceDetail();

            con.Student = form["Student"];
            con.Status = form["Status"];
            a.AttendanceItems.Add(con);

            Session["At"] = a;

            return PartialView("_AddDetails", a);
        }
        [HttpPost]
        public ActionResult AddAttendance(Attendance model)
        {
            Attendance o = (Attendance)Session["At"];
            o.ClasslistID = model.ClasslistID;
            o.Date = model.Date;
            o.Add();

            Classlist cl = (Classlist)Session["ClasslistHeader"];

            return RedirectToAction("ViewAttendances", "Classlist", new { id = cl.ID });
        }
        public ActionResult MyAttendance()
        {
            try
            {
                if (Session["User"] == null)
                {
                    return RedirectToAction("Index", "User");
                }

                User x = (User)Session["User"];
                if (x.UserLevel != "Faculty")
                {
                    Exception ex = new Exception("Only faculties can edit an attendance");
                    ex.Source = "System User";
                    throw ex;
                }

                List<Attendance> attendances = (List<Attendance>)Session["MyAttendances"];

                return View(attendances);
            }
            catch (Exception ex)
            {
                return View("ErrorPage", ex);
            }
        }
        public ActionResult EditAnAttendance(int id)
        {
            try
            {
                if (Session["User"] == null)
                {
                    return RedirectToAction("Index", "User");
                }

                User x = (User)Session["User"];
                if (x.UserLevel != "Faculty")
                {
                    Exception ex = new Exception("Only faculties can edit an attendance");
                    ex.Source = "System User";
                    throw ex;
                }
                Attendance att = new Attendance();
                List<AttendanceDetail> list = new List<AttendanceDetail>();
                list = att.ViewAttendees(id);
                att = att.GetDate(id);

                Session["AttendanceDate"] = att;

                return View(list);
            }
            catch (Exception ex)
            {
                return View("ErrorPage", ex);
            }
        }
        public ActionResult EditStatus(int id)
        {
            try
            {
                if (Session["User"] == null)
                {
                    return RedirectToAction("Index", "User");
                }

                User x = (User)Session["User"];
                if (x.UserLevel != "Faculty")
                {
                    Exception ex = new Exception("Only faculties can edit an attendee's status");
                    ex.Source = "System User";
                    throw ex;
                }

                AttendanceDetail ad = new AttendanceDetail();
                ad = ad.GetID(id);
                Session["ad"] = ad;

                return View(ad);
            }
            catch (Exception ex)
            {
                return View("ErrorPage", ex);
            }
        }
        [HttpPost]
        public ActionResult EditStatus(AttendanceDetail model)
        {
            model.Edit();
            Attendance att = (Attendance)Session["AttendanceDate"];

            return RedirectToAction("EditAnAttendance", new { id = att.ID });
        }
        public ActionResult Void(int id)
        {
            try
            {
                if (Session["User"] == null)
                {
                    return RedirectToAction("Index", "User");
                }

                User x = (User)Session["User"];
                if (x.UserLevel != "Faculty")
                {
                    Exception ex = new Exception("Only faculty can void their attendance");
                    ex.Source = "System User";
                    throw ex;
                }
                obj = obj.GetID(id);

                return View(obj);
            }
            catch (Exception ex)
            {
                return View("ErrorPage", ex);
            }
        }
        [HttpPost]
        public ActionResult Void(Attendance model)
        {
            model.Void(model.ID);
            Classlist cl = (Classlist)Session["ClasslistHeader"];

            return RedirectToAction("ViewAttendances", "Classlist", new { id = cl.ID });
        }
    }
}