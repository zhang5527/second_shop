using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace second_shop.filter
{
    public class UserCheckLogin: AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (SkipNoSign(filterContext))//是否该类标记为NoSign，如果是则不需要判断
            {
                base.OnAuthorization(filterContext);
                return;
            }
            if (filterContext.HttpContext.Session["userid"] == null)
            {
                ContentResult content = new ContentResult() { Content = "<script>alert('请先进行登录'); location.href('/home/index')</script>" };
                filterContext.Result = content;
            }
        }
        /// <summary>
        /// 使用验证时 [NoSign] 标注不需要登录和权限验证
        /// </summary>
        [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true)]
        public class NoSignAttribute : Attribute
        {
        }

        //操作是否需要判断
        private static bool SkipNoSign(AuthorizationContext actionContext)
        {
            return actionContext.ActionDescriptor.GetCustomAttributes(typeof(NoSignAttribute), true).Length == 1;//有NoSign属性  true
        }

    }
}