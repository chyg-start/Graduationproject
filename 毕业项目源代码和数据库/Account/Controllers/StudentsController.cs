using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Account.Models;

namespace Account.Controllers
{
    public class StudentsController : Controller
    {
        AccountDBEntities db = new AccountDBEntities();
        // GET: Students
        public ActionResult Index()
        {
            List<Student> list = db.Student.ToList();
            return View(list);
        }
        public ActionResult Create()
        {

            return View();
        }
        [HttpPost]
        public ActionResult Create(Student student)
        {
            if (ModelState.IsValid)
            {
                db.Student.Add(student);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }

        }
        public ActionResult Edit(int id)
        {
            Student student = db.Student.Find(id);

            return View(student);
        }

        [HttpPost]
        public ActionResult Edit(Student student)
        {
                db.Entry<Student>(student).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
        }
        public ActionResult Delete(int id)
        {
            List<Answer> answers = db.Answer.Where(p=>p.StuID==id).ToList();
            if (answers.Count>0)
            {
                return Content("<script>alert('该同学还有题目！！，不能删除');history.go(-1)</script>");
            }
            else
            {
                Student student = db.Student.Find(id);
                db.Student.Remove(student);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
           
        }
        public ActionResult Details(int id)
        {
            Student student = db.Student.Find(id);
            return View(student);
        }
    }
}