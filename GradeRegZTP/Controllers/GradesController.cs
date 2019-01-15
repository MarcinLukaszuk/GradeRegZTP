using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GradeRegZTP.Models;
using GradeRegZTP.Services;
using GradeRegZTP.ViewModel;
using Microsoft.AspNet.Identity;

namespace GradeRegZTP.Controllers
{
    [Authorize]
    public class GradesController : Controller
    {
        private IDbContext db;
        private IGradeService gradeService;

        public GradesController(IDbContext _db)
        {
            db = _db;
            gradeService = new GradeProxy(new GradeService(_db));
        }


        // GET: Grades
        public ActionResult Index()
        {
            var userID = User.Identity.GetUserId();
            if (User.IsInRole("Admin"))
            {
                var grades = gradeService.GetAllGrades();
                return View(grades.ToList());
            }
            else if (User.IsInRole("Teacher"))
            {
                var subjectStudentsGroup = db.SubjectStudentGroupTeacher.Where(x => x.TeacherID == userID).Select(x => new TeacherGradesViewModel()
                {
                    Subject = x.Subject,
                    StudentsGroup = x.StudentsGroup
                }).Distinct().ToList();




                return View("~\\Views\\Grades\\IndexTeacher.cshtml", subjectStudentsGroup);
            }
            else if (User.IsInRole("Student"))
            {
                var myStudentGrupuID = db.MyUsers.Where(x => x.Owner == userID).Select(x => x.StudentsGroupId).FirstOrDefault();

                var grades = Helper.ToIEnumerable<Grade>(gradeService.GradesForStudent(userID));
                var subjects = db.SubjectStudentGroupTeacher.Where(x => x.StudentsGroupId == myStudentGrupuID).Select(x => x.Subject).ToList();


                StudentGradesViewModel studentViewModel = new StudentGradesViewModel()
                {
                    Grades = grades,
                    Subjects = subjects
                };

                return View("~\\Views\\Grades\\IndexStudent.cshtml", studentViewModel);
            }
            return View();
        }



        // GET: Grades/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Grade grade = db.Grades.Find(id);
            if (grade == null)
            {
                return HttpNotFound();
            }
            return View(grade);
        }

        // GET: Grades/Create
        public ActionResult Create()
        {
            ViewBag.SubjectId = new SelectList(db.Subjects, "Id", "Name");
            return View();
        }

        // POST: Grades/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,SubjectId,Value,Weight,Note,Date,Owner")] Grade grade)
        {
            if (ModelState.IsValid)
            {
                gradeService.AddGrade(grade);
                return RedirectToAction("Index");
            }

            ViewBag.SubjectId = new SelectList(db.Subjects, "Id", "Name", grade.SubjectId);
            return View(grade);
        }

        // GET: Grades/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Grade grade = gradeService.Find(id);
            if (grade == null)
            {
                return HttpNotFound();
            }
            ViewBag.SubjectId = new SelectList(db.Subjects, "Id", "Name", grade.SubjectId);
            return View(grade);
        }

        // POST: Grades/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,SubjectId,Value,Weight,Note,Date,Owner")] Grade grade)
        {
            if (ModelState.IsValid)
            {
                gradeService.UpdateGrade(grade);
                return RedirectToAction("Index");
            }
            ViewBag.SubjectId = new SelectList(db.Subjects, "Id", "Name", grade.SubjectId);
            return View(grade);
        }

        // GET: Grades/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Grade grade = gradeService.Find(id);
            if (grade == null)
            {
                return HttpNotFound();
            }
            return View(grade);
        }

        // POST: Grades/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            gradeService.DeleteGrade(id);
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
