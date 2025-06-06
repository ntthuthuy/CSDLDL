using System.Collections.Generic;

namespace TechLife.Data.Entities
{
    public class LoaiHinh
    {
        public int Id { get; set; }
        public string TenLoai { get; set; }
        public string MoTa { get; set; }

        /// <summary>
        /// Enum LinhVucKinhDoanh
        /// </summary>
        public int LinhVucKinhDoanhId { get; set; }
        public bool IsStatus { get; set; }
        public bool IsDelete { get; set; }
        public string NgonNguId { get; set; }

        public List<HoSo> HoSo { get; set; }
    }
}
