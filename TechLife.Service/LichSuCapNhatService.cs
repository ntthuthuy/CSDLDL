using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechLife.Common;
using TechLife.Data;
using TechLife.Data.Entities;
using TechLife.Model;
using TechLife.Model.DuLieuDuLich;
using TechLife.Model.LichSuCapNhat;

namespace TechLife.Service
{
    public interface ILichSuCapNhatService
    {
        Task<ApiResult<bool>> Create(LichSuCapNhatCreateRequest request);
        Task<ApiResult<List<LichSuCapNhatModel>>> GetByHoSoId(int id);
    }

    public class LichSuCapNhatService : ILichSuCapNhatService
    {
        private readonly TLDbContext _context;

        public LichSuCapNhatService(TLDbContext context)
        {
            _context = context;
        }

        public async Task<ApiResult<bool>> Create(LichSuCapNhatCreateRequest request)
        {
            try
            {
                string jsonOldValue = JsonConvert.SerializeObject(request.OldValue,
                    Formatting.Indented,
                    new JsonSerializerSettings()
                    {
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                    });

                string jsonNewValue = JsonConvert.SerializeObject(request.NewValue,
                    Formatting.Indented,
                    new JsonSerializerSettings()
                    {
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                    });

                var history = new LichSuCapNhat
                {
                    OldValue = jsonOldValue,
                    NewValue = jsonNewValue,
                    HoSoId = request.HoSoId,
                    UpdateByUserId = request.UpdateByUserId,
                    UpdatedAt = DateTime.Now,
                    IsDelete = false
                };

                _context.LichSuCapNhat.Add(history);

                var result = await _context.SaveChangesAsync();

                if (result > 0) return new ApiSuccessResult<bool>(true, "Lưu lịch sử thành công!");

                return new ApiSuccessResult<bool>(false, "Lưu lịch sử thất bại!");
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<ApiResult<List<LichSuCapNhatModel>>> GetByHoSoId(int id)
        {
            try
            {
                var data = await _context.LichSuCapNhat.Where(x => !x.IsDelete && x.HoSoId == id).ToListAsync();
                var user = await _context.Users.Where(x => !x.IsDelete).Select(x => new UserModel
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    FullName = x.FullName,
                    CanCuocCongDan = x.CanCuocCongDan
                }).ToListAsync();

                List<LichSuCapNhatModel> result = new();

                foreach (var item in data)
                {
                    var oldValue = JsonConvert.DeserializeObject<DuLieuDuLichModel>(item.OldValue);
                    var newValue = JsonConvert.DeserializeObject<DuLieuDuLichModel>(item.NewValue);

                    result.Add(new LichSuCapNhatModel
                    {
                        Id = item.Id,
                        OldValue = oldValue,
                        NewValue = newValue,
                        UpdatedAt = item.UpdatedAt,
                        UpdateByUser = user.FirstOrDefault(x => x.Id == item.UpdateByUserId),
                        HoSoId = item.HoSoId,
                    });
                }
                return new ApiResult<List<LichSuCapNhatModel>> { IsSuccessed = true, Message = $"Xem lịch sử cập nhật hồ sơ {id}", ResultObj = result };
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
