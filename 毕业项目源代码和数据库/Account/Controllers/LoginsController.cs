using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Account.Models;
namespace Account.Controllers
{
    public class LoginsController : Controller
    {
        AccountDBEntities db = new AccountDBEntities();
        // GET: Logins
        public ActionResult Index()
        {
            return View();
        }
     
        public ActionResult LoginStudent()
        {
            return View();
        }
        public ActionResult LoginTeacher()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LoginTeacher(Teacher tea)
        {
            Teacher teacher = db.Teacher.Where(p => p.TeacherLoginName == tea.TeacherLoginName && p.TeacherLoginPwd == tea.TeacherLoginPwd).SingleOrDefault();
            if (ModelState.IsValid)
            {
                  if (teacher != null)
                {
                    Session["user"] = teacher;
                    Session["name"] = teacher.TeacherName + "老师";
                    Session["menus"] ="1";
                    return RedirectToAction("Index");
                }
                else
                {
                    return Content("<script>alert('账号或者密码错误，请重试！！');history.go(-1)</script>");
                }
            }
            else
            {
                return View();
            }
           
        }

        [HttpPost]
        public ActionResult LoginStudent(Student stu)
        {
            if (ModelState.IsValid)
            {
                Student student = db.Student.Where(p => p.StuLoginName == stu.StuLoginName && p.StuLoginPwd == stu.StuLoginPwd).SingleOrDefault();
                if (student != null)
                {
                    Session["user"] = student;
                    Session["name"] = student.StuName + "学生";
                    Session["menus"] = "2";
                    return RedirectToAction("Index");
                }
                else
                {
                    return Content("<script>alert('账号或者密码错误，请重试！！');history.go(-1)</script>");
                }
            }
            else
            {
                return View();
            }
                
        }
        public ActionResult Logout() 
        {
            Session.Abandon();//清除全部Session
            return RedirectToAction("Index");
        }




    }
}