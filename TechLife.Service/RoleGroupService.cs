using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechLife.Common;
using TechLife.Data;
using TechLife.Data.Entities;
using TechLife.Model;

namespace TechLife.Service
{
    public interface IRoleGroupService
    {
        Task<List<RoleGroupModel>> GetAll(int groupId);

        Task<ApiResult<bool>> Create(int groupId, List<RoleModel> request);

        Task<ApiResult<bool>> Remove(int groupId);
        Task<ApiResult<bool>> RoleUnAssign(Guid id, int groupId);
    }

    public class RoleGroupService : IRoleGroupService
    {
        private readonly TLDbContext _context;

        public RoleGroupService(TLDbContext context)
        {
            _context = context;
        }

        public async Task<ApiResult<bool>> Create(int groupId, List<RoleModel> request)
        {
            var rolegroup = await _context.RoleGroups.Where(v => v.GroupId == groupId).ToListAsync();
            if (rolegroup == null)
            {
                return new ApiErrorResult<bool>("Nhóm quyền này không tồn tại");
            }
            var listRoleGroup = new List<RoleGroupModel>();
            foreach (var r in request)
            {
                var role = await _context.RoleGroups.Where(v => v.RoleId == r.Id && v.GroupId == groupId).ToListAsync();
                if (role.Count() == 0)
                {
                    _context.RoleGroups.Add(new RoleGroup() { GroupId = groupId, RoleId = r.Id });
                }
            }
            var result = await _context.SaveChangesAsync();
            if (result == 0)
            {
                return new ApiErrorResult<bool>("Đã có lỗi xãy ra trong quá trình xử lý");
            }
            return new ApiSuccessResult<bool>();
        }

        public async Task<List<RoleGroupModel>> GetAll(int groupId)
        {
            var query = _context.RoleGroups.Where(x => x.GroupId == groupId);
            return await query.Select(x => new RoleGroupModel()
            {
                GroupId = x.GroupId,
                RoleId = x.RoleId,
            }).ToListAsync();
        }

        public async Task<ApiResult<bool>> Remove(int groupId)
        {
            var rolegroups = _context.RoleGroups.Where(x => x.GroupId == groupId);
            if (rolegroups == null)
            {
                return new ApiErrorResult<bool>("Không tồn tại");
            }
            foreach (var x in rolegroups)
            {
                _context.RoleGroups.Remove(x);
            }

            await _context.SaveChangesAsync();

            return new ApiSuccessResult<bool>();
        }
        public async Task<ApiResult<bool>> RoleUnAssign(Guid id, int groupId)
        {
            var groups = _context.Groups.Where(x => x.Id == groupId);
            if (groups == null || groups.Count() == 0)
            {
                return new ApiErrorResult<bool>("Vai trò không tồn tại");
            }
            var rolegroups = _context.RoleGroups.Where(x => x.GroupId == groupId && x.RoleId == id);
            if (rolegroups == null || rolegroups.Count() == 0)
            {
                return new ApiErrorResult<bool>("Vai trò không tồn quyền này tại");
            }

            var rolegroup = rolegroups.FirstOrDefault();
            _context.RoleGroups.Remove(rolegroup);

            var result = await _context.SaveChangesAsync();
            if (result == 0)
            {
                return new ApiErrorResult<bool>("Đã có lỗi xãy ra");
            }
            return new ApiSuccessResult<bool>();
        }
    }
}