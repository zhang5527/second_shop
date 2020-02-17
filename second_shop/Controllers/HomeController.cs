using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace second_shop.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (Request["type"] != "")
            {
                
            }
            return View();
        }
        
        public ActionResult login()
        {
            return View();   
        }
        
        public ActionResult register()
        {
            return View();
        }
    }
}