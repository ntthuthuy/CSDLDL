using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TechLife.SSO.Controllers
{
    public class AuthenticateController : Controller
    {
        // GET: Authenticate\
        [HttpPost]
        public ActionResult Index(string token)
        {
            string code = MaDangNhap.GetMaDangNhap(token);
            return Json(XacThucSso.XacThucDangNhap(code), JsonRequestBehavior.AllowGet);
        }
    }
}