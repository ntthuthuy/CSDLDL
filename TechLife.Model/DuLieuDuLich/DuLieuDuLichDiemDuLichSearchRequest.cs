using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TechLife.Model.DuLieuDuLich
{
   public class DuLieuDuLichDiemDuLichSearchRequest
    {
        [Display(Name = "Từ khóa")]
        public string Keyword { get; set; }

        [Display(Name = "Loại hình")]
        public int LoaiHinhId { get; set; }
     
    }
}
