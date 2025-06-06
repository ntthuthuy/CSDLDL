using System.Collections.Generic;

namespace TechLife.Data.Entities
{
    public class NgoaiNgu
    {
        public int Id { get; set; }
        public string TenNgoaiNgu { get; set; }
        public string MoTa { get; set; }
        public bool IsStatus { get; set; }
        public bool IsDelete { get; set; }
        public string NgonNguId { get; set; }
        public List<NgoaiNguHoSo> DSNgoaiNguHoSo { get; set; }
    }
}
