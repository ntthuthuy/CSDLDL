using System.Collections.Generic;

namespace TechLife.Data.Entities
{
    public class TienNghi
    {
        public int Id { get; set; }
        public string Ten { get; set; }
        public string MoTa { get; set; }
        public int ViTri { get; set; }
        public int DonViTinhId { get; set; }
        public string LinhVucId { get; set; }
        public bool IsStatus { get; set; }
        public bool IsDelete { get; set; }
        public string NgonNguId { get; set; }

        public List<TienNghiHoSo> DSTienNghiHoSo { get; set; }
        public virtual DonViTinh DonViTinh { get; set; }
    }
}
