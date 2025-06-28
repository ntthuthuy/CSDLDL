using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechLife.App.ApiClients;
using TechLife.Common;
using TechLife.Common.Extension;
using TechLife.Model;
using TechLife.Model.User;
using TechLife.Service;

namespace TechLife.App.Controllers
{
    [Authorize(Roles = "root")]
    public class UserController : BaseController
    {
        private readonly IRoleApiClient _roleApiClient;
        private readonly IGroupApiClient _groupApiClient;
        private readonly IRoleGroupApiClient _roleGroupApiClient;
        private readonly IHscvApiClient _hscvApiClient;
        private readonly IUserService _userService;
        private readonly IGroupService _groupService;
        private readonly IRoleService _roleService;
        private readonly IPhongBanService _phongBanService;

        public UserController(IUserService userService, IConfiguration configuration
            , IRoleApiClient roleApiClient
            , IGroupApiClient groupApiClient
            , IRoleGroupApiClient roleGroupApiClient
            , IHscvApiClient hscvApiClient
            , IGroupService groupService
            , IRoleService roleService
            , IPhongBanService phongBanService
            , ITrackingService trackingService)
           : base(userService, configuration, trackingService)
        {
            _roleApiClient = roleApiClient;
            _groupApiClient = groupApiClient;
            _roleGroupApiClient = roleGroupApiClient;
            _hscvApiClient = hscvApiClient;
            _userService = userService;
            _groupService = groupService;
            _roleService = roleService;
            _phongBanService = phongBanService;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Logout(string ReturnUrl)
        {
            var user = User.GetUser();

            await _userService.Logout();

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Session.Remove("Token");
            HttpContext.Session.Remove(SystemConstants.AppSettings.UserInfo);

            if (user.LoginType == Common.Enums.LoginType.SSO)
                return Redirect("/sso");
            else if (user.LoginType == Common.Enums.LoginType.SSOHueS)
                return Redirect($"https://sso.huecity.vn/auth/realms/hues/protocol/openid-connect/logout?id_token_hint={user.IdToken}&post_logout_redirect_uri={Request.GetAppUrl()}");
            else
                return Redirect("/");
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var request = new UserFromRequets()
            {
                Keyword = !String.IsNullOrEmpty(Request.Query["search"]) ? Request.Query["search"].ToString() : "",
                PageIndex = !String.IsNullOrEmpty(Request.Query["page"]) ? Convert.ToInt32(Request.Query["page"]) : SystemConstants.pageIndex,
                PageSize = !String.IsNullOrEmpty(Request.Query["page_size"]) ? Convert.ToInt32(Request.Query["page_size"]) : SystemConstants.pageSize,
            };
            var data = await _userService.GetUsersPaging(request);
            ViewBag.Keyword = request.Keyword;
            if (TempData["result"] != null)
            {
                ViewBag.SuccessMsg = TempData["result"];
            }

            await OptionLoaiTaiKhoan();

            return View(data.ResultObj);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            //ViewBag.Group = await _groupApiClient.GetAll();

            ViewBag.Group = await _groupService.GetAll();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserModel request)
        {
            request.FullName = request.LastName + " " + request.FirstName;
            var result = await _userService.Create(request);
            if (result.IsSuccessed)
            {
                TempData["result"] = "Thêm mới người dùng thành công";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", result.Message);
            return View(request);
        }

        public async Task<IActionResult> Role()
        {
            var request = new GetPagingRequest()
            {
                Keyword = !String.IsNullOrEmpty(Request.Query["search"]) ? Request.Query["search"].ToString() : "",
                PageIndex = !String.IsNullOrEmpty(Request.Query["page"]) ? Convert.ToInt32(Request.Query["page"]) : SystemConstants.pageIndex,
                PageSize = !String.IsNullOrEmpty(Request.Query["page_size"]) ? Convert.ToInt32(Request.Query["page_size"]) : SystemConstants.pageSize,
            };
            var data = await _roleService.GetRolesPaging(request);
            return View(data);
        }

        [HttpGet]
        public IActionResult CreateRole()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(RoleModel request, string btn_submit)
        {
            if (!ModelState.IsValid)
                return View();

            var result = await _roleService.Create(request);
            if (result.IsSuccessed)
            {
                TempData["result"] = "Thêm quyền thành công";
                switch (btn_submit)
                {
                    case "Luu":
                        return RedirectToAction("Role");

                    default: return RedirectToAction("CreateRole");
                }
            }

            ModelState.AddModelError("", result.Message);
            return View(request);
        }

        [HttpGet]
        public async Task<IActionResult> EditRole(string id)
        {
            var result = await _roleService.GetById(Guid.Parse(id));

            if (result.IsSuccessed)
            {
                var role = result.ResultObj;
                var updateRequest = new RoleModel()
                {
                    Name = role.Name,
                    Description = role.Description,
                    Id = role.Id
                };
                return View(role);
            }

            return RedirectToAction("Error", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> EditRole(RoleModel request)
        {
            if (!ModelState.IsValid)
                return View();

            var result = await _roleService.Update(request.Id, request);
            if (result.IsSuccessed)
            {
                TempData["result"] = "Cập nhật quyền thành công";
                return RedirectToAction("Role");
            }

            ModelState.AddModelError("", result.Message);

            return View(request);
        }

        [HttpGet]
        public async Task<IActionResult> RoleAssign(Guid id, int groupId = 0)
        {
            var roleAssignRequest = await GetRoleAssignRequest(id, groupId);

            return View(roleAssignRequest);
        }

        [HttpPost]
        public async Task<IActionResult> RoleUnAssign(Guid id, Guid userId)
        {
            var result = await _roleService.RoleUnAssign(id, userId);
            if (result.IsSuccessed)
            {
                return RedirectToAction("RoleAssign", new { id = userId });
            }
            ModelState.AddModelError("", result.Message);
            return View();
        }

        private async Task<RoleAssignRequest> GetRoleAssignRequest(Guid id, int groupId = 0)
        {
            var groups = await _groupService.GetAll();

            var userObj = await _userService.GetById(id);
            var roles = await _roleService.GetAll(groupId);
            var roleAll = await _roleService.GetAll();

            var roleAssignRequest = new RoleAssignRequest();
            roleAssignRequest.Id = id;
            roleAssignRequest.GroupId = groupId;
            var userRoles = roleAll.Where(v => userObj.ResultObj.Roles.Contains(v.Name.ToString())).Select(v => new RoleModel()
            {
                Id = v.Id,
                Name = v.Name,
                Description = v.Description
            }).ToList();

            roleAssignRequest.UserRoles = userRoles;

            roleAssignRequest.GroupRoles = groups != null ? groups.Select(s => new SelectListItem()
            {
                Value = s.Id.ToString(),
                Text = s.Name,
                Selected = userObj.ResultObj.Roles.Contains(s.Name)
            }).ToList() : new List<SelectListItem>();

            foreach (var role in roles)
            {
                roleAssignRequest.Roles.Add(new SelectListItem()
                {
                    Value = role.Id.ToString(),
                    Text = role.Description,
                    Selected = true
                });
            }
            return roleAssignRequest;
        }

        [HttpPost]
        public async Task<IActionResult> RoleAssign(IFormCollection fc)
        {
            if (!ModelState.IsValid)
                return View();

            Guid Id = Guid.Parse(fc["id"]);
            var request = new List<RoleModel>();
            string IdRoles = fc["roles"];
            if (!String.IsNullOrEmpty(IdRoles))
            {
                var arrIdRoles = IdRoles.Split(",");
                for (int i = 0; i < arrIdRoles.Length; i++)
                {
                    request.Add(new RoleModel() { Id = Guid.Parse(arrIdRoles[i]) });
                }
            }

            var result = await _roleService.RoleAssign(Id, request);
            if (result.IsSuccessed)
            {
                TempData["result"] = "Cập nhật quyền thành công";
                return RedirectToAction("RoleAssign", new { id = Id });
            }

            ModelState.AddModelError("", result.Message);

            return View(request);
        }

        [HttpGet]
        public async Task<IActionResult> Group()
        {
            var data = await _groupApiClient.GetAll();
            return View(data);
        }

        [HttpGet]
        public IActionResult CreateGroup()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateGroup(GroupModel request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }

            var result = await _groupService.Create(request);
            TempData.AddAlert(new Result<string>() { IsSuccessed = result.IsSuccessed, Message = result.Message });

            if (result.IsSuccessed)
            {
                return RedirectToAction("Group");
            }

            ModelState.AddModelError("", result.Message);

            return View(request);
        }

        [HttpGet]
        public async Task<IActionResult> AssignRoleGroup(int id)
        {
            var groupRoleAssignRequest = await GetGroupRoleAssignRequest(id);
            return View(groupRoleAssignRequest);
        }

        private async Task<RoleAssignRequest> GetGroupRoleAssignRequest(int id)
        {
            var groupObj = await _groupApiClient.GetById(id);
            var roleObj = await _roleApiClient.GetAll();
            var roleAssignRequest = new RoleAssignRequest();
            roleAssignRequest.Id = id;
            foreach (var role in roleObj.ResultObj)
            {
                roleAssignRequest.Roles.Add(new SelectListItem()
                {
                    Value = role.Id.ToString(),
                    Text = role.Description,
                    Selected = groupObj.ResultObj.Roles.Contains(role.Id)
                });
            }
            return roleAssignRequest;
        }

        [HttpPost]
        public async Task<IActionResult> AssignRoleGroup(IFormCollection fc)
        {
            int Id = Int32.Parse(fc["id"]);
            var request = new List<RoleModel>();
            string IdRoles = fc["roles"];
            if (!String.IsNullOrEmpty(IdRoles))
            {
                var arrIdRoles = IdRoles.Split(",");
                for (int i = 0; i < arrIdRoles.Length; i++)
                {
                    request.Add(new RoleModel() { Id = Guid.Parse(arrIdRoles[i]) });
                }
            }

            var result = await _roleGroupApiClient.RoleGroupAssign(Id, request);
            if (result.IsSuccessed)
            {
                TempData["result"] = "Cập nhật quyền thành công";
                return RedirectToAction("AssignRoleGroup", new { id = Id });
            }

            ModelState.AddModelError("", result.Message);

            return View(request);
        }

        [HttpPost]
        public async Task<IActionResult> RoleGroupUnAssign(Guid id, int groupId)
        {
            var result = await _roleGroupApiClient.RoleGroupUnAssign(id, groupId);
            if (result.IsSuccessed)
            {
                return RedirectToAction("AssignRoleGroup", new { id = groupId });
            }
            ModelState.AddModelError("", result.Message);
            return View();
        }

        public async Task<IActionResult> Phongban()
        {
            ViewData["Title"] = "Phòng ban, trung tâm";
            ViewData["Title_parent"] = "Hệ thống";

            var request = new GetPagingRequest()
            {
                Keyword = !String.IsNullOrEmpty(Request.Query["search"]) ? Request.Query["search"].ToString() : "",
                PageIndex = !String.IsNullOrEmpty(Request.Query["page"]) ? Convert.ToInt32(Request.Query["page"]) : SystemConstants.pageIndex,
                PageSize = !String.IsNullOrEmpty(Request.Query["page_size"]) ? Convert.ToInt32(Request.Query["page_size"]) : SystemConstants.pageSize,
            };

            var data = await _phongBanService.GetPaging(request);
            //var data = await _phongBanService.DSPhongBan(SystemConstants.AppSettings.UniqueCode);

            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> Dongbo_phongban(int service_sso)
        {
            var resultApi = await _hscvApiClient.DSPhongBan(SystemConstants.AppSettings.UniqueCode);

            var result = await _phongBanService.CreateSso(resultApi);

            TempData.AddAlert(new Result<string>() { IsSuccessed = result.IsSuccessed, Message = result.Message });

            return Redirect(Request.GetBackUrl());
        }

        [HttpPost]
        public async Task<IActionResult> Dongbo(int service_sso)
        {
            var resultApi = await _hscvApiClient.DSTaiKhoan(SystemConstants.AppSettings.UniqueCode);

            var result = await _userService.CreateSSO(resultApi);

            TempData.AddAlert(new Result<string>() { IsSuccessed = result.IsSuccessed, Message = result.Message });

            return Redirect(Request.GetBackUrl());
        }

        [HttpGet]
        public async Task<IActionResult> EditGroup(int id)
        {
            var result = await _groupApiClient.GetById(id);

            if (result.IsSuccessed)
            {
                var role = result.ResultObj;
                var updateRequest = new GroupModel()
                {
                    Name = role.Name,
                    Description = role.Description,
                    Id = role.Id
                };
                return View(role);
            }

            return RedirectToAction("Error", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> EditGroup(GroupModel request)
        {
            if (!ModelState.IsValid)
                return View();

            var result = await _groupService.Update(request.Id, request);

            TempData.AddAlert(new Result<string>() { IsSuccessed = result.IsSuccessed, Message = result.Message });

            if (result.IsSuccessed)
            {
                return RedirectToAction("Group");
            }

            ModelState.AddModelError("", result.Message);

            return View(request);
        }

        [HttpGet]
        public async Task<IActionResult> Tracking()
        {
            ViewData["Title"] = "Truy vết sử dụng";
            ViewData["Title_parent"] = "Hệ thống";

            var request = new GetPagingRequest()
            {
                Keyword = !String.IsNullOrEmpty(Request.Query["search"]) ? Request.Query["search"].ToString() : "",
                PageIndex = !String.IsNullOrEmpty(Request.Query["page"]) ? Convert.ToInt32(Request.Query["page"]) : SystemConstants.pageIndex,
                PageSize = !String.IsNullOrEmpty(Request.Query["page_size"]) ? Convert.ToInt32(Request.Query["page_size"]) : SystemConstants.pageSize,
            };

            var data = await _trackingService.GetPaging(request);
            //var data = await _phongBanService.DSPhongBan(SystemConstants.AppSettings.UniqueCode);

            return View(data);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var data = await _userService.GetById(Guid.Parse(id));

            //ViewBag.Group = await _groupService.GetAll();

            var user = data.ResultObj;

            var model = new UserUpdateRequest
            {
                Id = user.Id.ToString(),
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Dob = user.Dob,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                CanCuocCongDan = user.CanCuocCongDan
            };

            return View(model);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        //[Consumes("multipart/form-data")]
        public async Task<IActionResult> Edit(UserUpdateRequest request)
        {
            if (!ModelState.IsValid) return View();
            var result = await _userService.Update(Guid.Parse(request.Id), request);

            TempData.AddAlert(new Result<string>() { IsSuccessed = result.IsSuccessed, Message = result.Message });

            return Redirect(Request.GetBackUrl());
        }
    }
}