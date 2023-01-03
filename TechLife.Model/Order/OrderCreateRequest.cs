using System;
namespace TechLife.Model.Order
{
    public class OrderCreateRequest
    {
        public string MaThietBi { get; set; }
        public string UserName { get; set; }
        public string HoVaTen { get; set; }
        public string Email { get; set; }
        public string SoDienThoai { get; set; }
        public string LoaiDinhVu { get; set; }
        public string DichVuId { get; set; }
        public int NhaCungCapId { get; set; }
        public int SoLuong { get; set; }
        public string MoTa { get; set; }
        public string NgayDat { get; set; }
    }
}