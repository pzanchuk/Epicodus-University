using Microsoft.AspNetCore.Mvc;
using UniversityProject.Models;
using System.Collections.Generic;
using System;

namespace UniversityProject.Controllers
{
    public class StudentsController : Controller
    {

      [HttpGet("/students")]
      public ActionResult Index()
      {
        List<Student> allStudents = Student.GetAllStudents();
        return View(allStudents);
      }

      [HttpGet("/students/new")]
      public ActionResult New()
      {
        return View();
      }

      [HttpPost("/students/create")]
      public ActionResult New(string name, DateTime date)
      {
        Student newStudent = new Student(name, date);
        newStudent.Save();
        return RedirectToAction("Index");
      }

    }
}
