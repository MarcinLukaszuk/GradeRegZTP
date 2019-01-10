using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GradeRegZTP.Models;

namespace GradeRegZTP.Controllers
{
    public class StrudentsGroupsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: StrudentsGroups
        public ActionResult Index()
        {
            return View(db.StrudentsGroups.ToList());
        }

        // GET: StrudentsGroups/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StrudentsGroup strudentsGroup = db.StrudentsGroups.Find(id);
            if (strudentsGroup == null)
            {
                return HttpNotFound();
            }
            return View(strudentsGroup);
        }

        // GET: StrudentsGroups/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: StrudentsGroups/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Level")] StrudentsGroup strudentsGroup)
        {
            if (ModelState.IsValid)
            {
                db.StrudentsGroups.Add(strudentsGroup);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(strudentsGroup);
        }

        // GET: StrudentsGroups/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StrudentsGroup strudentsGroup = db.StrudentsGroups.Find(id);
            if (strudentsGroup == null)
            {
                return HttpNotFound();
            }
            return View(strudentsGroup);
        }

        // POST: StrudentsGroups/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Level")] StrudentsGroup strudentsGroup)
        {
            if (ModelState.IsValid)
            {
                db.Entry(strudentsGroup).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(strudentsGroup);
        }

        // GET: StrudentsGroups/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StrudentsGroup strudentsGroup = db.StrudentsGroups.Find(id);
            if (strudentsGroup == null)
            {
                return HttpNotFound();
            }
            return View(strudentsGroup);
        }

        // POST: StrudentsGroups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StrudentsGroup strudentsGroup = db.StrudentsGroups.Find(id);
            db.StrudentsGroups.Remove(strudentsGroup);
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
