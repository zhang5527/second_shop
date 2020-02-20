using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using second_shop.Models;
using Newtonsoft.Json;
using second_shop.filter;
namespace second_shop.Controllers
{
    public class UserController : Controller
    {
        public ActionResult AddRequire()
        {
            var require = JsonConvert.DeserializeObject<requirement>(Request["data"]);
            require.users_id = Convert.ToInt32(Session["userid"]);
            require.publish_time = DateTime.Now.ToString("yyyy-MM-dd");     
            BaseDal<requirement>.AddEntity(require);
            return Content("1");
        }

        public ActionResult infor()
        {
            //int userid = Convert.ToInt32(Session["userid"]);
            //var user = BaseDal<users>.firstordefault(a => a.id == userid);
            return View(/*user*/);
        }

        public ActionResult mytrack()
        {
            int userid = Convert.ToInt32(Session["userid"]);
            var user = BaseDal<users>.firstordefault(a => a.id == userid);
            return View(user.user_history.ToList());
        }

        public ActionResult mycollection()
        {
            int userid = Convert.ToInt32(Session["userid"]);
            var user = BaseDal<users>.firstordefault(a => a.id == userid);
            return View(user.collection.ToList());
        }
         public ActionResult delcollection()
        {
            int colid = Convert.ToInt32(Request["data"]);
            var col = BaseDal<collection>.firstordefault(a => a.id == colid);
            BaseDal<collection>.DeleteEntity(col);
            return Content("1");
        }
        public ActionResult mypassword()
        {            
            return View();
        }
        public ActionResult mupurcase()
        {
            int id = Convert.ToInt32(Session["userid"]);
            var user = BaseDal<users>.firstordefault(a => a.id == id);
            return View(user.requirement.ToList());
        }

        public ActionResult delpurchase()
        {
            int id = Convert.ToInt32(Request["data"]);
            BaseDal<requirement>.DeleteEntity(BaseDal<requirement>.firstordefault(a => a.id == id));
            return Content("1");
        }

        public ActionResult uploadheadimg()
        {
            if (Request.Files.Count == 0)
            {
                return Json(new
                {
                    code = 1,
                    msg = "无文件",
                    data = new
                    {
                        src = ""
                    }
                });
            }
            int id =Convert.ToInt32(Session["userid"]);
            var user = BaseDal<users>.firstordefault(a => a.id == id);
            //用户路径
            string dir1 = "/upload1/" + "Userheadimg" + "/";
            string path;
            string currentFileName = "";
            if (!Directory.Exists(Request.MapPath(dir1)))
            {
                Directory.CreateDirectory(Request.MapPath(dir1));
            }
            for (int i = 0; i < Request.Files.Count; i++)
            {
                string originalFileName = Request.Files[i].FileName;
                string fileExtension = originalFileName.Substring(originalFileName.LastIndexOf('.'), originalFileName.Length - originalFileName.LastIndexOf('.'));
                path = Request.MapPath(dir1);
                currentFileName = Guid.NewGuid() + fileExtension;
                Request.Files[i].SaveAs(path + currentFileName);
            }
            user.img_url = dir1 + currentFileName;
            BaseDal<users>.ModifyEntity(user);
            return Json(new
            {
                code = 0,
                msg = "ok",
                data = new
                {
                    src = dir1 + currentFileName
                }
            });
        }
        //更新个人信息
        public ActionResult updateinfor()
        {
            var obj=JsonConvert.DeserializeObject<users>(Request["data"]);
            int userid =Convert.ToInt32(Session["userid"]);
            var user = BaseDal<users>.firstordefault(a => a.id == userid);
            user.name = obj.name;
            user.signature = obj.signature;
            user.nickname = obj.nickname;
            user.school = obj.school;
            user.gender = obj.gender;
            BaseDal<users>.ModifyEntity(user);
            return Content("1");
        }
        [UserCheckLogin]
        //提交评论
        public ActionResult getcomment()
        {
            string content = Request["comment"];
            if (Session["userid"] == null||content=="")
            {
                return Content("0");
            }
            int p_id = Convert.ToInt32(Session["p_id"]);
            int uid = Convert.ToInt32(Session["userid"]);
            BaseDal<comment>.AddEntity(new comment { content = content, p_id = p_id, users_id = uid, publish_time = DateTime.Now.ToString("yyyy-MM-dd") });
            return Content("1");
        }
        [UserCheckLogin]
        //举报
        public ActionResult complain()
        {
            int p_id = Convert.ToInt32(Request["p_id"]);
            string content = Request["content"];                          
            int uid = Convert.ToInt32(Session["userid"]);            
            BaseDal<complain>.AddEntity(new complain { product_id = p_id, content = content,complaint_time=DateTime.Now.Date});
            return Content("<script>alert('举报成功，确认后跳转回原页面 ');history.go('-1')</script>");
        }

        public ActionResult changepassword()
        {
            int userid = Convert.ToInt32(Session["userid"]);
            var user = BaseDal<users>.firstordefault(a => a.id == userid);
            if (user.password != Request["oldpass"])
            {
                return Content("0");
            }
            user.password = Request["pass"];
            BaseDal<users>.ModifyEntity(user);
            return Content("1");
        }
    }
}