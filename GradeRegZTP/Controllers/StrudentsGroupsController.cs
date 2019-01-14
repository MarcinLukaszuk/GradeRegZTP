using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GradeRegZTP.Models;
using GradeRegZTP.ViewModel;

namespace GradeRegZTP.Controllers
{
    [Authorize]
    public class StudentsGroupsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: StudentsGroups
        public ActionResult Index()
        {
            return View(db.StudentsGroups.ToList());
        }

        public ActionResult AddGrade(int? studentGroupId, int? subjectId)
        {
            if (User.IsInRole("Teacher"))
            {
                var studentsGroup = db.StudentsGroups.FirstOrDefault(x => x.Id == studentGroupId);
                var subjectName = db.Subjects.FirstOrDefault(x => x.Id == subjectId).Name;

                var myUsers = db.MyUsers.Where(x => x.StudentsGroupId == studentGroupId).Select(x => new StudentsGroupAddGradeViewModel()
                {
                    MyUserId = x.Owner,
                    MyUser = x,
                    SubjectName = subjectName,
                    StudentGroupName = studentsGroup.Level + studentsGroup.Name,
                    SubjectId = (int)subjectId,
                    StudentsGroupId = (int)studentGroupId
                }).ToList();

                StudentsGroupAddGradeViewModelList model = new StudentsGroupAddGradeViewModelList();
                model.StudentsGroupAddGradeViewModels = myUsers;


                return View("~\\Views\\StudentsGroups\\AddGradeTeacher.cshtml", model);
            }
            return HttpNotFound();
        }
        [HttpPost]

        [ValidateAntiForgeryToken]
        public ActionResult AddGrade([Bind(Include = "StudentsGroupAddGradeViewModels")]  StudentsGroupAddGradeViewModelList model)
        {
            foreach (var studentsGroupAddGradeViewModels in model.StudentsGroupAddGradeViewModels)
            {
                if (studentsGroupAddGradeViewModels.Note != null)
                {
                    var grade = new Grade()
                    {
                        Value = studentsGroupAddGradeViewModels.Grade.Value,
                        Owner = studentsGroupAddGradeViewModels.MyUserId,
                        Note = studentsGroupAddGradeViewModels.Note,
                        Date = DateTime.Now,
                        SubjectId = studentsGroupAddGradeViewModels.SubjectId,
                        Weight = studentsGroupAddGradeViewModels.Weight.Value
                    };
                    db.Grades.Add(grade);
                }
            }
            db.SaveChanges();
            if (!model.StudentsGroupAddGradeViewModels.Any())
            {
                return View("~\\Views\\Grades\\index.cshtml");
            }

            return RedirectToAction("Index", "Grades", new { id = 1 });
        }
        public ActionResult Details(int? studentGroupId, int? subjectId)
        {
            if (studentGroupId == null || studentGroupId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (User.IsInRole("Teacher"))
            {
                var studentsGroup = db.StudentsGroups.FirstOrDefault(x => x.Id == studentGroupId);
                var subjectName = db.Subjects.FirstOrDefault(x => x.Id == subjectId).Name;

                var myUsers = db.MyUsers.Where(x => x.StudentsGroupId == studentGroupId).Select(x => new StudentGroupViewModel()
                {
                    MyUser = x,
                    SubjectName = subjectName,
                    StudentGroupName = studentsGroup.Level + studentsGroup.Name,
                    StudentGroupId = (int)studentGroupId,
                    SubjectId = (int)subjectId
                }).ToList();


                foreach (var myUser in myUsers)
                {
                    myUser.Grades = db.Grades.Where(x => x.Owner == myUser.MyUser.Owner && x.SubjectId == subjectId).ToList();
                }

                return View("~\\Views\\StudentsGroups\\DetailsTeacher.cshtml", myUsers);
            }

            StudentsGroup StudentsGroup = db.StudentsGroups.Find(studentGroupId);
            if (StudentsGroup == null)
            {
                return HttpNotFound();
            }
            return View(StudentsGroup);
        }
        // GET: StudentsGroups/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: StudentsGroups/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Level")] StudentsGroup StudentsGroup)
        {
            if (ModelState.IsValid)
            {
                db.StudentsGroups.Add(StudentsGroup);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(StudentsGroup);
        }

        // GET: StudentsGroups/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentsGroup StudentsGroup = db.StudentsGroups.Find(id);
            if (StudentsGroup == null)
            {
                return HttpNotFound();
            }
            return View(StudentsGroup);
        }

        // POST: StudentsGroups/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Level")] StudentsGroup StudentsGroup)
        {
            if (ModelState.IsValid)
            {
                db.Entry(StudentsGroup).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(StudentsGroup);
        }

        // GET: StudentsGroups/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentsGroup StudentsGroup = db.StudentsGroups.Find(id);
            if (StudentsGroup == null)
            {
                return HttpNotFound();
            }
            return View(StudentsGroup);
        }

        // POST: StudentsGroups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StudentsGroup StudentsGroup = db.StudentsGroups.Find(id);
            db.StudentsGroups.Remove(StudentsGroup);
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
