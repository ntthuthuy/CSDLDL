using System;
namespace TechLife.Data.Entities
{
    public class ThietBi
    {
        public int Id { get; set; }
        public string MaThietBi { get; set; }
        public DateTime NgayCaiDat { get; set; }
        public DateTime NgaySuDungMoiNhat { get; set; }
        public string UserName { get; set; }
        public string LoaiThietBi { get; set; }
        public bool IsStatus { get; set; }
    }
}