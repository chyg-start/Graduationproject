using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Account.Models;

namespace Account.Controllers
{
    public class Topics1Controller : Controller
    {
        private AccountDBEntities db = new AccountDBEntities();

        // GET: Topics1
        public ActionResult Index()
        {
            var topic = db.Topic.Include(t => t.Paper);
            return View(topic.ToList());
        }

        // GET: Topics1/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Topic topic = db.Topic.Find(id);
            if (topic == null)
            {
                return HttpNotFound();
            }
            return View(topic);
        }

        // GET: Topics1/Create
        public ActionResult Create(int PaperID)
        {
            Paper paper = db.Paper.Find(PaperID);
            ViewBag.Paper = paper;
            //试卷类型存储试卷类型// SelectListItem
            List<SelectListItem> listItems = new List<SelectListItem>() {
              new SelectListItem(){Value="1",Text="单选" },
              new SelectListItem(){Value="2",Text="判断" },
              new SelectListItem(){Value="3",Text="问答题" },
            };
            ViewBag.type = new SelectList(listItems, "Value", "Text");
            return View();
        }

        // POST: Topics1/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TopicID,TopicExplain,TopicScore,TopicType,TopicA,TopicB,TopicC,TopicD,TopicSort,TopicAnswer,PaperID")] Topic topic)
        {
            if (ModelState.IsValid)
            {
                db.Topic.Add(topic);
                db.SaveChanges();
                return RedirectToAction("Details","Papers1",new { PaperID = topic.PaperID });
            }

           // ViewBag.PaperID = new SelectList(db.Paper, "PaperID", "PaperName", topic.PaperID);
            return RedirectToAction("Create",new { PaperID=topic.PaperID });
        }

        // GET: Topics1/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Topic topic = db.Topic.Find(id);
            if (topic == null)
            {
                return HttpNotFound();
            }
            ViewBag.PaperID = new SelectList(db.Paper, "PaperID", "PaperName", topic.PaperID);
            List<SelectListItem> listItems = new List<SelectListItem>() {
              new SelectListItem(){Value="1",Text="单选" },
              new SelectListItem(){Value="2",Text="判断" },
              new SelectListItem(){Value="3",Text="问答题" },
            };
            ViewBag.type = new SelectList(listItems, "Value", "Text",topic.TopicType);
            return View(topic);
        }

        // POST: Topics1/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TopicID,TopicExplain,TopicScore,TopicType,TopicA,TopicB,TopicC,TopicD,TopicSort,TopicAnswer,PaperID")] Topic topic)
        {
            if (ModelState.IsValid)
            {
                db.Entry(topic).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", "Papers1", new { id = topic.PaperID });
            }
            return RedirectToAction("Edit", new { PaperID = topic.PaperID });
        }

        // GET: Topics1/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Topic topic = db.Topic.Find(id);
            if (topic == null)
            {
                return HttpNotFound();
            }
            return View(topic);
        }

        // POST: Topics1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            List<Detail> list = db.Detail.Where(p=>p.TopicID==id).ToList();
            Topic topic = db.Topic.Find(id);
            if (list.Count>0)
            {
                ModelState.AddModelError("erro","已作答,不可删除");
                return View(topic);
            }
            db.Topic.Remove(topic);
            db.SaveChanges();
            return RedirectToAction("Details", "Papers1", new { id = topic.PaperID });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
