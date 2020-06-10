using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ego_前台.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        //商品详情
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult List()
        {
            return View();
        }
        //购物车
        public ActionResult ShopCar() {
            return View();
        }
        //订单信息
        public ActionResult OderInfo()
        {
            return View();
        }
        //在线支付
        public ActionResult Payply()
        {
            return View();
        }

    }
}