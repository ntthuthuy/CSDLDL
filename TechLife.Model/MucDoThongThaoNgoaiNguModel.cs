using System.ComponentModel.DataAnnotations;

namespace TechLife.Model
{
    public class MucDoThongThaoNgoaiNguModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập mức độ thành thạo ngoại ngữ")]
        public string MucDo { get; set; }
        public string MoTa { get; set; }
        public bool IsStatus { get; set; }
        public bool IsDelete { get; set; }
        public string NgonNguId { get; set; }
    }
}
