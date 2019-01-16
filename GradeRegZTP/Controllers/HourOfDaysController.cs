using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GradeRegZTP.Builder;
using GradeRegZTP.Models;
using Microsoft.AspNet.Identity;

namespace GradeRegZTP.Controllers
{
    [Authorize]
    public class HourOfDaysController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private PDFTimetableBuilder PDFBuilder = new PDFTimetableBuilder();

        // GET: HourOfDays1
        public ActionResult Index()
        {
            var userID = User.Identity.GetUserId();
            if (User.IsInRole("Admin"))
            {
                var hourOfDays = db.HourOfDays.Include(h => h.DayOfWeek).Include(h => h.Hour).Include(h => h.StudentsGroup).Include(h => h.Subject);
                return View(hourOfDays.ToList());
            }
            else if (User.IsInRole("Teacher"))
            {
                var hourOfDays = db.SubjectStudentGroupTeacher
                    .Join(db.HourOfDays,
                    SSGT => new { StudentsGroupId = SSGT.StudentsGroupId, SubjectId = SSGT.SubjectId },
                    HoD => new { StudentsGroupId = HoD.StudentsGroupId, SubjectId = HoD.SubjectId },
                    (SSGT, HoD) => new { SSGT, HoD })
                    .Where(x => x.SSGT.TeacherID == userID)
                    .Select(x => x.HoD)
                    .Distinct()
                    .ToList();
                return View("~\\Views\\HourOfDays\\IndexTeacher.cshtml", hourOfDays);

            }
            else
            {
                var hourOfDays = db.HourOfDays.Include(h => h.DayOfWeek).Include(h => h.Hour).Include(h => h.StudentsGroup).Include(h => h.Subject).Where(x => x.StudentsGroupId == db.MyUsers.FirstOrDefault(xd => xd.Owner == userID).StudentsGroupId).ToList();
                return View("~\\Views\\HourOfDays\\IndexStudent.cshtml", hourOfDays);
            }
        }

        // GET: HourOfDays1/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HourOfDay hourOfDay = db.HourOfDays.Find(id);
            if (hourOfDay == null)
            {
                return HttpNotFound();
            }
            return View(hourOfDay);
        }

        // GET: HourOfDays1/Create
        public ActionResult Create()
        {
            ViewBag.DayOfWeekId = new SelectList(db.DayOfWeek, "Id", "Name");
            ViewBag.HourId = new SelectList(db.Hours, "Id", "HourString");
            ViewBag.StudentsGroupId = new SelectList(db.StudentsGroups.Select(x => new { Id = x.Id, Name = x.Level + x.Name }), "Id", "Name");
            ViewBag.SubjectId = new SelectList(db.Subjects, "Id", "Name");
            return View();
        }

        // POST: HourOfDays1/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,HourId,SubjectId,StudentsGroupId,DayOfWeekId")] HourOfDay hourOfDay)
        {
            if (ModelState.IsValid)
            {
                db.HourOfDays.Add(hourOfDay);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DayOfWeekId = new SelectList(db.DayOfWeek, "Id", "Name", hourOfDay.DayOfWeekId);
            ViewBag.HourId = new SelectList(db.Hours, "Id", "HourString", hourOfDay.HourId);
            ViewBag.StudentsGroupId = new SelectList(db.StudentsGroups, "Id", "Name", hourOfDay.StudentsGroupId);
            ViewBag.SubjectId = new SelectList(db.Subjects, "Id", "Name", hourOfDay.SubjectId);
            return View(hourOfDay);
        }

        // GET: HourOfDays1/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HourOfDay hourOfDay = db.HourOfDays.Find(id);
            if (hourOfDay == null)
            {
                return HttpNotFound();
            }
            ViewBag.DayOfWeekId = new SelectList(db.DayOfWeek, "Id", "Name", hourOfDay.DayOfWeekId);
            ViewBag.HourId = new SelectList(db.Hours, "Id", "HourString", hourOfDay.HourId);
            ViewBag.StudentsGroupId = new SelectList(db.StudentsGroups, "Id", "Name", hourOfDay.StudentsGroupId);
            ViewBag.SubjectId = new SelectList(db.Subjects, "Id", "Name", hourOfDay.SubjectId);
            return View(hourOfDay);
        }

        // POST: HourOfDays1/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,HourId,SubjectId,StudentsGroupId,DayOfWeekId")] HourOfDay hourOfDay)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hourOfDay).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DayOfWeekId = new SelectList(db.DayOfWeek, "Id", "Name", hourOfDay.DayOfWeekId);
            ViewBag.HourId = new SelectList(db.Hours, "Id", "HourString", hourOfDay.HourId);
            ViewBag.StudentsGroupId = new SelectList(db.StudentsGroups, "Id", "Name", hourOfDay.StudentsGroupId);
            ViewBag.SubjectId = new SelectList(db.Subjects, "Id", "Name", hourOfDay.SubjectId);
            return View(hourOfDay);
        }

        // GET: HourOfDays1/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HourOfDay hourOfDay = db.HourOfDays.Find(id);
            if (hourOfDay == null)
            {
                return HttpNotFound();
            }
            return View(hourOfDay);
        }

        // POST: HourOfDays1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            HourOfDay hourOfDay = db.HourOfDays.Find(id);
            db.HourOfDays.Remove(hourOfDay);
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
        public ActionResult PDFGenerator()
        {
            var userID = User.Identity.GetUserId();
            List<HourOfDay> lessons;
            if (User.IsInRole("Teacher"))
            {
                lessons = db.SubjectStudentGroupTeacher
                    .Join(db.HourOfDays,
                    SSGT => new { StudentsGroupId = SSGT.StudentsGroupId, SubjectId = SSGT.SubjectId },
                    HoD => new { StudentsGroupId = HoD.StudentsGroupId, SubjectId = HoD.SubjectId },
                    (SSGT, HoD) => new { SSGT, HoD })
                    .Where(x => x.SSGT.TeacherID == userID)
                    .Select(x => x.HoD)
                    .Distinct()
                    .ToList();
            }
            else
            {
                lessons = db.HourOfDays.Include(h => h.DayOfWeek)
                    .Include(h => h.Hour).Include(h => h.StudentsGroup)
                    .Include(h => h.Subject)
                    .Where(x => x.StudentsGroupId == db.MyUsers.FirstOrDefault(xd => xd.Owner == userID).StudentsGroupId).ToList();
            }
            CreateTimetable(PDFBuilder, lessons);
            
            return PDFBuilder.GeneratePDF(); 
        }

        private void CreateTimetable(ITimetableBuilder builder, List<HourOfDay> lessons)
        {
            builder.AddHeader();
            builder.AddColumn("Godzina");
            builder.AddColumn("Poniedziałek");
            builder.AddColumn("Wtorek");
            builder.AddColumn("Środa");
            builder.AddColumn("Czwartek");
            builder.AddColumn("Piątek");
            builder.AddColumn("Sobota");
            builder.AddColumn("Niedziela");

            for (int j = 8; j <= 18; j++)
            {
                var godzina = j + ":15";
                builder.AddRow();
                builder.AddColumn(godzina);
                for (int i = 1; i < 8; i++)
                {
                    var dzienTygodnia = i;
                    var zajecie = lessons.Where(x => x.Hour.HourString.Equals(godzina) && x.DayOfWeekId == dzienTygodnia).FirstOrDefault();
                    if (zajecie != null)
                        builder.AddColumn(zajecie.Subject.Name);
                    else
                        builder.AddColumn("");
                }
            }
        }
    }
}
