using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TechLife.Model.HoSoThanhTra
{
    public class HoSoThanhTraUpdateRequest
    {
        public string Id { get; set; }
        [Display(Name = "Địa điểm thanh, kiểm tra")]
        public int HoSoId { get; set; }
        [Display(Name = "Trưởng đoàn")]
        public string TruongDoan { get; set; }
        [Display(Name = "Nội dung thanh, kiểm tra")]
        public string NoiDung { get; set; }
        [Display(Name = "Lý do")]
        public string KetLuan { get; set; }
        [Display(Name = "Kết luận thanh tra")]
        public int KetQua { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn thời gian")]
        [DataType(DataType.Date)]
        [Display(Name = "Thời gian")]
        public DateTime ThoiGian { get; set; } = DateTime.Now;
        public int UserId { get; set; }
        public List<VanBanHoSoThanhTraCreateRequest> DSVanBan { get; set; }
        public List<VanBanHoSoThanhTraVm> DSVanBanDaLuu { get; set; }
    }
}
