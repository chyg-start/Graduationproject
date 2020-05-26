using Account.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Account.Filter;
namespace Account.Controllers
{
  
    public class UserInfoController : Controller
    {
        AccountDBEntities db = new AccountDBEntities();


        // GET: UserInfo
        public ActionResult Index(int departmentID = 0, string Name = "", int pageIndex = 1, int pageCount = 5)
        {
            //获得部门下拉框列表
            List<Departments> dList = db.Departments.ToList();
            ViewBag.dList = dList;

            //获得根据条件所查的总行数
            int tatalCount = db.UserInfos.OrderBy(p => p.ID)
                .Where(p => (departmentID == 0 || p.DepartmentID == departmentID) && (Name == "" || p.Name.Contains(Name)))
                .Count();

            //获得总页数Ceiling()向上取整 2.1  3   2.9 3  round四舍五入2.1   2     2.6  3
            //11/5  2.2   3
            double tatalPage = Math.Ceiling(tatalCount / (double)pageCount);

            //获得用户表,先按照主键正序排列，条件过滤，转成集合
            //skip()跳过指定数量的元素，返回剩下的集合
            //Take()从剩下的集合中，从第一条开始获取制定数量的集合
            List<UserInfos> uList = db.UserInfos.OrderByDescending(p => p.ID).
                Where(p => (departmentID == 0 || p.DepartmentID == departmentID) && (Name == "" || p.Name.Contains(Name)))
                .Skip((pageIndex - 1) * pageCount).Take(pageCount)
                .ToList();
            //列表加载的同时，将条件存储在对应控件显示
            ViewBag.departmentID = departmentID;
            ViewBag.name = Name;


            //当前页
            ViewBag.pageIndex = pageIndex;
            //每页行数
            ViewBag.pageCount = pageCount;
            //总行数
            ViewBag.tatalCount = tatalCount;
            //总页数
            ViewBag.tatalPage = tatalPage;
            return View(uList);
        }

        //public ActionResult Index(int? page)
        //{
        //    List<UserInfos> list = db.UserInfos.OrderByDescending(p=>p.ID).ToList();

        //    //第几页  
        //    int pageNumber = page ?? 1;

        //    //每页显示多少条  
        //    int pageSize = 4;
           
        //    //总条数
        //   // int pageCount=
        //    //通过ToPagedList扩展方法进行分页  
        //    IPagedList<UserInfos> userPagedList = list.ToPagedList(pageNumber, pageSize);

        //    ViewBag.modules = new SelectList(db.Departments.ToList(), "ID", "Name");
        //    //将分页处理后的列表传给View 
        //    return View(userPagedList);
        //}
        public ActionResult DelRoles(int id)
        {
            var r_User_Roles = new R_User_Roles
            {
                ID = id
            };
            db.Entry<R_User_Roles>(r_User_Roles).State = System.Data.Entity.EntityState.Deleted;
            db.SaveChanges();
            return RedirectToAction("Index");//返回Index页面
        }
      
        public ActionResult EditRole()
        {
            int id = int.Parse(Request["ids"].ToString());
            string name = Request["name"].ToString();
            UserInfos user = db.UserInfos.Find(id);
            List<R_User_Roles> list1 = db.R_User_Roles.Where(p => p.UserID == id).ToList();
            ViewBag.user = user;
            ViewBag.id = id;
            ViewBag.name = name;
            ViewBag.list = list1;
            List<Roles> list = db.Roles.ToList();
            return View(list);
        }
        [HttpPost]
        public ActionResult EditRole(int UserID,List<int> RoleID)
        {
            
                List<R_User_Roles> romvee = db.R_User_Roles.Where(p=>p.UserID==UserID).ToList();
                foreach (var items in romvee)
                {
                    db.R_User_Roles.Remove(items);
                    db.SaveChanges();
                }

            foreach (var item in RoleID)
            {
                var r_User_Roles = new R_User_Roles { 
                   UserID=UserID,RoleID=item
                };
                db.R_User_Roles.Add(r_User_Roles);
                db.SaveChanges();
            }  
            return RedirectToAction("Index");//返回Index页面
        }
        //[HttpPost]
        //public ActionResult Index(int DepartmentID,string Name,int? page)
        //{
        //    List<UserInfos> list;
        //    if (Name==""&&string.IsNullOrEmpty(DepartmentID.ToString()))
        //    {
        //        list = db.UserInfos.OrderByDescending(p => p.ID).ToList();
        //    }
        //    else
        //    {
        //        list= db.UserInfos.Where(s => s.DepartmentID == DepartmentID && s.Name.Contains(Name)).OrderByDescending(p => p.ID).ToList();
        //    }
        //    //第几页  
        //    int pageNumber = page ?? 1;
        //    //每页显示多少条  
        //    int pageSize = 4;
        //    //通过ToPagedList扩展方法进行分页  
        //    IPagedList<UserInfos> userPagedList = list.ToPagedList(pageNumber, pageSize);
        //    ViewBag.modules = new SelectList(db.Departments.ToList(), "ID", "Name");
        //    //将分页处理后的列表传给View 
        //    return View(userPagedList);
        //}
        /// <summary>
        /// 添加用户信息的方法
        /// </summary>
        /// <returns></returns>
        public ActionResult Add() 
        {
            ViewBag.modules = new SelectList(db.Departments.ToList(), "ID", "Name");
            List<Roles> list = db.Roles.ToList();
            return View(list);
        }

        

        /// <summary>
        /// 修改的方法
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public ActionResult Edit(int? UserID)
        {
            UserInfos userInfos = db.UserInfos.Find(UserID);
            ViewBag.img = userInfos.Photo;
            ViewBag.modules = new SelectList(db.Departments.ToList(), "ID", "Name");
            List<R_User_Roles> rur = db.R_User_Roles.Where(p => p.UserID == UserID).ToList();
            List<Roles> list = db.Roles.ToList();
            ViewBag.list = list;
            ViewBag.rur = rur;
            return View(userInfos);
        }
        /// <summary>
        /// 保存修改信息
        /// </summary>
        /// <param name="Photo"></param>
        /// <param name="userInfos"></param>
        /// <param name="RoleID"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Edit(HttpPostedFileBase Photo, UserInfos userInfos, List<int> RoleID)
        {
            if (Photo != null)
            {

                if (Photo.ContentLength == 0)//如果上传文件大小为零
                {
                    ViewBag.magees = "文件大小为零！！";
                    return View();
                }
                else
                {

                    string fileNames = Path.GetFileName(Photo.FileName);
                    if (fileNames.EndsWith("jpg")||fileNames.EndsWith("png")||fileNames.EndsWith("jpeg"))
                    {
                        Photo.SaveAs(Server.MapPath("~/imgaes/" + fileNames));//保存物理路径
                        userInfos.Photo = fileNames;
                    }
                   
                }
            }
            else
            {
                string imgName = Request["imgName"].ToString();
                userInfos.Photo = imgName;
            }
            db.Entry<UserInfos>(userInfos).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            foreach (var item in db.R_User_Roles.Where(p=>p.UserID==userInfos.ID).ToList())
            {
                db.R_User_Roles.Remove(item);
                db.SaveChanges();
            }
            foreach (var item in RoleID)
            {
                var rur = new R_User_Roles
                {
                    UserID = userInfos.ID,
                    RoleID = item
                };
                db.R_User_Roles.Add(rur);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        /// <summary>
        /// 保存添加的信息
        /// </summary>
        /// <param name="Photo"></param>
        /// <param name="userInfos"></param>
        /// <param name="RoleID"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Add(HttpPostedFileBase Photo, UserInfos userInfos,List<int> RoleID)
        {
            if (Photo != null)
            {
                if (Photo.ContentLength == 0)//如果上传文件大小为零
                {
                    ViewBag.magees = "文件大小为零！！";
                    return View();
                }
                else
                {
                    string fileNames = Path.GetFileName(Photo.FileName);
                    if (fileNames.EndsWith("jpg") || fileNames.EndsWith("png") || fileNames.EndsWith("jpeg"))
                    {
                        Photo.SaveAs(Server.MapPath("~/imgaes/" + fileNames));//保存物理路径
                        userInfos.Photo = fileNames;
                    }
                   
                }
            }
            db.UserInfos.Add(userInfos);
            db.SaveChanges();
 
            var userInfos1 = db.UserInfos.ToList().Last();
            if (RoleID!=null)
            {
                if (RoleID.Count != 0)
                {
                    foreach (var item in RoleID)
                    {
                        var rur = new R_User_Roles
                        {
                            UserID = userInfos1.ID,
                            RoleID = item
                        };
                        db.R_User_Roles.Add(rur);
                        db.SaveChanges();
                    }
                }
            }
            return RedirectToAction("Index");
        }
        public ActionResult Remove(int? UserID) {
           
            List<R_User_Roles> rur = db.R_User_Roles.Where(p => p.UserID == UserID).ToList();
            if (rur.Count>0)
            {
               // Response.Write("<script>alert('该用户有角色，不能删除！！')</script>");
                return Content("<script>alert('该用户有角色，不能删除！！');history.go(-1)</script>");
               // return RedirectToAction("Index");
            }
            else
            {
                foreach (var item in rur)
                {
                    db.R_User_Roles.Remove(item);
                    db.SaveChanges();
                }
                db.UserInfos.Remove(db.UserInfos.Find(UserID));
                db.SaveChanges();
               
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult DelAll()
        {
            //获取前台传过来的字符串数据：【3，4，5】
            string Id = Request["dellist"].ToString();
            //以逗号分割sId字符串
            string[] strarr = Id.Split(',');

            int count = 0;
            for (int i = 0; i < strarr.Length; i++)
            {
                int id = int.Parse(strarr[i]);
                if (db.R_User_Roles.Where(p => p.UserID == id).ToList().Count>0)
                {

                }
                else
                {
                    UserInfos user = db.UserInfos.Find(id);
                    db.UserInfos.Remove(user);
                    db.SaveChanges();
                    count++;
                }
            }
            if (strarr.Length == count)
            {
                return Content("所选全部删除成功。");
            }
            else
            {
                return Content("部分用户有角色，不能删除！！。");
            }

        }
    }
}