using System;
namespace TechLife.Model.ChuyenDi
{
    public class ChuyenDiUpdateRequest
    {
        public int Id { get; set; }
        public string TenChuyenDi { get; set; }
        public DateTime? NgayTao { get; set; }
        public string MaThietBi { get; set; }
        public string UserName { get; set; }
        public string MoTa { get; set; }
        public decimal Gia { get; set; }
        public int SoNgay { get; set; }
        public int SoNguoi { get; set; }
    }
}