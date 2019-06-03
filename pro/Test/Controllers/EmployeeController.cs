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
            var q = Employee.GetAll();
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

        #region Create
        [HttpPost]  
        public ActionResult Create(string LastName, string FirstName,  string HomePhone)
        {
            try
            {
                Employees emp = new Employees();
                emp.LastName = LastName;
                emp.FirstName = FirstName;
                emp.HomePhone = HomePhone;
                Employee.Create(emp);
                return Json(new { Status = "0", StatusDesc = "新增成功!!" });
            }
            catch (Exception err)
            {
                 
                return Json(new { Status = "2", StatusDesc = "新增失敗" + err.Message });
            }           
        }
        #endregion

        #region Delete
        [HttpPost]
        public ActionResult Delete(string Id)
        {
            try
            {
                Employees _Employees = Employee.GetById(int.Parse(Id));
                Employee.Delete(_Employees);
                return Json(new { Status = "0", StatusDesc = "刪除成功" });
            }
            catch (Exception err)
            {

                return Json(new { Status = "2", StatusDesc = "刪除失敗" + err.Message });
            }

        }
        #endregion

        #region Get
        [HttpPost]
        public ActionResult Get(string Id)
        {
            try
            {
                Employees _Employees = Employee.GetById(int.Parse(Id));
                return Json(new { Status = "0", StatusDesc = _Employees });
            }
            catch (Exception err)
            {

                return Json(new { Status = "2", StatusDesc = "失敗" + err.Message });
            }
        }
        #endregion

        [HttpPost]
        public ActionResult Edit(string Id,string LastName, string FirstName, string HomePhone)
        {
            try
            {
                Employees _Employees = Employee.GetById(int.Parse(Id));
                _Employees.LastName = LastName;
                _Employees.FirstName = FirstName;
                _Employees.HomePhone = HomePhone;
                Employee.Update(_Employees);
                return Json(new { Status = "0", StatusDesc ="修改成功" });
            }
            catch (Exception err)
            {

                return Json(new { Status = "2", StatusDesc = "修改失敗" + err.Message });
            }
 
        }
	}
}