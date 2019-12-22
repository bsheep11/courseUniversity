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
    public class PassTheTestsController : Controller
    {
        private universityEntities db = new universityEntities();

        // GET: PassTheTests
        public async Task<ActionResult> Index()
        {
            var passTheTest = db.PassTheTest.Include(p => p.Student).Include(p => p.Test);
            return View(await passTheTest.ToListAsync());
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
