using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Learun.Application.Web.Areas.LR_WebChatModule.Controllers
{
    public class UserController : Controller
    {
        // GET: LR_WebChatModule/User
        public ActionResult Index()
        {
            return View();
        }
    }
}