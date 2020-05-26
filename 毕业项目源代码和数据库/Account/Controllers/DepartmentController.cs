using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Account.Models;
using PagedList;
using Account.Filter;
namespace Account.Controllers
{
    
    public class DepartmentController : Controller
    {
        AccountDBEntities db = new AccountDBEntities();
        // GET: Department
       
        public ActionResult Index(int? page)
        {
            List<Departments> list = db.Departments.OrderByDescending(p=>p.ID).ToList();
            
            int pageNumber = page ?? 1;
            //每页显示多少条  
            int pageSize = 5;
            //通过ToPagedList扩展方法进行分页  
            IPagedList<Departments> RolesPagedList = list.ToPagedList(pageNumber, pageSize);

            return View(RolesPagedList);
            
        }
        [HttpGet]
        public ActionResult Edit(int id,int? page)
        {
            Departments departments = db.Departments.Find(id);
            ViewBag.de = departments;
            List<Departments> list = db.Departments.OrderByDescending(p => p.ID).ToList();

            int pageNumber = page ?? 1;
            //每页显示多少条  
            int pageSize = 5;
            //通过ToPagedList扩展方法进行分页  
            IPagedList<Departments> RolesPagedList = list.ToPagedList(pageNumber, pageSize);

            return RedirectToAction("Index",RolesPagedList);

        }
        [HttpPost]
        public ActionResult Index(string Name,int? page)
        {
            List<Departments> list = db.Departments.OrderByDescending(p => p.ID).Where(p=>Name==""||p.Name.Contains(Name)).ToList();

            int pageNumber = page ?? 1;
            //每页显示多少条  
            int pageSize = 5;
            //通过ToPagedList扩展方法进行分页  
            IPagedList<Departments> RolesPagedList = list.ToPagedList(pageNumber, pageSize);
            ViewBag.i = Name;
            return View(RolesPagedList);

        }
        [HttpPost]
        public ActionResult Add(Departments departments)
        {
            if (departments.ID==0)
            {
                db.Departments.Add(departments);
                db.SaveChanges();
            }
            else
            {
                db.Entry<Departments>(departments).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int id)
        {
            List<UserInfos> list = db.UserInfos.Where(p=>p.DepartmentID==id).ToList();
            if (list.Count>0)
            {
                return Content("<script>alert('该部门还有员工！！，不能删除');history.go(-1)</script>");
            }
            else
            {
                db.Departments.Remove(db.Departments.Find(id));
                db.SaveChanges();
                return RedirectToAction("Index");
            }
           
           
        }
        public ActionResult Indexs() 
        {
            return View();
        
        }
    }
}