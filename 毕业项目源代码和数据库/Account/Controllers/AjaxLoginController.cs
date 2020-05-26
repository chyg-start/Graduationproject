using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Account.Models;
namespace Account.Controllers
{
    public class AjaxLoginController : Controller
    {
        AccountDBEntities db = new AccountDBEntities();
        // GET: AjaxLogin
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AjaxLogins() {
            return View();
        }
        public ActionResult CheckLoginName(string LoginName) {
            if (db.Student.FirstOrDefault(p=>p.StuLoginName==LoginName)!=null)
            {
                return Content("true");
            }
            else
            {
                return Content("false");
            }
           
        }
    }
}