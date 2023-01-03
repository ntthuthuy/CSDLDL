using System.ComponentModel.DataAnnotations;

namespace TechLife.App.Areas.HueCIT.Models
{
    public class LoaiAmThucDiaDiemAnUong
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập tên loại địa điểm ăn uống")]
        public string TenLoaiAmThuc { get; set; }
        public string MoTa { get; set; }
        public bool IsStatus { get; set; }
        public bool IsDelete { get; set; }
    }
}
