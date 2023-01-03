using System;
using System.Collections.Generic;

namespace TechLife.Model.Order
{
    public class OrderVm
    {
        public int Id { get; set; }
        public string LoaiDinhVu { get; set; }
        public string DichVuId { get; set; }
        public List<DichVuOrderVm> DichVu { get; set; }
        public string NhaCungCap { get; set; }
        public int SoLuong { get; set; }
        public DateTime NgayDat { get; set; }
        public string MoTa { get; set; }
    }
    public class DichVuOrderVm
    {
        public int Id { get; set; }
        public string TenDichVu { get; set; }
    }
}