using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TechLife.Model.HoSoVanBan
{
    public class HoSoVanBanVm
    {
        public int Id { get; set; }
        public int GiayPhepId { get; set; }

        [Display(Name = "Tên gọi")]
        public string TenGoi { get; set; }

        [Display(Name = "Mã số")]
        public string MaSo { get; set; }
        [Display(Name = "Nơi cấp")]
        public string NoiCap { get; set; }
        [Display(Name = "Ngày cấp")]
        [DataType(DataType.Date)]
        public DateTime NgayCap { get; set; }
        [Display(Name = "Ngày hết hạn")]
        [DataType(DataType.Date)]
        public DateTime NgayHetHan { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public bool IsStatus { get; set; }
        public IFormFile Files { get; set; }
    }
}
