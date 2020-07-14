using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Health.WebUI.Controllers
{
    public class DoctorProfileController : Controller
    {
        // GET: DoctorProfile
        [Authorize]
        public ActionResult Index()
        {
            return View(GetData("Index"));
        }

        [Authorize(Roles = "Doctors")]
        public ActionResult OtherAction()
        {
            return View("Index", GetData("OtherAction"));
        }

        private Dictionary<string, object> GetData(string actionName)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();

            dict.Add("Action", actionName);
            dict.Add("Пользователь", HttpContext.User.Identity.Name);
            dict.Add("Аутентифицирован?", HttpContext.User.Identity.IsAuthenticated);
            dict.Add("Тип аутентификации", HttpContext.User.Identity.AuthenticationType);
            dict.Add("В роли Doctors?", HttpContext.User.IsInRole("Doctors"));

            return dict;
        }
    }
}