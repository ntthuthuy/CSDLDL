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
    public interface IGroupService
    {
        Task<List<GroupModel>> GetAll();

        Task<ApiResult<bool>> Create(GroupModel request);

        Task<ApiResult<bool>> Update(int id, GroupModel request);

        Task<int> Delete(int id);
        Task<ApiResult<GroupModel>> GetById(int id);
    }
    public class GroupService : IGroupService
    {
        private readonly TLDbContext _context;
        public GroupService(TLDbContext context)
        {
            _context = context;
        }
        public async Task<ApiResult<bool>> Create(GroupModel request)
        {
            var group = new Group()
            {
                Name = request.Name,
                Description = request.Description
            };
            _context.Groups.Add(group);

            var result = await _context.SaveChangesAsync();
            if (result > 0)
                return new ApiSuccessResult<bool>(true, "Thêm nhóm quyền thành công");

            else return new ApiErrorResult<bool>("Thêm quyền không thành công");

        }

        public async Task<int> Delete(int id)
        {
            var group = await _context.Groups.FindAsync(id);
            if (group == null) throw new TLException($"Cannot find a group: {id}");

            _context.Groups.Remove(group);

            return await _context.SaveChangesAsync();
        }

        public async Task<List<GroupModel>> GetAll()
        {
            var query = _context.Groups;
            return await query.Select(x => new GroupModel()
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description
            }).ToListAsync();
        }

        public async Task<ApiResult<GroupModel>> GetById(int id)
        {
            var group = await _context.Groups.FirstOrDefaultAsync(x => x.Id == id);
            if (group == null)
            {
                return new ApiErrorResult<GroupModel>("Vai trò không tồn tại");
            }
            var rolegroups = _context.RoleGroups.Where(v => v.GroupId == id);
            List<Guid> roles = new List<Guid>();
            foreach (var r in rolegroups)
            {
                roles.Add(r.RoleId);
            }
            var groupVm = new GroupModel()
            {
                Name = group.Name,
                Id = group.Id,
                Description = group.Description,
                Roles = roles
            };
            return new ApiSuccessResult<GroupModel>(groupVm);
        }

        public async Task<ApiResult<bool>> Update(int id, GroupModel request)
        {
            var group = await _context.Groups.FindAsync(id);
            if (group == null)
            {
                return new ApiErrorResult<bool>("Không tìm thấy group tương ứng");
            }

            group.Name = request.Name;
            group.Description = request.Description;

            _context.Groups.Update(group);

            var result = await _context.SaveChangesAsync();
            if (result > 0)
                return new ApiSuccessResult<bool>(true, "Cập nhật nhóm quyền thành công");

            else return new ApiErrorResult<bool>("Cập nhật nhóm quyền không thành công");
        }
    }
}