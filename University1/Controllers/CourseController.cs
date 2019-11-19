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
    public class CourseController : Controller
    {
        private universityEntities db = new universityEntities();

        // GET: Course
        public async Task<ActionResult> Index()
        {
            var Course = db.Сourse.Include(с => с.Teacher);
            return View(await Course.ToListAsync());
        }

        // GET: Сourse/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Сourse Course = await db.Сourse.FindAsync(id);
            if (Course == null)
            {
                return HttpNotFound();
            }
            return View(Course);
        }

        // GET: Сourse/Create
        public ActionResult Create()
        {
            ViewBag.TeacherID = new SelectList(db.Teacher, "Id", "Name");
            return View();
        }

        // POST: Сourse/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name,TeacherID")] Сourse Course)
        {
            if (ModelState.IsValid)
            {
                db.Сourse.Add(Course);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.TeacherID = new SelectList(db.Teacher, "Id", "Name", Course.TeacherID);
            return View(Course);
        }

        // GET: Сourse/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Сourse Course = await db.Сourse.FindAsync(id);
            if (Course == null)
            {
                return HttpNotFound();
            }
            ViewBag.TeacherID = new SelectList(db.Teacher, "Id", "Name", Course.TeacherID);
            return View(Course);
        }

        // POST: Сourse/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,TeacherID")] Сourse Course)
        {
            if (ModelState.IsValid)
            {
                db.Entry(Course).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.TeacherID = new SelectList(db.Teacher, "Id", "Name", Course.TeacherID);
            return View(Course);
        }

        // GET: Сourse/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Сourse Course = await db.Сourse.FindAsync(id);
            if (Course == null)
            {
                return HttpNotFound();
            }
            return View(Course);
        }

        // POST: Сourse/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Сourse Course = await db.Сourse.FindAsync(id);
            db.Сourse.Remove(Course);
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
