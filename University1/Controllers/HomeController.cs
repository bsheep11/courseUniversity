using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace University1.Controllers
{
    public class HomeController : Controller
    {
        private universityEntities db = new universityEntities();
        public ActionResult Index()
        {
            try
            {
                var listStudents = db.Student.ToList();
                string user = User.Identity.GetUserId();
                foreach (var student in listStudents)
                {
                    if (student.UserID == user)
                    {
                        Session["Student"] = student;
                        ViewBag.studentId = student.Id;
                        break;
                    }
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}