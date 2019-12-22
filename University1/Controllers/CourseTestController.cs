using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using University1;

namespace University1.Controllers
{
    [Authorize(Roles = "Admin, Teacher, Student")]
    public class CourseTestController : Controller
    {
        private universityEntities db = new universityEntities();

        // GET: Tests
        public async Task<ActionResult> Index(int id)
        {
            var test = db.Test.Include(t => t.Сourse)
                    .Where(t => t.CourseID == id);
                return View(await test.ToListAsync());
            
            
        }

        // GET: Tests/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Test test = await db.Test.FindAsync(id);
            if (test == null)
            {
                return HttpNotFound();
            }
            return View(test);
        }

        // GET: Tests/Create
        [Authorize(Roles = "Admin, Teacher")]
        public ActionResult Create()
        {
            ViewBag.CourseID = new SelectList(db.Сourse, "Id", "Name");
            return View();
        }

        // POST: Tests/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin, Teacher")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,TestName,CourseID")] Test test)
        {
            if (ModelState.IsValid)
            {
                db.Test.Add(test);
                await db.SaveChangesAsync();
                return RedirectToAction("Index", new {id = test.CourseID});
            }

            ViewBag.CourseID = new SelectList(db.Сourse, "Id", "Name", test.CourseID);
            return View(test);
        }

        // GET: Tests/Edit/5
        [Authorize(Roles = "Admin, Teacher")]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Test test = await db.Test.FindAsync(id);
            if (test == null)
            {
                return HttpNotFound();
            }
            ViewBag.CourseID = new SelectList(db.Сourse, "Id", "Name", test.CourseID);
            return View(test);
        }

        // POST: Tests/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin, Teacher")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,TestName,CourseID")] Test test)
        {
            if (ModelState.IsValid)
            {
                db.Entry(test).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index", new { id = test.CourseID });
            }
            ViewBag.CourseID = new SelectList(db.Сourse, "Id", "Name", test.CourseID);
            return View(test);
        }

        // GET: Tests/Delete/5
        [Authorize(Roles = "Admin, Teacher")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Test test = await db.Test.FindAsync(id);
            if (test == null)
            {
                return HttpNotFound();
            }
            return View(test);
        }

        // POST: Tests/Delete/5
        [Authorize(Roles = "Admin, Teacher")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Test test = await db.Test.FindAsync(id);
            db.Test.Remove(test);
            await db.SaveChangesAsync();
            return RedirectToAction("Index", new { id = test.CourseID });
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
