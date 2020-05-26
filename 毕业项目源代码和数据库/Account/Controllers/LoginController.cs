using Account.Models;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.SessionState;

namespace Account.Controllers
{
    public class LoginController : Controller
    {
        AccountDBEntities db = new AccountDBEntities();
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string account, string pwd)
        {
            UserInfos user = db.UserInfos.Where(p => p.Account == account && p.Pwd == pwd).SingleOrDefault();
            if (user == null)
            {
                return Content("<script>alert('账号或者密码错误，请重试！！');history.go(-1)</script>");

            }
            else
            {
                List<V_User_Role_Menus> ru = db.V_User_Role_Menus.Where(p => p.UserID == user.ID).DistinctBy(s=>s.Name).ToList();
               
                Session["User"] = user;
                Session["ru"] = ru;
            }
            return RedirectToAction("Index", "Home");
        }
        public ActionResult Loginoff()
        {
            Session["User"] = null;
            Session["ru"] = null;
            return RedirectToAction("Index");
        }

        //public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        //{
        //    HashSet<TKey> seenKeys = new HashSet<TKey>();
        //    foreach (TSource element in source)
        //    {
        //        if (seenKeys.Add(keySelector(element)))
        //        {
        //            yield return element;
        //        }
        //    }


        //} 
    }
}