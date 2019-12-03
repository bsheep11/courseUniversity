using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using University1;
using University1.Models;

namespace University1.Controllers
{
    [Authorize(Roles = "Admin")]
    public class StudentsController : Controller
    {
        private universityEntities db = new universityEntities();
        private ApplicationDbContext localDbContextdb = new ApplicationDbContext();

        // GET: Students
        public ActionResult Index()
        {
            return View(db.Student.ToList());
        }

        // GET: Students/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Student.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // GET: Students/Create
        public ActionResult Create()
        {
            var listUsers = localDbContextdb.Users.ToList();
            List<string> listOfNames = new List<string>();
            ApplicationUserManager _app = Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var usr = _app.FindById(User.Identity.GetUserId());
            foreach (var user in listUsers)
            {
                if (user.UserName == usr.UserName)
                { }
                else listOfNames.Add(user.Id);
            }
            ViewBag.UserID = new SelectList(listOfNames);
            return View();
        }

        // POST: Students/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name,Telephone,Age,UserID")] Student student)
        {
            ApplicationUserManager _app = Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            if (ModelState.IsValid)
            {
                db.Student.Add(student);
                db.SaveChanges();
                await _app.AddToRoleAsync(student.UserID, "Student");
                return RedirectToAction("Index");
            }

            return View(student);
        }

        // GET: Students/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Student.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            var listUsers = localDbContextdb.Users.ToList();
            List<string> listOfNames = new List<string>();
            ApplicationUserManager _app = Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var usr = _app.FindById(User.Identity.GetUserId());

            foreach (var user in listUsers)
            {
                if (user.UserName == usr.UserName)
                { }
                else listOfNames.Add(user.Id);
            }
            ViewBag.UserID = new SelectList(listOfNames);
            return View(student);
        }

        // POST: Students/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,Telephone,Age,UserID")] Student student)
        {
            ApplicationUserManager _app = Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            if (ModelState.IsValid)
            {
                db.Entry(student).State = EntityState.Modified;
                db.SaveChanges();
                IList<string> listRoles = await _app.GetRolesAsync(student.UserID);
                await _app.RemoveFromRolesAsync(student.UserID, listRoles.ToArray());
                await _app.AddToRoleAsync(student.UserID, "Teacher");
                return RedirectToAction("Index");
            }
            return View(student);
        }

        // GET: Students/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Student.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                Student student = db.Student.Find(id);
                db.Student.Remove(student);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                return RedirectToAction("Index");
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
