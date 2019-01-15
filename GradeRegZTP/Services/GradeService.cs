using GradeRegZTP.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace GradeRegZTP.Services
{
    public interface IGradeService: IEnumerable<Grade>
    {
        List<Grade> GetAllGrades();
        Grade Find(int? id);
        void AddGrade(Grade grade);
        void DeleteGrade(int? id);
        void UpdateGrade(Grade grade);
        IEnumerator GradesForSubject(string subject);
        IEnumerator GradesForDate(DateTime date);
        IEnumerator GradesForStudent(string id);

    }
    public class GradeService : IGradeService
    {
        private List<Grade> grades = new List<Grade>();

        IDbContext context;
        public GradeService(IDbContext _context)
        {
            context = _context;
        }

        public void AddGrade(Grade grade)
        {
            grades.Add(grade);
            context.Grades.Add(grade);
            context.SaveChanges();
        }

        public void DeleteGrade(int? id)
        {
            Grade grade = context.Grades.Find(id);
            grades.Remove(grade);
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

        public IEnumerator GetEnumerator()
        {
            foreach (Grade grade in grades)
            {
                yield return grade;
            }
        }

        public IEnumerator GradesForSubject(string subject)
        {
            foreach (Grade grade in grades)
            {
                if(subject.Equals(grade.Subject.Name))
                {
                    yield return grade;
                }
            }
        }

        public IEnumerator GradesForDate(DateTime date)
        {
            foreach (Grade grade in grades)
            {
                if (date == grade.Date)
                {
                    yield return grade;
                }
            }
        }

        public IEnumerator GradesForStudent(string id)
        {
            foreach (Grade grade in grades)
            {
                if (id.Equals(grade.Owner))
                {
                    yield return grade;
                }
            }
        }

        IEnumerator<Grade> IEnumerable<Grade>.GetEnumerator()
        {
            return (IEnumerator<Grade>)GetEnumerator();
        }
    }
}