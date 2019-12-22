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
    [Authorize(Roles = "Admin, Teacher")]
    public class ListAllTestsController : Controller
    {
        private universityEntities db = new universityEntities();

        // GET: ListAllTests
        public async Task<ActionResult> Index()
        {
            var test = db.Test.Include(t => t.Сourse);
            return View(await test.ToListAsync());
        }

        // GET: ListAllTests/Details/5
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

        // GET: ListAllTests/Create
        public ActionResult Create()
        {
            ViewBag.CourseID = new SelectList(db.Сourse, "Id", "Name");
            return View();
        }

        // POST: ListAllTests/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,TestName,CourseID")] Test test)
        {
            if (ModelState.IsValid)
            {
                db.Test.Add(test);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.CourseID = new SelectList(db.Сourse, "Id", "Name", test.CourseID);
            return View(test);
        }

        // GET: ListAllTests/Edit/5
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

        // POST: ListAllTests/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,TestName,CourseID")] Test test)
        {
            if (ModelState.IsValid)
            {
                db.Entry(test).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.CourseID = new SelectList(db.Сourse, "Id", "Name", test.CourseID);
            return View(test);
        }

        // GET: ListAllTests/Delete/5
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

        // POST: ListAllTests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Test test = await db.Test.FindAsync(id);
            db.Test.Remove(test);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
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
