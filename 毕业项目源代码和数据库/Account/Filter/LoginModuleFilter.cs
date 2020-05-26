using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Account.Filter
{
    public class LoginModuleFilter : IHttpModule
    {
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="context"></param>
        public void Init(HttpApplication context)
        {
            //原因：AcquireRequestState能获取Session
            context.AcquireRequestState += Context_AcquireRequestState;
        }
        /// <summary>
        /// 处理事件（拦截登录的过滤器）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Context_AcquireRequestState(object sender, EventArgs e)
        {
            //获得应用请求
            HttpApplication app = sender as HttpApplication;
            //捕获、可以点到四个内置对象、获取当前请求Http特定信息
            HttpContext context = app.Context;
            //获得浏览器端路径/例如：https://localhost:44394/Login/Index
            string url = context.Request.Url.ToString();
            //不过滤下列文件
            if (url.ToLower().Contains("css") || url.ToLower().Contains("js") || url.ToLower().Contains("jpg") || url.ToLower().Contains("png")||url.ToLower().Contains("fonts"))
            {
                return;
            }
            else
            {
                //请求地址是否包括Login/Index登录界面
                if (!url.ToLower().Contains("/login/index"))
                {
                    //判断是否登录
                    if (context.Session["User"] == null)
                    {
                        //发送地址到浏览器到登录地址
                        context.Response.Redirect("/Login/Index");
                    }
                }
            }
        }
    }
}