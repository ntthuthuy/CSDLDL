using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TechLife.Model.DuLieuDuLich
{
   public class DuLieuDuLichLuHanhSearchRequest
    {
        [Display(Name = "Từ khóa")]
        public string Keyword { get; set; }

        [Display(Name = "Loại công ty")]
        public int LoaiHinhId  { get; set; }
        
    }

}
