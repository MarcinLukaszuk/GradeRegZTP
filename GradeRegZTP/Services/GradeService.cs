using GradeRegZTP.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace GradeRegZTP.Services
{
    public interface IGradeService
    {
        List<Grade> GetAllGrades();
        Grade Find(int? id);
        void AddGrade(Grade grade);
        void DeleteGrade(int? id);
        void UpdateGrade(Grade grade);
    }
    public class GradeService : IGradeService
    {
        IDbContext context;
        public GradeService(IDbContext  _context)
        {
            context = _context;
        }

        public void AddGrade(Grade grade)
        {
            context.Grades.Add(grade);
            context.SaveChanges();
        }

        public void DeleteGrade(int? id)
        {
            Grade grade = context.Grades.Find(id);
            context.Grades.Remove(grade);
            context.SaveChanges();
        }

        public Grade Find(int? id)
        {
            return context.Grades.Find(id);
        }

        public List<Grade> GetAllGrades()
        {
            return context.Grades.Include(g => g.Subject).ToList();
        }

        public void UpdateGrade(Grade grade)
        {
            context.Entry(grade).State = EntityState.Modified;
            context.SaveChanges();
        }
    }
}