using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DemoAuthentication.ServiceReference1;

namespace DemoAuthentication.Controllers
{
    public class HomeController : Controller
    {
        Level2ServiceClient proxy = new Level2ServiceClient();
        public ActionResult Index()
        {
            
            if (User.Identity.Name == "")
            {
                ViewBag.Message = "Hệ Thống Quản Lý Tạm Trú Tạm Vắng!";
                return View("About");
            }
            else
            {
                int? role = proxy.RoleID(User.Identity.Name);
                if (role == 0 || role == 2)
                {
                    return View("AccessDeny");
                }
                else
                {
                    if (role == 1)
                    {
                        return View("Admin");
                    }
                    else
                    {
                        ViewData["user"] = User.Identity.Name;
                        return View("User");
                    }
                }
            }

           
        }
       
        public ActionResult About()
        {
            return View();
        }
    }
}
