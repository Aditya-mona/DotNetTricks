using EFCore.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUD.Controllers
{
    public class StudentController : Controller
    {
        private readonly DotNetTricksContext db;
        public StudentController(DotNetTricksContext DbContext)
        {
            db = DbContext;
        }
        public IActionResult Index()
        {
            List<StudentModel> studModel = new List<StudentModel>();
            List<Student> model = (from s in db.Students select s).ToList();
            foreach (var stud in model)
            {
                studModel.Add(new StudentModel() { StudentId = stud.StudentId, StudentName = stud.StudentName, CourseId = stud.CourseId, CourseName = db.Courses.Find(stud.CourseId).CourseName });
            }
            return View(studModel);
        }
        public IActionResult Create()
        {
            ViewBag.Course = db.Courses.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult Create(Student model)
        {
            try
            {
                if (model.StudentId == Guid.Empty)
                {
                    model.StudentId = Guid.NewGuid();
                    db.Students.Add(model);
                }
                else
                {
                    db.Students.Update(model);
                }

                
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {

            }
            return View("Create", model);
        }
        public IActionResult Edit(Guid? Id)
        {
            //Student model = db.Students.Find(Id);
            var model = db.usp_getproduct(Id);
            ViewBag.Course = db.Courses.ToList();
            return View("Create", model);
        }
        public IActionResult Delete(Guid? Id)
        {
            Student model = db.Students.Find(Id);
            db.Students.Remove(model);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
