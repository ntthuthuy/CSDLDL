using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TechLife.Model.DuLieuDuLich
{
   public class DuLieuDuLichCoSoMuaSamSearchRequest
    {
        [Display(Name = "Từ khóa")]
        public string Keyword { get; set; }

        [Display(Name = "Loại hình")]
        public int LoaiHinhId { get; set; }
    }
}
