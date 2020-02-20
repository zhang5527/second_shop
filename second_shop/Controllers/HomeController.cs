using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using second_shop.Models;

namespace second_shop.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            List<product> products = new List<product>();
            int totalcount = 0;
            int page=1;
            System.Linq.Expressions.Expression<Func<product, bool>> wherelambdal = a => a.signature != "下架";
            if (Request["search"] != null && Request["search"] != "")
            {
                wherelambdal = a => a.signature != "下架" && (a.title.Contains(Request["search"]) || a.profile.Contains(Request["search"]));
            }
            if (Request["type"] != null && Request["type"] != "")
            {
                wherelambdal = a => a.signature != "下架" && (a.depart==Request["type"]);
            }
            if (Request["page"]==null||Request["page"]=="")                
            products=BaseDal<product>.LoadEntitiesByPages(1,20,out totalcount, wherelambdal, a=>a.publish_time,true).ToList();
            else
            {
                page = Convert.ToInt32(Request["[page"]);
                products = BaseDal<product>.LoadEntitiesByPages(page, 20, out totalcount, wherelambdal, a => a.publish_time, true).ToList();
            }
            return View(products);
        }
        
        public ActionResult login()
        {
            string account = Request["account"];
            string password = Request["password"];
            var user =BaseDal<users>.firstordefault(a => a.account_state == 1 && a.useraccount == account && a.password == password);
            if(user!=null)
            {
                Session["userid"] = user.id;
                return Redirect("/home/index");
            }
            return Content("<script>alert('请检查用户名和密码');history.go('-1')</script>");
        }
        
        public ActionResult register()
        {
            string account = Request["account"];
            string password = Request["password"];
            try
            {
                var user = BaseDal<users>.AddEntity(new users
                {
                    useraccount = account,
                    password = password,
                    account_state = 1
                });
                Session["userid"] = user.id;
                return Content("<script>alert('注册成功');history.go('-1')</script>");
            }
            catch (Exception)
            {
                return Content("<script>alert('注册失败，请检查用户名和密码 ');history.go('-1')</script>");
            }
        }

        public ActionResult product()
        {
            int p_id = Convert.ToInt32(Request["p_id"]);
            var pro = BaseDal<product>.firstordefault(a => a.id == p_id);
            pro.view_count++;           
            Session["p_id"] = p_id;
            BaseDal<product>.ModifyEntity(pro);
            if (Session["userid"] != null)
            {
                int userid = Convert.ToInt32(Session["userid"]);
                var user = BaseDal<users>.firstordefault(a => a.id == userid);
                BaseDal<user_history>.AddEntity(new user_history { p_id = p_id, users_id = userid });                                        
            }
            return View(pro);
        }

        public ActionResult getstate()
        {
            if (Session["userid"] == null) return Content("0");
            else return Content("1");
        }
    }
}