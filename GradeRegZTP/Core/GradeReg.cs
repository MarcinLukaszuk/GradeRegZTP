using GradeRegZTP.Models;
using System.Collections.Generic;
using System.Linq;

namespace GradeRegZTP.Core
{
   
    public class GradeReg
    {
        private static GradeReg instance;
        private ApplicationDbContext context;
        private GradeReg() { context = new ApplicationDbContext(); }

        public List<MyUser> users
        {
            get
            {
                return context.MyUsers.ToList();
            }
        }

        public List<Subject> subjects
        {
            get
            {
                return context.Subjects.ToList();
            }
        }

        public static GradeReg GetInstance()
        {
            if (instance == null)
            {
                instance = new GradeReg();
            }
            return instance;
        }

        public void AddSubject(Subject subject)
        {
            context.Subjects.Add(subject);
            context.SaveChanges();
        }

        public void DeleteSubject(int id)
        {
            Subject subject = context.Subjects.Find(id);
            context.Subjects.Remove(subject);
            context.SaveChanges(); 
        }
    }
}