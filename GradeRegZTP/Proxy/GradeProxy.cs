using System.Collections.Generic;
using System.Diagnostics;
using GradeRegZTP.Models;
using GradeRegZTP.Services;

namespace GradeRegZTP.Controllers
{
    internal class GradeProxy : IGradeService
    {
        private GradeService gradeService;
        public GradeProxy(GradeService _gradeService)
        {
            gradeService = _gradeService;
        }

        public void AddGrade(Grade grade)
        {
            Debug.WriteLine("Dodawanie oceny");
            gradeService.AddGrade(grade);
        }

        public void DeleteGrade(int? id)
        {
            Debug.WriteLine("Usuwanie oceny");
            gradeService.DeleteGrade(id);
        }

        public Grade Find(int? id)
        {
            Debug.WriteLine("Wyszukiwanie oceny");
            return gradeService.Find(id);
        }

        public List<Grade> GetAllGrades()
        {
            Debug.WriteLine("Pobieranie wszystkich ocen");
            return gradeService.GetAllGrades();
        }

        public void UpdateGrade(Grade grade)
        {
            Debug.WriteLine("Aktualizacja oceny");
            gradeService.UpdateGrade(grade);
        }
    }
}