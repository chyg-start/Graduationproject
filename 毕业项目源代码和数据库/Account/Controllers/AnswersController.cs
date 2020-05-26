using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Account.Models;
using PagedList;
namespace Account.Controllers
{
    public class AnswersController : Controller
    {
        AccountDBEntities db = new AccountDBEntities();
        // GET: Answers
        public ActionResult Index(int?page)
        {
            List<Answer> answer = db.Answer.OrderByDescending(p=>p.AnswerTime).ToList();
            //第几页
            int PageNmber = page ?? 1;
            //每页显示几条数据
            int PageSize=4;
            IPagedList<Answer> answersList = answer.ToPagedList(PageNmber,PageSize);
            return View(answersList);
        }
        public ActionResult TeAnswerDetail(int id)
        {
            Answer answer = db.Answer.Find(id);        
            List<Detail> list = db.Detail.Where(w=>w.AnswerID==id).ToList();
            ViewBag.Detaillist = list;
            return View(answer);
        }
        /// <summary>
        /// 提交审核
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public void ToSmbuit(int AnswerID,int Sorec,int ss)
        {
            Teacher teacher = Session["user"] as Teacher;
            Answer answer = db.Answer.Find(AnswerID);
            answer.TeacherID = teacher.TeacherID;
            answer.AnswerScore = Sorec+ss;
            answer.AnswerState = 2;
            db.SaveChanges();
          
        }
        
        public ActionResult MyAnswerDetail(int id)
        {
            Answer answer = db.Answer.Find(id);
            answer.AnswerState = 2;
           
            List<Detail> list = db.Detail.Where(w => w.AnswerID == id).ToList();
            ViewBag.Detaillist = list;
            return View(answer);
        }
        
        public ActionResult BeginAnswer(int PaperID)
        {
           // List<Topic> topics = db.Topic.Where(p=>p.PaperID==PaperID).OrderBy(p=>p.TopicSort).ToList();
            Paper paper = db.Paper.Find(PaperID);
            Student stu = Session["user"] as Student;
            Answer answer = new Answer {
                PaperID = paper.PaperID,
                StuID = stu.StuID,
                AnswerScore = 0,
                AnswerTime = DateTime.Now
            };
            db.Answer.Add(answer);
            db.SaveChanges();
            Answer Nowanswer = db.Answer.LastOrDefault();
            ViewBag.answerID = Nowanswer.AnswerID;
            //ViewBag.paper = paper;
            return View(paper);
        }
        public ActionResult AnswersDetail(int ParerID, int StuID)
        {
            // List<Topic> topics = db.Topic.Where(p=>p.PaperID==PaperID).OrderBy(p=>p.TopicSort).ToList();
            Paper paper = db.Paper.Find(ParerID);
            int aid = 0;
            Answer aw = db.Answer.SingleOrDefault(p=>p.PaperID== ParerID&&p.StuID==StuID);
            if (aw==null)
            {
                Answer answer = new Answer
                {
                    PaperID = paper.PaperID,
                    StuID = StuID,
                    AnswerScore = 0,
                    AnswerTime = DateTime.Now,
                    SubmitTime = null,
                    TeacherID = null
                };
                db.Answer.Add(answer);
                db.SaveChanges();
                aid = answer.AnswerID;
            }
            else
            {
                aid = aw.AnswerID;
            }
            Answer asw = db.Answer.Find(aid);
            //查询该试卷的答题情况
            List<Detail> delList = db.Detail.Where(p => p.AnswerID ==aid).ToList();
            if (delList.Count > 0 && delList != null)
            {
                return View(asw);
            }
            else
            {
                //添加该试卷所有题目的答题表
                List<Topic> topList = db.Topic.Where(p => p.PaperID == ParerID).ToList();
                foreach (var item in topList)
                {
                    Detail detail = new Detail() { TopicID = item.TopicID, AnswerID = aid, DetailAnswer = "" };
                    db.Detail.Add(detail);
                    db.SaveChanges();
                }
               
                return View(asw);
            }

           
        }
        
        public ActionResult MyAnswer(int StuID)
        {
            List<Answer> list = db.Answer.Where(p => p.StuID == StuID).ToList();
            return View(list);
        }
        public ActionResult _Topic(int aid,int sort)
        {
            Topic topic = db.Topic.Where(p=>p.TopicSort==sort&&p.PaperID==aid).SingleOrDefault();

            return Content("123");
        }
        public ActionResult AllDetailStu(int aid)
        {
            List<Topic> list = db.Topic.Where(p => p.PaperID == aid).ToList();

            return PartialView("list",list);
        }
        [HttpPost]
        public void Index(int topicid, int aid, string DetailAnswer)
        {
            Detail del = db.Detail.SingleOrDefault(p => p.AnswerID == aid && p.TopicID == topicid);
            del.DetailAnswer = DetailAnswer;
            db.SaveChanges();
        }

        [HttpGet]
        public void ChangeDetailAnswer(int AnwserID, int id, string DetailAnswer)
        {
            Detail detail = db.Detail.SingleOrDefault(p =>p.AnswerID==AnwserID&& p.AnswerID == AnwserID && p.TopicID == id);
            detail.DetailAnswer = DetailAnswer;
            db.SaveChanges();
        }
        [HttpGet]
        public ActionResult ChangeAnser(int AnwserID,int id,string DetailAnswer)
        {
            Detail detail = db.Detail.SingleOrDefault(p => p.AnswerID == AnwserID && p.TopicID == id && p.DetailAnswer == DetailAnswer);
            if (detail!=null)
            {
                detail = new Detail
                {              
                    DetailAnswer = DetailAnswer
                };
                db.Entry<Detail>(detail).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
            else
            {
                Detail detaisl = new Detail
                {
                    AnswerID=AnwserID,
                    TopicID = id,
                    DetailAnswer = DetailAnswer
                };
                db.Detail.Add(detaisl);
                db.SaveChanges();
            }
           

           return Content("list");
        }
        [HttpGet]
        public ActionResult SubmitPaper(int AnswerID)
        {
            Answer answer = db.Answer.Find(AnswerID);
            
            answer.AnswerState = 1;
            answer.SubmitTime = DateTime.Now;
           
            db.SaveChanges();
            return RedirectToAction("MyAnswer",new { StuID= answer.StuID });
        }
        
    }
}