﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using NUnit.Framework.Constraints;
using University1;

namespace University1.Controllers
{
    [Authorize(Roles = "Admin, Teacher, Student")]
    public class RecordsController : Controller
    {
        private universityEntities db = new universityEntities();

        // GET: Records
        public async Task<ActionResult> Index(int? studentId, bool? isStudent)
        {
            if (studentId == null && isStudent==true)
            {
                return RedirectToAction("Index", "Home");
            }
            else if (isStudent==true)
            {
                var record = db.Record.Include(r => r.Student).Include(r => r.Сourse).Where(r => r.StudentID== studentId);
                return View(await record.ToListAsync());
            }
            else
            {
                var record = db.Record.Include(r => r.Student).Include(r => r.Сourse);
                return View(await record.ToListAsync());
            }
        }

        // GET: Records/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Record record = await db.Record.FindAsync(id);
            if (record == null)
            {
                return HttpNotFound();
            }
            return View(record);
        }

        // GET: Records/Create
        [Authorize(Roles = "Admin, Teacher")]
        public ActionResult Create()
        {
            ViewBag.StudentID = new SelectList(db.Student, "Id", "Name");
            ViewBag.CourseID = new SelectList(db.Сourse, "Id", "Name");
            return View();
        }

        // POST: Records/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin, Teacher")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,StudentID,CourseID")] Record record)
        {
            if (ModelState.IsValid)
            {
                db.Record.Add(record);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.StudentID = new SelectList(db.Student, "Id", "Name", record.StudentID);
            ViewBag.CourseID = new SelectList(db.Сourse, "Id", "Name", record.CourseID);
            return View(record);
        }

        // GET: Records/Edit/5
        [Authorize(Roles = "Admin, Teacher")]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Record record = await db.Record.FindAsync(id);
            if (record == null)
            {
                return HttpNotFound();
            }
            ViewBag.StudentID = new SelectList(db.Student, "Id", "Name", record.StudentID);
            ViewBag.CourseID = new SelectList(db.Сourse, "Id", "Name", record.CourseID);
            return View(record);
        }

        // POST: Records/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin, Teacher")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,StudentID,CourseID,Passed")] Record record)
        {
            if (ModelState.IsValid)
            {
                db.Entry(record).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.StudentID = new SelectList(db.Student, "Id", "Name", record.StudentID);
            ViewBag.CourseID = new SelectList(db.Сourse, "Id", "Name", record.CourseID);
            return View(record);
        }

        // GET: Records/Delete/5
        [Authorize(Roles = "Admin, Teacher")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Record record = await db.Record.FindAsync(id);
            if (record == null)
            {
                return HttpNotFound();
            }
            return View(record);
        }

        // POST: Records/Delete/5
        [Authorize(Roles = "Admin, Teacher")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Record record = await db.Record.FindAsync(id);
            db.Record.Remove(record);
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
