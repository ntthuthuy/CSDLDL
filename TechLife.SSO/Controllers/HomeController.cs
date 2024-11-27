using EsbUsers.Sso;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TechLife.SSO.ApiClients;
namespace TechLife.SSO.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            int abc = 5;
            if (ClientSso.Ins.CurrentSessionLoginInfo != null)
            {
                ClientSso.Ins.XacThucNguoiDung();
                var user = ClientSso.Ins.CurrentSessionLoginInfo;

                var message = HashUtil.Encrypt(user.TenDangNhap + "|" + user.MaDonVi + "|" + user.HoVaTen + "|" + user.TenDonVi + "|" + user.EmailCaNhan);
                HashUtil.SetCookies("AccessToken", message);
                return Redirect("/Login/Authenticate/?token=" + HashUtil.Hash(message));
            }
            else
            {
                return RedirectToAction("login");
            }
        }
        public ActionResult Login(int count = 0)
        {
            try
            {
                if (count <= 3)
                {
                    if (UriUtil.RequestId.Equals(ClientSso.ReqStatus.LOGIN_SSO) || UriUtil.RequestId.Equals(ClientSso.ReqStatus.TOKEN_SSO))
                    {
                        if (ClientSso.Ins.CurrentSessionLoginInfo != null)
                        {
                            return RedirectToAction("index");
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
            catch
            {
                return RedirectToAction("login", new { count = count, ReqId = "b26e13b8" });
            }

        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Login(LoginModel request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(request);
                }
                //request.UserName = Server.HtmlEncode(request.UserName);
                //request.Password = Server.HtmlEncode(request.Password);

                var login = ClientSso.Ins.DangNhap(request.UserName, request.Password);
                if (!login.IsError)
                {
                    ClientSso.Ins.XacThucNguoiDung();
                    var user = ClientSso.Ins.CurrentSessionLoginInfo;

                    var message = HashUtil.Encrypt(user.TenDangNhap + "|" + user.MaDonVi + "|" + user.HoVaTen + "|" + user.TenDonVi + "|" + user.EmailCaNhan);
                    HashUtil.SetCookies("AccessToken", message);
                    return Redirect("/Login/Authenticate/?token=" + HashUtil.Hash(message));
                }
                ModelState.AddModelError(string.Empty, "Tài khoản hoặc mật khẩu không đúng");
                return View(request);
            }
            catch
            {
                ModelState.AddModelError(string.Empty, "Tài khoản hoặc mật khẩu không đúng");
                return View(request);
            }

        }
        [HttpGet]
        public ActionResult Logout(string ReturnUrl = "/")
        {
            ClientSso.Ins.XoaChungThuHienHanh();
            return Redirect(ReturnUrl);
        }
    }
}