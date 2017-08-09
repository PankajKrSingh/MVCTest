using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCTest.Models;

namespace MVCTest.Controllers
{
    public class DefaultController : Controller
    {
        

        [HttpPost]
        public ActionResult SetEmployee(Employee employee)
        {
            Employee.InsertUpdateEmployee(employee);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Index(int id = 0)
        {
            Employee employee = new Employee();
            if (id != 0)
                employee = Employee.GetEmployee(id);


            return View(employee);
        }
    }
}