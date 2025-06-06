using System.ComponentModel.DataAnnotations;

namespace TechLife.Model
{
    public class TrinhDoModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập tên trình độ")]
        public string TenTrinhDo { get; set; }
        public string MoTa { get; set; }
        public bool IsStatus { get; set; }
        public bool IsDelete { get; set; }
        public string NgonNguId { get; set; }
    }
}
