using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using University1;
using University1.Models;
using System.Threading.Tasks;
using System.Web.Security;

namespace University1.Controllers
{
    [Authorize(Roles = "Admin")]
    public class TeachersController : Controller
    {
        private universityEntities db = new universityEntities();
        private ApplicationDbContext localDbContextdb = new ApplicationDbContext();

        // GET: Teachers
        public ActionResult Index()
        {
            return View(db.Teacher.ToList());
        }

        // GET: Teachers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Teacher teacher = db.Teacher.Find(id);
            if (teacher == null)
            {
                return HttpNotFound();
            }
            return View(teacher);
        }

        // GET: Teachers/Create
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

        // POST: Teachers/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name,Telephone,UserID")] Teacher teacher)
        {
            ApplicationUserManager _app = Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            
            if (ModelState.IsValid)
            {
                db.Teacher.Add(teacher);
                db.SaveChanges();
                await _app.AddToRoleAsync(teacher.UserID, "Teacher");
                return RedirectToAction("Index");
            }

            return View(teacher);
        }

        // GET: Teachers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Teacher teacher = db.Teacher.Find(id);
            if (teacher == null)
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
            return View(teacher);
        }

        // POST: Teachers/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,Telephone,UserID")] Teacher teacher)
        {
            ApplicationUserManager _app = Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            if (ModelState.IsValid)
            {
                db.Entry(teacher).State = EntityState.Modified;
                db.SaveChanges();
                IList<string> listRoles = await _app.GetRolesAsync(teacher.UserID);
                await _app.RemoveFromRolesAsync(teacher.UserID, listRoles.ToArray());
                await _app.AddToRoleAsync(teacher.UserID, "Teacher");
                return RedirectToAction("Index");
            }
            return View(teacher);
        }

        // GET: Teachers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Teacher teacher = db.Teacher.Find(id);
            if (teacher == null)
            {
                return HttpNotFound();
            }
            return View(teacher);
        }

        // POST: Teachers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                Teacher teacher = db.Teacher.Find(id);
                db.Teacher.Remove(teacher);
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
