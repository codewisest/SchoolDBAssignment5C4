using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SchoolDBAssignment4C2.Models;

namespace SchoolDBAssignment4C2.Controllers
{
    public class TeacherController : Controller
    {
        // GET: Teacher
        public ActionResult Index()
        {
            return View();
        }

        //GET : /Teacher/List
        public ActionResult List(string SearchKey = null)
        {
            TeacherDataController controller = new TeacherDataController();
            IEnumerable<Teacher> Teachers = controller.ListTeachers(SearchKey);
            return View(Teachers);
        }

        // Get : /Teacher/New
        public ActionResult New()
        {
            return View();
        }

        // Post : /Teacher/Create
        [HttpPost]
        public ActionResult Create(string TeacherFirstName, string TeacherLastName, string EmployeeNumber, DateTime HireDate, Decimal Salary)
        {
            Debug.WriteLine("Created");
            Debug.WriteLine(TeacherFirstName);
            Debug.WriteLine(TeacherLastName);
            Debug.WriteLine(EmployeeNumber);
            Debug.WriteLine(HireDate);
            Debug.WriteLine(Salary);

            Teacher NewTeacher = new Teacher();
            NewTeacher.TeacherFirstName = TeacherFirstName;
            NewTeacher.TeacherLastName = TeacherLastName;
            NewTeacher.EmployeeNumber = EmployeeNumber;
            NewTeacher.HireDate = HireDate;
            NewTeacher.Salary = Salary;

            TeacherDataController TeacherDataController = new TeacherDataController();
            TeacherDataController.AddTeacher(NewTeacher);
            return RedirectToAction("list");
        }

        // Get : /Teacher/Show/{id}
        public ActionResult Show(int id)
        {
            TeacherDataController teacherDataController = new TeacherDataController();
            Teacher NewTeacher = teacherDataController.ShowTeacher(id);

            return View(NewTeacher);
        }

        // Get : /Teacher/ConfirmDelete/{id}
        public ActionResult ConfirmDelete(int id)
        {
            TeacherDataController teacherDataController = new TeacherDataController();
            Teacher NewTeacher = teacherDataController.ShowTeacher(id);

            return View(NewTeacher);
        }


        //POST : /Teacher/Delete/{id}
        [HttpPost]
        public ActionResult Delete(int id)
        {
            TeacherDataController teacherDataController = new TeacherDataController();
            teacherDataController.DeleteTeacher(id);
            return RedirectToAction("List");
        }
    }
}