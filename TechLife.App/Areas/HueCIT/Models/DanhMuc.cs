using System.ComponentModel.DataAnnotations;

namespace TechLife.App.Areas.HueCIT.Models
{
    public class DanhMuc
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tên danh mục")]
        public string Ten { get; set; }
        public int LoaiId { get; set; }
        public string MoTa { get; set; }
        public bool IsStatus { get; set; }
        public bool IsDelete { get; set; }

    }
}
