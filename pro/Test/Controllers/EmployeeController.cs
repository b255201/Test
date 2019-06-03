using Library;
using Library.Dao;
using Library.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Linq.Dynamic;


namespace Test.Controllers
{
    public class EmployeeController : Controller
    {
        NorthwindEntities db = new NorthwindEntities();
        private IRepository<Employees> Employee = new Repository<Employees>();
        // GET: /Employee/
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Search()
        {
            JQueryDataTableRequest jqDataTableRq = JQueryDataTableHelper<Employees>.GetRequest(Request);
            var q = db.Employees.AsEnumerable();
            int totalLen = Convert.ToInt16(q.Count());
             //排序
            q = q.OrderBy(jqDataTableRq.orderBy +"  "+ jqDataTableRq.orderDir).ToList();
            q = q.Skip(jqDataTableRq.start).Take(jqDataTableRq.length)
                     .ToList();
            var result = from Row in q
                         select new Employees
                         {
                             EmployeeID=Row.EmployeeID,
                             FirstName = Row.FirstName,
                             LastName = Row.LastName,
                             HomePhone=Row.HomePhone
                         };
            JQueryDataTableResponse<Employees> jqDataTableRs = JQueryDataTableHelper<Employees>.GetResponse(jqDataTableRq.draw, totalLen, totalLen, result.ToList());
            return Json(jqDataTableRs);
        }

	}
}