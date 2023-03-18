using EsbUsers.Sso;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace TechLife.SSO.Controllers
{
    public class HomeController : Controller
    {
        string RedirectUrl = System.Configuration.ConfigurationManager.AppSettings["APP_RETURN_URL"];
        string RedirectLogoutUrl = System.Configuration.ConfigurationManager.AppSettings["APP_RETURN_URL_LOGOUT"];
        string AppKey = System.Configuration.ConfigurationManager.AppSettings["APP_AccessKey"];

        public ActionResult Index()
        {
            if (ClientSso.Ins.CurrentSessionLoginInfo != null)
            {
                ClientSso.Ins.XacThucNguoiDung();
                var user = ClientSso.Ins.CurrentSessionLoginInfo;
                if (user != null)
                {
                    string token = HashUtil.Encrypt(user.TenDangNhap + "|" + user.HoVaTen + "|" + user.TenDonVi + "|" + DateTime.Now.ToString());
                    string code = Guid.NewGuid().ToString();
                    var isTaoMa = MaDangNhap.TaoMaDangNhap(code, token);
                    return Redirect(RedirectUrl + "/?token=" + code);
                }
                else
                {
                    return RedirectToAction("login", new { ReqId = "b26e13b8" });
                }
            }
            else
            {
                return RedirectToAction("login", new { ReqId = "b26e13b8" });
            }
        }
        public ActionResult Login(string RedirectUrl, int count = 0)
        {
            if (count <= 3)
            {
                if (UriUtil.RequestId.Equals(ClientSso.ReqStatus.LOGIN_SSO) || UriUtil.RequestId.Equals(ClientSso.ReqStatus.TOKEN_SSO))
                {
                    if (ClientSso.Ins.CurrentSessionLoginInfo != null)
                    {
                        return RedirectToAction("Index", new { ReqId = "b26e13b8" });
                        //Todo: Bổ sung xác thực riêng cho phần mềm tích hợp
                    }
                }
                else// TH gọi kiểm tra xác thực
                {
                    count++;
                    ClientSso.Ins.XacThucNguoiDung();
                }
                return RedirectToAction("login", new { count = count, ReqId = "b26e13b8" });
            }
            else
            {
                ClientSso.Ins.DangXuat();
                return View();
            }
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Login(LoginModel request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }
            var login = ClientSso.Ins.DangNhap(request.UserName, request.Password);
            if (!login.IsError)
            {
                ClientSso.Ins.XacThucNguoiDung();
                return RedirectToAction("Index");
            }
            ModelState.AddModelError(string.Empty, "Tài khoản hoặc mật khẩu không đúng");
            return View(request);
        }

        [HttpGet]
        public ActionResult Logout()
        {
            var user = ClientSso.Ins.CurrentSessionLoginInfo;
            if (user != null)
            {
                ClientSso.Ins.XoaChungThuHienHanh();
            }
            return Redirect(RedirectLogoutUrl);
        }
    }
}