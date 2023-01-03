using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechLife.Common;
using TechLife.Data;
using TechLife.Data.Entities;
using TechLife.Data.Extensions;
using TechLife.Model;

namespace TechLife.Service
{
    public interface IRoleService
    {
        Task<List<RoleModel>> GetAll();

        Task<PagedResult<RoleModel>> GetRolesPaging(GetPagingRequest request);

        Task<ApiResult<bool>> Create(RoleModel request);

        Task<ApiResult<bool>> Update(Guid id, RoleModel request);

        Task<ApiResult<RoleModel>> GetById(Guid id);

        Task<ApiResult<bool>> Delete(Guid id);

        Task<ApiResult<bool>> RoleAssign(Guid id, List<RoleModel> request);
        Task<ApiResult<bool>> RoleUnAssign(Guid id, Guid userId);
        Task<List<RoleModel>> GetAll(int groupId);
    }

    public class RoleService : IRoleService
    {
        private readonly RoleManager<Role> _roleManager;
        private readonly UserManager<User> _userManager;
        private readonly TLDbContext _context;

        public RoleService(RoleManager<Role> roleManager
            , UserManager<User> userManager
            , TLDbContext context)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _context = context;
        }

        public async Task<ApiResult<bool>> Create(RoleModel request)
        {
            var role = await _roleManager.FindByNameAsync(request.Name);
            if (role != null)
            {
                return new ApiErrorResult<bool>("Quyền này đã tồn tại");
            }

            role = new Role()
            {
                Name = request.Name,
                Description = request.Description
            };
            var result = await _roleManager.CreateAsync(role);
            if (result.Succeeded)
            {
                return new ApiSuccessResult<bool>();
            }
            return new ApiErrorResult<bool>("Thêm quyền không thành công");
        }

        public async Task<List<RoleModel>> GetAll()
        {
            var roles = await _roleManager.Roles
                .Select(x => new RoleModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description
                }).ToListAsync();

            return roles;
        }
        public async Task<List<RoleModel>> GetAll(int groupId)
        {
            var prams = new Dictionary<string, object>();
            prams.Add("GroupId", groupId);

            var query = "select Roles.* from RoleGroups inner join Roles on RoleGroups.RoleId = Roles.Id " +
                "where RoleGroups.GroupId = @GroupId";

            var roles = await _context.RawQuery<RoleModel>(query, prams);
              

            return roles;
        }
        public async Task<ApiResult<RoleModel>> GetById(Guid id)
        {
            var role = await _roleManager.FindByIdAsync(id.ToString());
            if (role == null)
            {
                return new ApiErrorResult<RoleModel>("Quyền không tồn tại");
            }
            var obj = new RoleModel()
            {
                Name = role.Name,
                Id = role.Id,
                Description = role.Description
            };
            return new ApiSuccessResult<RoleModel>(obj);
        }

        public async Task<PagedResult<RoleModel>> GetRolesPaging(GetPagingRequest request)
        {
            var query = _roleManager.Roles;
            if (!string.IsNullOrEmpty(request.Keyword))
            {
                query = query.Where(x => x.Description.Contains(request.Keyword)
                 || x.Name.Contains(request.Keyword));
            }

            //3. Paging
            int totalRow = await query.CountAsync();

            var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new RoleModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description
                }).ToListAsync();

            //4. Select and projection
            var pagedResult = new PagedResult<RoleModel>()
            {
                TotalRecords = totalRow,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                Items = data
            };
            return pagedResult;
        }

        public async Task<ApiResult<bool>> Update(Guid id, RoleModel request)
        {
            if (await _roleManager.Roles.AnyAsync(x => x.Name == request.Name && x.Id != id))
            {
                return new ApiErrorResult<bool>("Quyền đã tồn tại");
            }
            var role = await _roleManager.FindByIdAsync(id.ToString());
            role.Name = request.Name;
            role.Description = request.Description;

            var result = await _roleManager.UpdateAsync(role);
            if (result.Succeeded)
            {
                return new ApiSuccessResult<bool>();
            }
            return new ApiErrorResult<bool>("Cập nhật không thành công");
        }

        public async Task<ApiResult<bool>> Delete(Guid id)
        {
            var role = await _roleManager.FindByIdAsync(id.ToString());
            var result = await _roleManager.DeleteAsync(role);
            if (result.Succeeded)
            {
                return new ApiSuccessResult<bool>();
            }
            return new ApiErrorResult<bool>("Xóa không thành công");
        }

        public async Task<ApiResult<bool>> RoleAssign(Guid id, List<RoleModel> request)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                return new ApiErrorResult<bool>("Tài khoản không tồn tại");
            }

            var addedRoles = request;
            foreach (var roleName in addedRoles)
            {
                var role = await _roleManager.FindByIdAsync(roleName.Id.ToString());
                if (await _userManager.IsInRoleAsync(user, role.Name) == false)
                {
                    await _userManager.AddToRoleAsync(user, role.Name);
                }
            }

            return new ApiSuccessResult<bool>();
        }

        public async Task<ApiResult<bool>> RoleUnAssign(Guid id, Guid userId)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userId.ToString());
                if (user == null)
                {
                    return new ApiErrorResult<bool>("Tài khoản không tồn tại");
                }

                var role = await _roleManager.FindByIdAsync(id.ToString());
                if (user == null)
                {
                    return new ApiErrorResult<bool>("Quyền không tồn tại");
                }

                var userRoles = await _roleManager.Roles.Where(v => v.Id == id).ToListAsync();
                if (userRoles == null || userRoles.Count() == 0)
                {
                    return new ApiErrorResult<bool>("Quyền không có trong tài khoản");
                }
                var result = await _userManager.RemoveFromRoleAsync(user, role.Name);
                if (result.Succeeded)
                {
                    return new ApiSuccessResult<bool>();
                }
                return new ApiErrorResult<bool>("Xóa không thành công");
            }
            catch (Exception ex)
            {
                return new ApiErrorResult<bool>(ex.Message);
            }
        }
    }
}