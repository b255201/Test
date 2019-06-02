using Library;
using Library.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Test.Controllers
{
    public class EmployeeController : Controller
    {
        NorthwindEntities db = new NorthwindEntities();
        private IRepository<Employees> Employee = new Repository<Employees>();
        // GET: /Employee/
        public ActionResult Index()
        {
            var q = Employee.GetAll().OrderBy(x=>x.EmployeeID);
            string a = "";
            foreach (var n in q)
            {
                a=n.FirstName;
            }
            return View();
        }
	}
}