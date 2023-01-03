using System.Threading.Tasks;
using TechLife.Common;
using TechLife.Data;
using TechLife.Data.Entities;
using TechLife.Model.Order;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System;

namespace TechLife.Service
{
    public interface IOrderService
    {
        Task<ApiResult<bool>> Create(OrderCreateRequest request);
        Task<ApiResult<bool>> Delete(int id);
        Task<PagedResult<OrderVm>> GetAll(string ma, string loai);

    }
    public class OrderService : IOrderService
    {
        private readonly TLDbContext _context;
        public OrderService(TLDbContext context)
        {
            _context = context;
        }
        public async Task<ApiResult<bool>> Create(OrderCreateRequest request)
        {
            var obj = new Order()
            {
                MaThietBi = request.MaThietBi,
                DichVuId = request.DichVuId,
                LoaiDinhVu = request.LoaiDinhVu,
                NhaCungCapId = request.NhaCungCapId,
                SoLuong = request.SoLuong,
                UserName = request.UserName,
                MoTa = request.MoTa,
                Email = request.Email,
                HoVaTen = request.HoVaTen,
                SoDienThoai = request.SoDienThoai,
                NgayDat = Functions.ConvertDateToSql(request.NgayDat)

            };
            await _context.Orders.AddAsync(obj);

            var resultObj = await _context.SaveChangesAsync();
            if (!String.IsNullOrEmpty(request.DichVuId))
            {
                var arr = request.DichVuId.Split(',');
                for (int i = 0; i < arr.Length; i++)
                {
                    var detailObj = new OrderDetail()
                    {
                        DichVuId = Convert.ToInt32(arr[i]),
                        OrderId = obj.Id
                    };
                    await _context.OrderDetails.AddAsync(detailObj);
                }
                resultObj = await _context.SaveChangesAsync();
            }

            if (resultObj > 0)
                return new ApiSuccessResult<bool>(true, "Đặt dịch vụ thành công");
            else return new ApiErrorResult<bool>("Đặt dịch vụ không thành công");
        }

        public async Task<ApiResult<bool>> Delete(int id)
        {
            var obj = await _context.Orders.FindAsync(id);
            if (obj == null) return new ApiErrorResult<bool>("Không tìm thấy đơn hàng bạn muốn xóa");

            var detailObj = _context.OrderDetails.Where(v => v.OrderId == id).ToArray();

            if (detailObj != null)
            {
                _context.OrderDetails.RemoveRange(detailObj);
            }

            _context.Orders.Remove(obj);

            var resultObj = await _context.SaveChangesAsync();
            if (resultObj > 0)
                return new ApiSuccessResult<bool>(true, "Đặt dịch vụ thành công");
            else return new ApiErrorResult<bool>("Đặt dịch vụ không thành công");
        }

        public async Task<PagedResult<OrderVm>> GetAll(string ma, string loai)
        {


            var query = from m in _context.Orders

                        join d in _context.HoSo on m.NhaCungCapId equals d.Id into nhacungcap
                        from n in nhacungcap.DefaultIfEmpty()
                        where (m.MaThietBi == ma || m.UserName == ma)
                        && m.LoaiDinhVu == loai
                        select new { m, n };

            var data = await query.Select(v => new OrderVm()
            {
                Id = v.m.Id,
                NhaCungCap = v.n.Ten,
                LoaiDinhVu = v.m.LoaiDinhVu,
                MoTa = v.m.MoTa,
                NgayDat = v.m.NgayDat,
                SoLuong = v.m.SoLuong,
                DichVuId = v.m.DichVuId,
                DichVu = _context.OrderDetails.Where(x => x.OrderId == v.m.Id).Select(v => new DichVuOrderVm()
                {
                    Id = v.DichVuId,
                    TenDichVu = _context.TienNghi.Where(n => n.Id == v.DichVuId).Select(c => c.Ten).FirstOrDefault()
                }).ToList()
            }).ToListAsync();

            return new PagedResult<OrderVm>()
            {
                TotalRecords = data.Count(),
                PageIndex = 1,
                PageSize = 1,
                Items = data,
            };

        }
    }
}