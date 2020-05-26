using Account.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Account.Filter;
namespace Account.Controllers
{
    public class MenusController : Controller
    {
        AccountDBEntities db = new AccountDBEntities();
        // GET: Menus
        public ActionResult Index(int? page)
        {
            List<Menus> list = db.Menus.OrderByDescending(p=>p.ID).ToList();
            int pageNumber = page ?? 1;
            //每页显示多少条  
            int pageSize = 4;
            //通过ToPagedList扩展方法进行分页  
            IPagedList<Menus> MenusPagedList = list.ToPagedList(pageNumber, pageSize);
            
            return View(MenusPagedList);
        }
        [HttpPost]
        public ActionResult Index(string Name,int? page) {
            List<Menus> list = db.Menus.Where(s=>s.Name==""||s.Name.Contains(Name)).OrderByDescending(p => p.ID).ToList();
            int pageNumber = page ?? 1;
            //每页显示多少条  
            int pageSize = 4;
            //通过ToPagedList扩展方法进行分页  
            IPagedList<Menus> MenusPagedList = list.ToPagedList(pageNumber, pageSize);
            ViewBag.i = Name;
            return View(MenusPagedList);
            
        }

        [Login]
        public ActionResult Add() {
            ViewBag.i = "添加菜单";

            return View();
        }
        [HttpGet]
        public ActionResult Add(int? ID)
        {
            ViewBag.i = "修改菜单";

            Menus menus = db.Menus.Find(ID);
            return View(menus);
        }
        [HttpPost]
        public ActionResult Add(Menus menus)
        {
            if (menus.ID==0)
            {
                db.Menus.Add(menus);
                db.SaveChanges();
            }
            else
            {
                db.Entry<Menus>(menus).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int? ID)
        {
            List<R_Role_Menus> list = db.R_Role_Menus.Where(p=>p.MenuID==ID).ToList();
            if (list.Count>0)
            {
                return Content("<script>alert('该菜单正在使用！！，不能删除');history.go(-1)</script>");
            }
            else
            {
                Menus menus = db.Menus.Find(ID);
                db.Menus.Remove(menus);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}