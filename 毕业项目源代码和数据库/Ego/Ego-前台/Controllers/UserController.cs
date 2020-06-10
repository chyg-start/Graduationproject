using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ego_前台.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        //用户中心
        public ActionResult Index()
        {
            return View();
        }
        //登录
        public ActionResult Login()
        {
            return View();
        }
        //注册
        public ActionResult Register()
        {
            return View();
        }
        //修改密码
        public ActionResult UpdatePwd() {
            return View();
        }
        //修改密码
        public ActionResult MyOrder()
        {
            return View();
        }

        public ActionResult AC() {
            return View();
        }

    }
}