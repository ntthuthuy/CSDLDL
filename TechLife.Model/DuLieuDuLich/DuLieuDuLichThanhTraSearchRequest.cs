using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TechLife.Model.DuLieuDuLich
{
   public class DuLieuDuLichThanhTraSearchRequest
    {
        [Display(Name = "Từ khóa")]
        public string Keyword { get; set; }
        public string NgayTao { get; set; }
                     

    }
}
