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
    public class TestQuestionsController : Controller
    {
        private universityEntities db = new universityEntities();

        // GET: TestQuestions
        public async Task<ActionResult> Index(int? id)
        {
            var testQuestions = db.TestQuestions.Include(t => t.Test)
                .Where(t => t.TestID == id);
            Test test = await db.Test.FindAsync(id);
            ViewBag.testID = id;
            ViewBag.testName = test.TestName;
            return View(await testQuestions.ToListAsync());
        }

        // GET: TestQuestions/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TestQuestions testQuestions = await db.TestQuestions.FindAsync(id);
            if (testQuestions == null)
            {
                return HttpNotFound();
            }
            return View(testQuestions);
        }

        // GET: TestQuestions/Create
        public ActionResult Create(int? id)
        {
            @ViewBag.currID = id;
            ViewBag.TestID = new SelectList(db.Test, "Id", "TestName", id);
            return View();
        }

        // POST: TestQuestions/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Question,TrueAns,Ans2,Ans3,Ans4,TestID")] TestQuestions testQuestions)
        {
            if (ModelState.IsValid)
            {
                db.TestQuestions.Add(testQuestions);
                await db.SaveChangesAsync();
                return RedirectToAction("Index", new { id = testQuestions.TestID });
            }

            ViewBag.TestID = new SelectList(db.Test, "Id", "TestName");
            return View(testQuestions);
        }

        // GET: TestQuestions/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TestQuestions testQuestions = await db.TestQuestions.FindAsync(id);
            if (testQuestions == null)
            {
                return HttpNotFound();
            }
            ViewBag.TestID = new SelectList(db.Test, "Id", "TestName", testQuestions.TestID);
            return View(testQuestions);
        }

        // POST: TestQuestions/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Question,TrueAns,Ans2,Ans3,Ans4,TestID")] TestQuestions testQuestions)
        {
            if (ModelState.IsValid)
            {
                db.Entry(testQuestions).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index",new { id = testQuestions.TestID });
            }
            ViewBag.TestID = new SelectList(db.Test, "Id", "TestName", testQuestions.TestID);
            return View(testQuestions);
        }

        // GET: TestQuestions/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TestQuestions testQuestions = await db.TestQuestions.FindAsync(id);
            if (testQuestions == null)
            {
                return HttpNotFound();
            }
            return View(testQuestions);
        }

        // POST: TestQuestions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            TestQuestions testQuestions = await db.TestQuestions.FindAsync(id);
            db.TestQuestions.Remove(testQuestions);
            await db.SaveChangesAsync();
            return RedirectToAction("Index", new { id = testQuestions.TestID });
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
