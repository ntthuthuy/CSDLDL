using System.ComponentModel.DataAnnotations;

namespace TechLife.Model
{
    public class NgoaiNguModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập tên ngoại ngữ")]
        public string TenNgoaiNgu { get; set; }
        public string MoTa { get; set; }
        public bool IsStatus { get; set; }
        public bool IsDelete { get; set; }
        public string NgonNguId { get; set; }
    }
}
