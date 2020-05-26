using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Account.Models;

namespace Account.Controllers
{
    public class PapersController : Controller
    {
        AccountDBEntities db = new AccountDBEntities();
        // GET: Papers
        public ActionResult Index()
        {
            List<Paper> list = db.Paper.ToList();
         
            return View(list);
        }
        public ActionResult Edit(int id)
        {
            Paper paper = db.Paper.Find(id);

            return View(paper);
        }

        [HttpPost]
        public ActionResult Edit(Paper paper)
        {
            if (ModelState.IsValid)
            {
                db.Entry<Paper>(paper).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }
        public ActionResult Delete(int id)
        {
           
            List<Topic> topic = db.Topic.Where(p=>p.PaperID==id).ToList();
            if (topic.Count>0)
            {
                return Content("<script>alert('该试卷还有题目正在使用！！，不能删除');history.go(-1)</script>");
            }
            else
            {
                Paper paper = db.Paper.Find(id);
                db.Paper.Remove(paper);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }
        public ActionResult Details(int id)
        {
            Paper paper = db.Paper.Find(id);
            return View(paper);
        }
        public ActionResult IndexStu()
        {
            List<Paper> paper = db.Paper.ToList();
            return View(paper);
          
        }
        
    }
}