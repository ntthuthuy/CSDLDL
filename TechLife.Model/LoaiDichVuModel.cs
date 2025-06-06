using System.ComponentModel.DataAnnotations;

namespace TechLife.Model
{
    public class LoaiDichVuModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập tên loại dịch vụ")]
        public string TenLoai { get; set; }
        public string MoTa { get; set; }
        public bool IsStatus { get; set; }
        public bool IsDelete { get; set; }
        public string NgonNguId { get; set; }
        // HueCIT
        public int? DongBoID { get; set; }
        public int? NguonDongBo { get; set; }
    }
}
