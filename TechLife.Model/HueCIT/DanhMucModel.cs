using System.Collections.Generic;

namespace TechLife.Model.HueCIT
{
    public class DanhMucModel
    {
        public int Id { get; set; }
        public string Ten { get; set; }
        public int LoaiId { get; set; }
        public string MoTa { get; set; }
        public bool IsStatus { get; set; }
        public bool IsDelete { get; set; }
        public int? DongBoID { get; set; }
        public int? NguonDongBo { get; set; }
    }

    public class DanhMucHoSoDongBo
    {
        public int id { get; set; }
        public int idloaihinh { get; set; }
        public string tenloaihinh { get; set; }
    }

    public class DanhSachDanhMucHoSoDongBo
    {
        public int code { get; set; }
        public string message { get; set; }
        public List<DanhMucHoSoDongBo> data { get; set; }
        public int totalrow { get; set; }
    }
}
