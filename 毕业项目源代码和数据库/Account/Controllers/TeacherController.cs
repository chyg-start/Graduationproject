using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Account.Models;
using PagedList;

namespace Account.Controllers
{
    public class TeacherController : Controller
    {
        AccountDBEntities db = new AccountDBEntities();
        // GET: Teacher
       
        public ActionResult Index(int? page) {
            List<Teacher> list = db.Teacher.OrderByDescending(p => p.TeacherID).ToList();
            int pageNumber = page ?? 1;
            //每页显示多少条  
            int pageSize = 5;
            //通过ToPagedList扩展方法进行分页  
            IPagedList<Teacher> TeacherPagedList = list.ToPagedList(pageNumber, pageSize);
            return View(TeacherPagedList);
        }
        public ActionResult Create() {

            return View();
        }
        [HttpPost]
        public ActionResult Create(Teacher teacher)
        {
            if (ModelState.IsValid)
            {
                db.Teacher.Add(teacher);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
          
        }
        public ActionResult Edit(int id) {
            Teacher teacher = db.Teacher.Find(id);

            return View(teacher);
        }

        [HttpPost]
        public ActionResult Edit(Teacher teacher)
        {        
                db.Entry<Teacher>(teacher).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            
        }
        public ActionResult Delete(int id)
        {
            Teacher teacher = db.Teacher.Find(id);
            db.Teacher.Remove(teacher);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Details(int id)
        {
            Teacher teacher = db.Teacher.Find(id);
            return View(teacher);
        }
        [HttpPost]
        public ActionResult DelAll()
        {
            //获取前台传过来的字符串数据：【3，4，5】
            string Id = Request["dellist"].ToString();
          
            //以逗号分割sId字符串
            string[] strarr = Id.Split(',');

            int count = 0;
            for (int i = 0; i < strarr.Length; i++) {
               int id = int.Parse(strarr[i]);
                if (db.Answer.FirstOrDefault(p=>p.TeacherID==id)!=null)
                {  
                    
                }
                else
                {
                    Teacher tea = db.Teacher.Find(id);
                    db.Teacher.Remove(tea);
                    db.SaveChanges();
                    count++;         
                }
            }
            if (strarr.Length==count)
            {
                return Content("所选全部删除成功。已删除"+count+"条数据");
            }
            else
            {
                return Content("部分老师正在使用！！不能删除。已删除" + count + "条数据");
            }
          
        }

    }
}