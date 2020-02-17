using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using second_shop.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;

namespace second_shop.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        #region 用户操作
        public ActionResult Users()
        {
            int totalcount = 0;
            int page = 1;
            List<users> users;
            ViewBag.cur = 1;
            if (Request["page"] == null)
            {
                users = BaseDal<users>.LoadEntitiesByPages(1, 10, out totalcount, a => a != null, a => a.login_time, true).ToList();
            }
            else
            {
                page = Convert.ToInt32(Request["page"]);
                ViewBag.cur = page;
                users = BaseDal<users>.LoadEntitiesByPages(page, 10, out totalcount, a => a != null, a => a.login_time, true).ToList();
            }

            return View(users);
        }
        public ActionResult adduser()
        {
            string jdata = Request["data"];
            var obj = JsonConvert.DeserializeObject<users>(jdata);
            BaseDal<users>.AddEntity(obj);
            return Content("1");
        }

        public ActionResult delete()
        {
            IEnumerable<int> ids = Request["id"].Split(',').Select(a => Convert.ToInt32(a));
            List<users> users = new List<users>();
            foreach (var item in ids)
            {
                users.Add(new users { id = item });
            }
            BaseDal<users>.DeleteByIds(users);
            return Content("1");
        }

        public ActionResult banuser()
        {
            IEnumerable<int> ids = Request["id"].Split(',').Select(a => Convert.ToInt32(a));
            List<users> users = new List<users>();
            foreach (var item in ids)
            {
                var a = BaseDal<users>.firstordefault(x => x.id == item);
                a.account_state = 0;
                users.Add(a);
            }
            BaseDal<users>.UpdateMany(users);
            return Content("1");
        }

        public ActionResult ChangeUserState()
        {
            int id = Convert.ToInt32(Request["id"]);
            var a = BaseDal<users>.firstordefault(x => x.id == id);
            if (a.account_state == 0)
            {
                a.account_state = 1;
            }
            else
            {
                a.account_state = 0;
            }
            BaseDal<users>.ModifyEntity(a);
            return Content("1");
        }
        #endregion
        public ActionResult Index()
        {
            ViewBag.UsersCount = BaseDal<users>.LoadEntities(a => a != null).Count();
            ViewBag.CommentCount = BaseDal<comment>.LoadEntities(a => a != null).Count();
            ViewBag.NewUserCount = BaseDal<users>.LoadEntities(a => a.login_time == DateTime.Now.ToString("yyyy-MM-dd")).Count();
            ViewBag.NewCommentCount = BaseDal<users>.LoadEntities(a => a.login_time == DateTime.Now.ToString("yyyy-MM-dd")).Count();
            return View();
        }
        #region 管理员列表 
        public ActionResult admins()
        {
            int totalcount = 0;
            int page = 1;
            List<admin> admins;
            ViewBag.cur = 1;
            if (Request["page"] == null)
            {
                admins = BaseDal<admin>.LoadEntitiesByPages(1, 10, out totalcount, a => a != null, a => a.useraccount, true).ToList();
            }
            else
            {
                page = Convert.ToInt32(Request["page"]);
                ViewBag.cur = page;
                admins = BaseDal<admin>.LoadEntitiesByPages(page, 10, out totalcount, a => a != null, a => a.password, true).ToList();
            }
            return View(admins);
        }

        public ActionResult deleteadmin()
        {
            IEnumerable<int> ids = Request["id"].Split(',').Select(a => Convert.ToInt32(a));
            List<admin> admin = new List<admin>();
            foreach (var item in ids)
            {
                admin.Add(new admin { id = item });
            }
            BaseDal<admin>.DeleteByIds(admin);
            return Content("1");
        }

        public ActionResult addadmin()
        {
            string jdata = Request["data"];
            var obj = JsonConvert.DeserializeObject<admin>(jdata);
            BaseDal<admin>.AddEntity(obj);
            return Content("1");
        }
        #endregion

        public ActionResult product()
        {
            string type = Request["type"];
            int totalcount = 0;
            int page = 1;
            if (Request["page"] != null)
            {
                page = Convert.ToInt32(Request["page"]);
            }
            List<product> products=new List<product>();
            ViewBag.cur = 1;
            System.Linq.Expressions.Expression<Func<product, bool>> WhereLambda = a => a.isadmin == 0;
            switch (type)
            {
                case "users": {
                        WhereLambda = a => a.isadmin == 0;
                        break;    
            }
                case "admin":
            {
                        WhereLambda = a => a.isadmin == 1;
                        break;
            }
                default:break;
            }
            products = BaseDal<product>.LoadEntitiesByPages(page, 10, out totalcount, WhereLambda, a => a.publish_time, true).ToList();
            return View(products);
        }
        public ActionResult AddProduct()
        {
            var pro = JsonConvert.DeserializeObject<product>(Request["data"]);
            pro.publish_time = DateTime.Now.ToString("yyyy-MM-dd");
            pro.isadmin = 1;
            pro.signature = "正常";
            BaseDal<product>.AddEntity(pro);
            return Content("1");
        }
        public ActionResult GetProById()
        {
            int id = Convert.ToInt32(Request["data"]);
            var pro=BaseDal<product>.firstordefault(a => a.id == id);
            string jstr = JsonConvert.SerializeObject(new { pro.title,pro.id, pro.old_price, pro.phone, pro.profile, pro.wechat, pro.qq, pro.depart });
            return Content(jstr);
        }
        public ActionResult ChangeProductState()
        {
             int id= Convert.ToInt32(Request["id"]);
             var pro =BaseDal<product>.firstordefault(a => a.id == id);
            if (pro.signature == "正常") pro.signature = "下架";
            else pro.signature = "正常";
            BaseDal<product>.ModifyEntity(pro);
            return Content("1");
        }
        public ActionResult uploadfile()
        {
            if (Request.Files. Count== 0)
            {
                return Json(new
                {
                    code=1,
                    msg="无文件",
                    data = new
                    {
                        src=""
                    }
                });
            }
            //用户路径
            string dir1 = "/upload1/" + "UserImgs" + "/";
            string path;
            string currentFileName="";
            if (!Directory.Exists(Request.MapPath(dir1)))
            {
                Directory.CreateDirectory(Request.MapPath(dir1));
            }
            for (int i = 0; i < Request.Files.Count; i++)
            {
                string originalFileName = Request.Files[i].FileName;
                string fileExtension = originalFileName.Substring(originalFileName.LastIndexOf('.'), originalFileName.Length - originalFileName.LastIndexOf('.'));
                path = Request.MapPath(dir1);
                currentFileName= Guid.NewGuid() + fileExtension;
                Request.Files[i].SaveAs(path + currentFileName);
            }
            return Json(new
            {
                code = 0,
                msg = "ok",
                data = new
                {
                    src = dir1+currentFileName
                }
            });
        }


    }
}