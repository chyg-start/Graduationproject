using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Account.Models;
namespace Account.Controllers
{
    public class TopicsController : Controller
    {
        AccountDBEntities db = new AccountDBEntities();
        // GET: Topics
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Create(int PaperID)
        {
            @ViewBag.PaperID = PaperID;
            return View();
        }
        [HttpPost]
        public ActionResult Create(Topic topic)
        {
            db.Topic.Add(topic);
            db.SaveChanges();
            return RedirectToAction("Index","Papers");
        }
        public ActionResult Edit(int id)
        {
            Topic topic = db.Topic.Find(id);
            ViewBag.model = new SelectList(db.Paper.ToList(), "PaperID", "PaperName");
            return View(topic);
        }

        [HttpPost]
        public ActionResult Edit(Topic topic)
        {   
                db.Entry<Topic>(topic).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", "Papers",new {id=topic.PaperID });        
        }
        public ActionResult Delete(int id)
        {

            List<Detail> details = db.Detail.Where(p => p.TopicID == id).ToList();
            if (details.Count > 0)
            {
                return Content("<script>alert('该题目已作答正在使用！！，不能删除');history.go(-1)</script>");
            }
            else
            {
                Topic topic = db.Topic.Find(id);
                db.Topic.Remove(topic);
                db.SaveChanges();
                return RedirectToAction("Index", "Papers");
            }


        }
    }
}