using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GradeRegZTP.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace GradeRegZTP.Controllers
{
    public class SubjectStudentGroupTeachersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: SubjectStudentGroupTeachers
        public ActionResult Index()
        {
            var subjectStudentGroupTeacher = db.SubjectStudentGroupTeacher.Include(s => s.StudentsGroup).Include(s => s.Subject).ToList();

            foreach (var item in subjectStudentGroupTeacher)
            {
                var user = db.MyUsers.Where(x => x.Owner.Equals(item.TeacherID)).FirstOrDefault();
                if (user != null)
                {
                    item.TeacherID = user.Name + " " + user.Surname;
                }
            }

            return View(subjectStudentGroupTeacher.ToList());
        }

        // GET: SubjectStudentGroupTeachers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubjectStudentGroupTeacher subjectStudentGroupTeacher = db.SubjectStudentGroupTeacher.Find(id);
            if (subjectStudentGroupTeacher == null)
            {
                return HttpNotFound();
            }
            return View(subjectStudentGroupTeacher);
        }

        // GET: SubjectStudentGroupTeachers/Create
        public ActionResult Create()
        {
            ViewBag.StudentsGroupId = new SelectList(db.StudentsGroups.Select(x => new { Id = x.Id, Name = x.Level + x.Name }), "Id", "Name");
            ViewBag.SubjectId = new SelectList(db.Subjects, "Id", "Name");
            ViewBag.TeacherId = new SelectList(db.MyUsers
                .Join(db.Users,
                myUsers => myUsers.Owner,
                systemUsers => systemUsers.Id,
                (myUsers, systemUsers) => new { myUsers, systemUsers })
               .Where(x => x.systemUsers.Roles.Any(r => r.RoleId == "ffb7427a-13cc-447d-868f-235d99f92d46"))

                 .Select(x => new
                 {
                     Id = x.myUsers.Id,
                     Name = x.myUsers.Name + " " + x.myUsers.Surname
                 }), "Id", "Name");


            return View();
        }

        // POST: SubjectStudentGroupTeachers/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,SubjectId,StudentsGroupId,TeacherID")] SubjectStudentGroupTeacher subjectStudentGroupTeacher)
        {
            if (ModelState.IsValid)
            {
                if (int.TryParse(subjectStudentGroupTeacher.TeacherID, out var teacherID))
                {
                    var teacherGuid = db.MyUsers.FirstOrDefault(x => x.Id == teacherID).Owner;
                    subjectStudentGroupTeacher.TeacherID = teacherGuid;

                }
                db.SubjectStudentGroupTeacher.Add(subjectStudentGroupTeacher);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.StudentsGroupId = new SelectList(db.StudentsGroups, "Id", "Name", subjectStudentGroupTeacher.StudentsGroupId);
            ViewBag.SubjectId = new SelectList(db.Subjects, "Id", "Name", subjectStudentGroupTeacher.SubjectId);
            ViewBag.TeacherId = new SelectList(db.MyUsers.Select(x => new { Id = x.Id, Name = x.Name + " " + x.Surname }), "Id", "Name");

            return View(subjectStudentGroupTeacher);
        }

        // GET: SubjectStudentGroupTeachers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubjectStudentGroupTeacher subjectStudentGroupTeacher = db.SubjectStudentGroupTeacher.Find(id);
            if (subjectStudentGroupTeacher == null)
            {
                return HttpNotFound();
            }
            ViewBag.StudentsGroupId = new SelectList(db.StudentsGroups, "Id", "Name", subjectStudentGroupTeacher.StudentsGroupId);
            ViewBag.SubjectId = new SelectList(db.Subjects, "Id", "Name", subjectStudentGroupTeacher.SubjectId);
            return View(subjectStudentGroupTeacher);
        }

        // POST: SubjectStudentGroupTeachers/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,SubjectId,StudentsGroupId,TeacherID")] SubjectStudentGroupTeacher subjectStudentGroupTeacher)
        {
            if (ModelState.IsValid)
            {
                db.Entry(subjectStudentGroupTeacher).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.StudentsGroupId = new SelectList(db.StudentsGroups, "Id", "Name", subjectStudentGroupTeacher.StudentsGroupId);
            ViewBag.SubjectId = new SelectList(db.Subjects, "Id", "Name", subjectStudentGroupTeacher.SubjectId);
            return View(subjectStudentGroupTeacher);
        }

        // GET: SubjectStudentGroupTeachers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubjectStudentGroupTeacher subjectStudentGroupTeacher = db.SubjectStudentGroupTeacher.Find(id);
            if (subjectStudentGroupTeacher == null)
            {
                return HttpNotFound();
            }
            return View(subjectStudentGroupTeacher);
        }

        // POST: SubjectStudentGroupTeachers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SubjectStudentGroupTeacher subjectStudentGroupTeacher = db.SubjectStudentGroupTeacher.Find(id);
            db.SubjectStudentGroupTeacher.Remove(subjectStudentGroupTeacher);
            db.SaveChanges();
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
