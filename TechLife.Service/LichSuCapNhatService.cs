using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TechLife.Common;
using TechLife.Data;
using TechLife.Data.Entities;
using TechLife.Data.Repositories;
using TechLife.Model.DuLieuDuLich;
using TechLife.Model.LichSuCapNhat;

namespace TechLife.Service
{
    public interface ILichSuCapNhatService
    {
        Task<ApiResult<bool>> Create(LichSuCapNhatCreateRequest request);
        Task<ApiResult<List<DuLieuDuLichModel>>> GetAll(int id);
    }

    public class LichSuCapNhatService : BaseRepository, ILichSuCapNhatService
    {
        private readonly TLDbContext _context;

        public LichSuCapNhatService(TLDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<ApiResult<bool>> Create(LichSuCapNhatCreateRequest request)
        {
            string jsonOldValue = JsonConvert.SerializeObject(request.OldValue);
            string jsonNewValue = JsonConvert.SerializeObject(request.NewValue);

            var history = new LichSuCapNhat
            {
                OldValue = jsonOldValue,
                NewValue = jsonNewValue,
                HoSoId = request.HoSoId,
                UpdateByUserId = request.UpdateByUserId.ToString(),
                UpdatedAt = DateTime.Now
            };

            _context.LichSuCapNhat.Add(history);

            var result = await _context.SaveChangesAsync();

            if (result > 0) return new ApiSuccessResult<bool>(true, "Lưu lịch sử thành công!");

            return new ApiSuccessResult<bool>(false, "Lưu lịch sử thất bại!");
        }

        public Task<ApiResult<List<DuLieuDuLichModel>>> GetAll(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}
