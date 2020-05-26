using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Account.Filter
{
    /// <summary>
    /// 过滤器命名规则：名+Attribute
    /// 引用命名空间using System.Web.Mvc;
    /// </summary>
    public class LoginAttribute:ActionFilterAttribute
    {
        //正在请求发送时，进行响应
        //public override void OnActionExecuting(ActionExecutingContext filterContext)
        //{
        //    //如果获得请求Session["User"]的值为空，就跳转登录界面
        //    if (HttpContext.Current.Session["User"]==null)
        //    {
        //       // HttpContext.Current.Response.Redirect("/Login/Index");
        //        //筛选器获得的请求的结果
        //        filterContext.Result = new RedirectResult("/Login/Index");
             
        //    }
        //}
       
    }
}