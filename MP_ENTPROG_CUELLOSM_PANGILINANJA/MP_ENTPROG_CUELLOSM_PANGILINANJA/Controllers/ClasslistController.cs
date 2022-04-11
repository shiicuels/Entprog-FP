using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MP_ENTPROG_CUELLOSM_PANGILINANJA.Models;

namespace MP_ENTPROG_CUELLOSM_PANGILINANJA.Controllers
{
    public class ClasslistController : Controller
    {
        private Classlist head = new Classlist();
        public ActionResult List()
        {
            return View(head.ViewAll()); //ADD THE DETAILS
        }

        public ActionResult ListFaculty()
        {
            User x = (User)Session["User"];
            head.Faculty = x.Firstname+" "+x.Lastname;
            return View(head.ViewAllofFaculty());
        }

        public ActionResult ListStudent()
        {
            Classlist obj = new Classlist();

            User x = (User)Session["User"];
            string Name = x.Firstname + " " + x.Lastname;
            return View(obj.SelectAllEnrolledSubjects(Name));
        }


        public ActionResult Add()
        {
            Session["Classlist"] = head;
            return View();
        }

        [HttpPost]
        public ActionResult Add(Classlist model)
        {
            Classlist head = (Classlist)Session["Classlist"];
            head.ClassDays = model.ClassDays;
            head.Venue = model.Venue;
            head.Subject = model.Subject;
            head.Faculty = model.Faculty;

            head.Add();
            return RedirectToAction("List");
        }

        [HttpPost]
        public PartialViewResult AddStudent(FormCollection form)
        {
            Classlist head = (Classlist)Session["Classlist"];

            ClasslistDetail dtl = new ClasslistDetail();
            dtl.Student = form["Student"];

            head.ClasslistItems.Add(dtl);

            Session["Classlist"] = head;

            return PartialView("_AddClasslistDetail", head);
        }

        public ActionResult AddMore()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddMoreStudent(FormCollection form)
        {
            ClasslistDetail dtl = new ClasslistDetail();
            dtl.ClasslistID = int.Parse(form["ClasslistID"]);
            dtl.Student = form["Student"];
            dtl.AddMoreStudent();
            return RedirectToAction("List");
        }

        
        public ActionResult Edit(int id)
        {
            try
            {
                head = head.GetID(id);
                Session["Classlist"] = head;
                return View(head);
            }
            catch (Exception ex)
            {

                return View("ErrorPage", ex);
            }
        }

        [HttpPost]
        public ActionResult Edit(Classlist model)
        {
            model.Edit();
            return RedirectToAction("List");
        }

        //Manage Students

        [HttpPost]
        public ActionResult DeleteStudent(FormCollection form)
        {
            ClasslistDetail dtl = new ClasslistDetail();
            dtl.ID = int.Parse(form["ID"]);
            dtl.ClasslistID = int.Parse(form["ClasslistID"]);
            dtl.Student = form["Student"];
            dtl.DeleteStudent();
            return RedirectToAction("List");
        }


        public ActionResult AddMoreStudents()
        {
            return View();
        }


        public ActionResult ManageStudents(int id)
        {
            try
            {
                return View(head.ViewAllDetails(id));
            }
            catch (Exception ex)
            {

                return View("ErrorPage", ex);
            }
        }

        public ActionResult ManageStudentsFaculty(int id)
        {
            try
            {
                return View(head.ViewAllDetails(id));
            }
            catch (Exception ex)
            {

                return View("ErrorPage", ex);
            }
        }



        public ActionResult ViewAttendance()
        {
            return RedirectToAction("List", "Attendance");
        }
        public ActionResult AddAttendance(int id)
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
                    Exception ex = new Exception("Only faculties can record an attendance");
                    ex.Source = "System User";
                    throw ex;
                }

                Classlist cl = new Classlist();
                List<ClasslistDetail> list = new List<ClasslistDetail>();
                list = cl.SelectAllStudentsFaculty(x.Firstname + " " + x.Lastname, id);
                cl = cl.Get(id);

                Session["ClassHeader"] = cl;
                Session["Attendance"] = list;

                return RedirectToAction("AddAttendance", "Attendance");
            }
            catch (Exception ex)
            {
                return View("ErrorPage", ex);
            }
        }
        [HttpPost]
        public ActionResult AddAttendance(Attendance model)
        {
            Attendance o = new Attendance();
            o.ClasslistID = model.ClasslistID;
            o.Date = model.Date;
            o.Add();

            return RedirectToAction("List");
        }
        public ActionResult ViewAttendances(int id)
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
                    Exception ex = new Exception("Only faculties can record an attendance");
                    ex.Source = "System User";
                    throw ex;
                }

                Classlist cl = new Classlist();
                cl = cl.Get(id);
                Session["ClasslistHeader"] = cl;

                Attendance a = new Attendance();
                List<Attendance> attendances = new List<Attendance>();
                attendances = a.ViewMyAttendances(id);
                Session["MyAttendances"] = attendances;

                return RedirectToAction("MyAttendance", "Attendance");
            }
            catch (Exception ex)
            {
                return View("ErrorPage", ex);
            }
        }
    }
}