using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MP_ENTPROG_CUELLOSM_PANGILINANJA.Models;

namespace MP_ENTPROG_CUELLOSM_PANGILINANJA.Controllers
{
    public class UserController : Controller
    {
        private User user = new User();
        
        public ActionResult List()
        {
            try
            {
                if (Session["User"] == null)
                {
                    return RedirectToAction("Index", "User");
                }

                User x = (User)Session["User"];
                if (x.UserLevel == "Administrator")
                {
                    return View(user.ViewAll());
                }
                else
                {
                    Exception ex = new Exception("Only the Admin is allowed to view this page");
                    ex.Source = "System User";
                    throw ex;
                }
            }
            catch (Exception ex)
            {
                return View("ErrorPage", ex);
            }
        }

        public ActionResult Add()
        {
            try
            {
                if (Session["User"] == null)
                {
                    return RedirectToAction("Index", "User");
                }

                User x = (User)Session["User"];
                if (x.UserLevel == "Administrator")
                {
                    return View(new User());
                }
                else if(x.UserLevel == "Chairperson")
                {
                    return RedirectToAction("AddStudentFaculty", "User");
                }
                else
                {
                    Exception ex = new Exception("Only the Admin or Chairperson could add a record on this section");
                    ex.Source = "System User";
                    throw ex;
                }
            }
            catch (Exception ex)
            {
                return View("ErrorPage", ex);
            }

        }

        [HttpPost]
        public ActionResult Add(User model)
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
            User user = new User();
            user = user.Get(id);

            return View(user);
        }

        [HttpPost]
        public ActionResult Edit(User model)
        {
            try
            {
                if (model.Password != model.ConfirmPassword)
                {
                    Exception ex = new Exception("Your password do not match!");
                    ex.Source = "System Log-In";
                    throw ex;
                }
                model.Edit();
                return RedirectToAction("List");
            }
            catch (Exception ex)
            {
                return View("ErrorPage", ex);
            }
        }
        public ActionResult Me()
        {
            User x = (User)Session["User"];
            return View(user.ViewMe(x.ID));
        }

        public ActionResult EditMe(int id)
        {
            User user = new User();
            user = user.Get(id);

            return View(user);
        }

        [HttpPost]
        public ActionResult EditMe(User model)
        {
            try
            {
                if (model.Password != model.ConfirmPassword)
                {
                    Exception ex = new Exception("Your password do not match!");
                    ex.Source = "Edit: My Account";
                    throw ex;
                }
                model.Edit();
                return View("Successful");
            }
            catch (Exception ex)
            {
                return View("ErrorPage", ex);
            }
        }

        public ActionResult Delete(int id)
        {
            user = user.Get(id);
            return View(user);
        }
        [HttpPost]
        public ActionResult Delete(User model)
        {
            model.Delete();
            return RedirectToAction("List");
        }
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(User model)
        {
            try
            {
                User obj = new User();
                User account = model.Get(model.Username, model.Password);
                if (account.Username == null)
                {
                    Exception ex = new Exception("You cannot Log In with what you have entered");
                    ex.Source = "System Log-In";
                    throw ex;
                }
                else
                {
                    //Valid Account
                    Session["User"] = account;
                }

                User x = (User)Session["User"];
                if (x.UserLevel == "Administrator")
                {
                    return RedirectToAction("List");
                }
                else
                {
                    return RedirectToAction("Home");
                }

            }
            catch (Exception ex)
            {

                return View("ErrorPage", ex);
            }

        }

        public ActionResult Home()
        {
            User x = (User)Session["User"];
            if (x.UserLevel == "Chairperson")
            {
                return View("Chairperson");
            }
            else if (x.UserLevel == "Faculty")
            {
                return View("Faculty");
            }
            else if (x.UserLevel == "Administrator")
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View("Student");
            }
        }

        //STUDENT
        public ActionResult AddStudentFaculty()
        {
            return View(new User());
        }
        [HttpPost]
        public ActionResult AddStudentFaculty(User model)
        {
            try
            {
                model.Add();
                if (model.UserLevel == "Student")
                {
                    return RedirectToAction("List", "Student");
                }
                return RedirectToAction("List", "Faculty");
            }
            catch (Exception ex)
            {
                return View("ErrorPage", ex);
            }

        }


    }
}