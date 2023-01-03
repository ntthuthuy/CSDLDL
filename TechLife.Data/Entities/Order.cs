using System;
namespace TechLife.Data.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public string MaThietBi { get; set; }
        public string UserName { get; set; }
        public string HoVaTen { get; set; }
        public string Email { get; set; }
        public string SoDienThoai { get; set; }
        public string LoaiDinhVu { get; set; }

        public DateTime NgayTao { get; set; }
        public DateTime NgayDat { get; set; }
        public string DichVuId { get; set; }
        public int NhaCungCapId { get; set; }
        public int SoLuong { get; set; }
        public string MoTa { get; set; }
        public bool IsStatus { get; set; }
        public bool IsDelete { get; set; }

    }
}