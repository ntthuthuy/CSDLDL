
namespace TechLife.Model.HueCIT
{
    public class LoaiDichVu
    {
        public int Id { get; set; }
        public string TenLoai { get; set; }
        public string MoTa { get; set; }
        public bool IsStatus { get; set; }
        public bool IsDelete { get; set; }
        public int? DongBoID { get; set; }
        public int? NguonDongBo { get; set; }
    }
}
