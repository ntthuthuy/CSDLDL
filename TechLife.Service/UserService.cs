using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TechLife.Common;
using TechLife.Common.Enums;
using TechLife.Data.Entities;
using TechLife.Model;
using TechLife.Model.HSCV;
using TechLife.Model.User;

namespace TechLife.Service
{
    public interface IUserService
    {
        Task<ApiResult<string>> Login(LoginModel request);

        Task<ApiResult<string>> Authencate(string userName);

        Task<ApiResult<bool>> Register(UserRegiterRequest request);

        Task<ApiResult<bool>> Create(UserModel request);

        Task<ApiResult<bool>> CreateSSO(List<TaiKhoanVm> request);

        Task<ApiResult<bool>> Update(Guid id, UserUpdateRequest request);

        Task<ApiResult<bool>> ChangePass(Guid id, UserChangePassRequest request);

        Task<ApiResult<PagedResult<UserModel>>> GetUsersPaging(GetPagingRequest request);

        Task<ApiResult<UserModel>> GetById(Guid id);

        Task<ApiResult<UserModel>> GetByUserName(string username);

        Task<ApiResult<bool>> Delete(Guid id);

        Task<ApiResult<bool>> UploadAvata(Guid id, string url);
        Task Logout();
        Task<ApiResult<string>> RegiterSocial(UserSocialRequest request);
        Task<bool> IsContantRoles(Guid userId, IEnumerable<string> roles);
        Task<bool> IsInRole(Guid userId, string role);

        Task<bool> IsIsInRoles(Guid userId, IEnumerable<string> roles);
        Task<ApiResult<string>> AuthencateByCitizen(string citizenId, string idToken = "", string avartarUrl = "");
    }

    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly IRoleGroupService _roleGroupService;
        private readonly IConfiguration _config;

        public UserService(UserManager<User> userManager,
           SignInManager<User> signInManager,
           RoleManager<Role> roleManager,
           IConfiguration config,
           IRoleGroupService roleGroupService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _config = config;
            _roleGroupService = roleGroupService;
        }

        public async Task<ApiResult<string>> Authencate(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null) return new ApiErrorResult<string>("Tài khoản không tồn tại");

            await _signInManager.SignInAsync(user, true);

            //var roles = await _userManager.GetRolesAsync(user);

            var claims = new List<Claim>();
            claims.Add(new Claim("Id", user.Id.ToString()));
            claims.Add(new Claim(ClaimTypes.Name, user.UserName));
            claims.Add(new Claim("FullName", user.FullName));
            claims.Add(new Claim("LoginType", LoginType.SSO.ToString()));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Tokens:Issuer"],
                _config["Tokens:Issuer"],
                claims,
                expires: DateTime.Now.AddHours(3),
                signingCredentials: creds);

            return new ApiSuccessResult<string>(new JwtSecurityTokenHandler().WriteToken(token));
        }
        public async Task<ApiResult<string>> AuthencateByCitizen(string citizenId, string idToken = "", string avartarUrl = "")
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.CanCuocCongDan == citizenId && (x.LockoutEnd == null || x.LockoutEnd <= DateTimeOffset.UtcNow));

            if (user == null) return new ApiErrorResult<string>("Tài khoản không tồn tại");

            await _signInManager.SignInAsync(user, true);

            //var roles = await _userManager.GetRolesAsync(user);

            var claims = new List<Claim>();
            claims.Add(new Claim("Id", user.Id.ToString()));
            claims.Add(new Claim(ClaimTypes.Name, user.UserName));
            claims.Add(new Claim("FullName", user.FullName));
            claims.Add(new Claim("IdToken", !String.IsNullOrWhiteSpace(idToken) ? idToken : ""));
            claims.Add(new Claim("AvartarUrl", !String.IsNullOrWhiteSpace(avartarUrl) ? avartarUrl : ""));
            claims.Add(new Claim("LoginType", LoginType.SSOHueS.ToString()));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Tokens:Issuer"],
                _config["Tokens:Issuer"],
                claims,
                expires: DateTime.Now.AddHours(3),
                signingCredentials: creds);

            return new ApiSuccessResult<string>(new JwtSecurityTokenHandler().WriteToken(token));
        }
        public async Task<ApiResult<string>> Login(LoginModel request)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user == null) return new ApiErrorResult<string>("Tài khoản hoặc mật khẩu không đúng!");

            var result = await _signInManager.PasswordSignInAsync(user, request.Password, request.RememberMe, true);
            if (!result.Succeeded)
            {
                return new ApiErrorResult<string>("Tài khoản hoặc mật khẩu không đúng!");
            }

            //var roles = await _userManager.GetRolesAsync(user);

            var claims = new List<Claim>();
            claims.Add(new Claim("Id", user.Id.ToString()));
            claims.Add(new Claim(ClaimTypes.Name, user.UserName));
            claims.Add(new Claim("FullName", user.FullName));
            claims.Add(new Claim("LoginType", LoginType.Default.ToString()));


            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Tokens:Issuer"],
                _config["Tokens:Issuer"],
                claims,
                expires: DateTime.Now.AddHours(3),
                signingCredentials: creds);

            return new ApiSuccessResult<string>(new JwtSecurityTokenHandler().WriteToken(token));
        }

        public async Task<ApiResult<bool>> Create(UserModel request)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user != null)
            {
                return new ApiErrorResult<bool>("Tài khoản đã tồn tại");
            }
            if (await _userManager.FindByEmailAsync(request.Email) != null)
            {
                return new ApiErrorResult<bool>("Emai đã tồn tại");
            }
            if (request.Password != request.ConfirmPassword)
            {
                return new ApiErrorResult<bool>("Mật khẩu không khớp");
            }
            user = new User()
            {
                Dob = request.Dob,
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserName = request.UserName,
                FullName = request.FullName,
                PhoneNumber = request.PhoneNumber,
                GroupId = request.GroupId,
                TypeId = request.TypeId,
                CanCuocCongDan = request.CanCuocCongDan?.Trim(),
                AvataUrl = request.AvataUrl
            };
            var result = await _userManager.CreateAsync(user, request.Password);
            if (result.Succeeded)
            {
                var roleGroups = _roleGroupService.GetAll(request.GroupId);
                if (roleGroups != null && roleGroups.Result.Count() > 0)
                {
                    var roles = new List<string>();

                    foreach (var r in roleGroups.Result)
                    {
                        var role = await _roleManager.FindByIdAsync(r.RoleId.ToString());
                        roles.Add(role.Name);
                    }
                    result = await _userManager.AddToRolesAsync(user, roles);
                    if (!result.Succeeded)
                    {
                        return new ApiErrorResult<bool>("Thêm tài không thành công, nhưng chưa cấp quyền");
                    }
                }
                return new ApiSuccessResult<bool>(result.Succeeded, "Thêm tài khoản thành công!");
            }
            return new ApiErrorResult<bool>("Thêm tài khoản không thành công");
        }

        public async Task<ApiResult<bool>> CreateSSO(List<TaiKhoanVm> request)
        {
            var users = await _userManager.Users.ToListAsync();
            foreach (var u in request)
            {
                var user = await _userManager.FindByNameAsync(u.UserName);
                if (user == null)
                {
                    user = new User()
                    {
                        Dob = u.DOB != null ? Convert.ToDateTime(u.DOB) : DateTime.MinValue,
                        Email = u.Email,
                        FirstName = u.FirstName,
                        LastName = u.LastName,
                        UserName = u.UserName,
                        FullName = u.FullName,
                        PhoneNumber = u.OfficePhone,
                        GroupId = 0,
                        TypeId = 2,
                        AvataUrl = "",

                    };
                    var result = await _userManager.CreateAsync(user, "Abcd1234@");
                    if (result.Succeeded)
                    {
                        var roleGroups = _roleGroupService.GetAll(4);
                        if (roleGroups != null && roleGroups.Result.Count() > 0)
                        {
                            var roles = new List<string>();

                            foreach (var r in roleGroups.Result)
                            {
                                var role = await _roleManager.FindByIdAsync(r.RoleId.ToString());
                                roles.Add(role.Name);
                            }
                            result = await _userManager.AddToRolesAsync(user, roles);
                        }
                    }
                }
            }
            return new ApiSuccessResult<bool>(true, "Thêm danh sách tài khoản thành công");
        }

        public async Task<ApiResult<bool>> Delete(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                return new ApiErrorResult<bool>("User không tồn tại");
            }

            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
                return new ApiSuccessResult<bool>(result.Succeeded, "Xóa thành công!");

            return new ApiErrorResult<bool>("Xóa không thành công");
        }

        public async Task<ApiResult<UserModel>> GetById(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                return new ApiErrorResult<UserModel>("User không tồn tại");
            }
            var roles = await _userManager.GetRolesAsync(user);
            var userVm = new UserModel()
            {
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                FirstName = user.FirstName,
                Dob = user.Dob,
                Id = user.Id,
                LastName = user.LastName,
                UserName = user.UserName,
                Roles = roles,
                AvataUrl = user.AvataUrl,
                TypeId = user.TypeId,
                CanCuocCongDan = user.CanCuocCongDan
            };
            return new ApiSuccessResult<UserModel>(userVm);
        }

        public async Task<ApiResult<UserModel>> GetByUserName(string usermame)
        {
            var user = await _userManager.FindByNameAsync(usermame);
            if (user == null)
            {
                return new ApiErrorResult<UserModel>("User không tồn tại");
            }
            var roles = await _userManager.GetRolesAsync(user);
            var userVm = new UserModel()
            {
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                FirstName = user.FirstName,
                Dob = user.Dob,
                Id = user.Id,
                LastName = user.LastName,
                UserName = user.UserName,
                AvataUrl = user.AvataUrl,
                Roles = roles,
                TypeId = user.TypeId
            };
            return new ApiSuccessResult<UserModel>(userVm);
        }

        public async Task<ApiResult<PagedResult<UserModel>>> GetUsersPaging(GetPagingRequest request)
        {
            var query = _userManager.Users;
            if (!string.IsNullOrEmpty(request.Keyword))
            {
                query = query.Where(x => x.UserName.Contains(request.Keyword)
                 || x.PhoneNumber.Contains(request.Keyword));
            }

            //3. Paging
            int totalRow = await query.CountAsync();

            var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new UserModel()
                {
                    Email = x.Email,
                    PhoneNumber = x.PhoneNumber,
                    UserName = x.UserName,
                    FirstName = x.FirstName,
                    FullName = x.FullName,
                    Id = x.Id,
                    LastName = x.LastName,
                    TypeId = x.TypeId
                }).ToListAsync();

            //4. Select and projection
            var pagedResult = new PagedResult<UserModel>()
            {
                TotalRecords = totalRow,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                Items = data
            };
            return new ApiSuccessResult<PagedResult<UserModel>>(pagedResult);
        }

        public async Task<ApiResult<bool>> Register(UserRegiterRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user != null)
            {
                return new ApiErrorResult<bool>("Tài khoản đã tồn tại");
            }
            if (await _userManager.FindByEmailAsync(request.Email) != null)
            {
                return new ApiErrorResult<bool>("Emai đã tồn tại");
            }
            if (request.ConfirmPassword != request.Password)
            {
                return new ApiErrorResult<bool>("Mật khẩu không trùng khớp");
            }

            user = new User()
            {
                Dob = Functions.ConvertDateToSql(request.Dob),
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserName = request.UserName,
                PhoneNumber = request.PhoneNumber,
                FullName = request.FirstName + " " + request.LastName,
                GroupId = 3
            };
            var result = await _userManager.CreateAsync(user, request.Password);
            if (result.Succeeded)
            {
                return new ApiSuccessResult<bool>(result.Succeeded, "Đăng ký tài khoản thành công!");
            }
            else if (result.Errors.Count() > 0)
            {
                Errors[] error = result.Errors.Select(v => new Errors { Code = v.Code, Description = v.Description }).ToArray();
                return new ApiErrorResult<bool>(error, "Đăng ký không thành công");
            }
            else
            {
                return new ApiErrorResult<bool>("Đăng ký không thành công");
            }
        }

        public async Task<ApiResult<bool>> Update(Guid id, UserUpdateRequest request)
        {
            if (await _userManager.Users.AnyAsync(x => x.Email == request.Email && x.Id != id))
            {
                return new ApiErrorResult<bool>("Emai đã tồn tại");
            }
            var user = await _userManager.FindByIdAsync(id.ToString());

            if (user == null) return new ApiErrorResult<bool>("Tài khoản không tồn tại");

            user.Dob = request.Dob;
            user.Email = request.Email;
            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.PhoneNumber = request.PhoneNumber;
            user.FullName = request.FirstName + " " + request.LastName;
            user.CanCuocCongDan = request.CanCuocCongDan?.Trim();

            if (!string.IsNullOrEmpty(request.Password))
            {
                if (request.Password != request.ConfirmPassword)
                {
                    return new ApiErrorResult<bool>("Mật khẩu và mật khẩu xác nhận không trùng khớp");
                }

                string passwordPattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$";

                Regex passwordRegex = new(passwordPattern);

                if (!passwordRegex.IsMatch(request.Password))
                {
                    return new ApiErrorResult<bool>("Mật khẩu tối thiểu 8 ký tự gồm ít nhất 1 chữ hoa, 1 chữ thường, 1 số và 1 ký tự đặc biệt");
                }

                var success = await _userManager.RemovePasswordAsync(user);

                if (success.Succeeded)
                {
                    success = await _userManager.AddPasswordAsync(user, request.Password);

                    if (!success.Succeeded)
                    {
                        return new ApiErrorResult<bool>("Cập nhật không thành công");
                    }
                }
                else
                {
                    return new ApiErrorResult<bool>("Cập nhật không thành công");
                }
            }

            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                return new ApiSuccessResult<bool>(result.Succeeded, "Cập nhật thành công!");
            }
            return new ApiErrorResult<bool>("Cập nhật không thành công");
        }

        public async Task<ApiResult<bool>> ChangePass(Guid id, UserChangePassRequest request)
        {
            if (request.ConfirmPassword != request.Password)
            {
                return new ApiErrorResult<bool>("Mật khẩu không trùng khớp");
            }

            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                return new ApiErrorResult<bool>("Tài khoản không tồn tại");
            }
            var result = await _userManager.ChangePasswordAsync(user, request.CurentPassword, request.Password);
            if (result.Succeeded)
            {
                return new ApiSuccessResult<bool>(result.Succeeded, "Đổi mật khẩu thành công!");
            }
            return new ApiErrorResult<bool>("Đã có lỗi xãy ra");
        }

        public async Task<ApiResult<bool>> UploadAvata(Guid id, string url)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());

            user.AvataUrl = url;

            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return new ApiSuccessResult<bool>(result.Succeeded, "Cập nhật thành công!");
            }
            return new ApiErrorResult<bool>("Cập nhật không thành công");
        }

        public async Task<ApiResult<string>> RegiterSocial(UserSocialRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);

            if (user == null)
            {
                user = new User()
                {
                    UserName = request.UserName,
                    LastName = request.LastName,
                    FullName = request.FullName,
                    FirstName = request.FirstName,
                    TypeId = (int)LoaiTaiKhoan.MangXaHoi
                };
                var result = await _userManager.CreateAsync(user);

                if (result.Succeeded)
                {

                    var claims = new List<Claim>();
                    claims.Add(new Claim(ClaimTypes.Name, user.UserName));
                    claims.Add(new Claim(ClaimTypes.Sid, user.Id.ToString()));
                    claims.Add(new Claim(ClaimTypes.GivenName, user.FullName));

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));
                    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                    var token = new JwtSecurityToken(_config["Tokens:Issuer"],
                        _config["Tokens:Issuer"],
                        claims,
                        expires: DateTime.Now.AddHours(3),
                        signingCredentials: creds);

                    return new ApiSuccessResult<string>(new JwtSecurityTokenHandler().WriteToken(token));
                }
                return new ApiErrorResult<string>("Thêm mới tài khoản không thành công");
            }
            else
            {
                user.UserName = request.UserName;
                user.LastName = request.LastName;
                user.FullName = request.FullName;
                user.FirstName = request.FirstName;
                user.TypeId = (int)LoaiTaiKhoan.MangXaHoi;
                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {

                    var claims = new List<Claim>();
                    claims.Add(new Claim(ClaimTypes.Name, user.UserName));
                    claims.Add(new Claim(ClaimTypes.Sid, user.Id.ToString()));
                    claims.Add(new Claim(ClaimTypes.GivenName, user.FullName));


                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));
                    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                    var token = new JwtSecurityToken(_config["Tokens:Issuer"],
                        _config["Tokens:Issuer"],
                        claims,
                        expires: DateTime.Now.AddHours(3),
                        signingCredentials: creds);

                    return new ApiSuccessResult<string>(new JwtSecurityTokenHandler().WriteToken(token));
                }
                return new ApiErrorResult<string>("Cập nhật không thành công");
            }

        }
        public async Task<bool> IsContantRoles(Guid userId, IEnumerable<string> roles)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userId.ToString());

                if (user == null) return false;

                var rolesInUser = await _userManager.GetRolesAsync(user);

                if (rolesInUser == null || rolesInUser.Count == 0) return false;

                var query = from ri in rolesInUser
                            join r in roles on ri equals r
                            select ri;

                return query.Any();
            }
            catch
            {
                throw;
            }
        }
        public async Task<bool> IsInRole(Guid userId, string role)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userId.ToString());

                if (user == null) return false;

                return await _userManager.IsInRoleAsync(user, role);
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> IsIsInRoles(Guid userId, IEnumerable<string> roles)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userId.ToString());
                if (user == null)
                    return false;
                if (await _userManager.IsInRoleAsync(user, "root"))
                    return true;

                var rolesInUser = await _userManager.GetRolesAsync(user);
                if (rolesInUser == null || rolesInUser.Count == 0) return false;
                var query = from ri in rolesInUser
                            join r in roles on ri equals r
                            select ri;

                return query.Any();
            }
            catch
            {
                throw;
            }
        }
        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
        }
    }
}