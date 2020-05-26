using Account.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Account.Controllers
{
    public class RoleController : Controller
    {
        AccountDBEntities db = new AccountDBEntities();
        // GET: Role
        public ActionResult Index(int? page)
        {
            List<Roles> list = db.Roles.OrderByDescending(p => p.ID).ToList();
            int pageNumber = page ?? 1;
            //每页显示多少条  
            int pageSize = 4;
            //通过ToPagedList扩展方法进行分页  
            IPagedList<Roles> RolesPagedList = list.ToPagedList(pageNumber, pageSize);

            return View(RolesPagedList);
        }
        [HttpPost]
        public ActionResult Index(string Name, int? page)
        {
            List<Roles> list = db.Roles.Where(s => s.Name == "" || s.Name.Contains(Name)).OrderByDescending(p => p.ID).ToList();
            int pageNumber = page ?? 1;
            //每页显示多少条  
            int pageSize = 4;
            //通过ToPagedList扩展方法进行分页  
            IPagedList<Roles> MenusPagedList = list.ToPagedList(pageNumber, pageSize);
            ViewBag.i = Name;
            return View(MenusPagedList);

        }
        [HttpPost]
        public ActionResult Add(Roles roles)
        {
            db.Roles.Add(roles);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult DelMenus(int id) 
        {
            R_Role_Menus rrm = db.R_Role_Menus.Where(p => p.ID == id).SingleOrDefault();
            db.R_Role_Menus.Remove(rrm);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult DelRoles(int id)
        {
            List<R_User_Roles> list = db.R_User_Roles.Where(p=>p.RoleID==id).ToList();
            List<R_Role_Menus> lists = db.R_Role_Menus.Where(p => p.RoleID == id).ToList();
            if (list.Count>0)
            {
                return Content("<script>alert('该角色正在与用户绑定使用！！，不能删除');history.go(-1)</script>");
            }
            else if (lists.Count>0)
            {
                return Content("<script>alert('该角色正在与菜单绑定使用！！，不能删除');history.go(-1)</script>");
            }
            else
            {
                Roles roles = db.Roles.Find(id);
                db.Roles.Remove(roles);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        
        public ActionResult SetMenus(int id)
        {
            Roles roles = db.Roles.Find(id);
            List<R_Role_Menus> rrm = db.R_Role_Menus.Where(p => p.RoleID == id).ToList();
            List<Menus> menus = db.Menus.ToList();
            ViewBag.roles = roles; 
            ViewBag.list = rrm;
            return View(menus);
        }

        [HttpPost]
        public ActionResult SetMenus(List<int> MenuID,int RoleID)
        {
            if (MenuID.Count==0)
            {

            }
            else
            {
                List<R_Role_Menus> r = db.R_Role_Menus.Where(p => p.RoleID == RoleID).ToList();
                foreach (var item in r)
                {
                    db.R_Role_Menus.Remove(item);
                    db.SaveChanges();
                }
                foreach (var item in MenuID)
                {
                    var r_Role_Menus = new R_Role_Menus
                    {
                        MenuID = item,
                        RoleID = RoleID
                    };
                    db.R_Role_Menus.Add(r_Role_Menus);
                    db.SaveChanges();
                }
            }
            
            return RedirectToAction("Index");
        }
    }
}