using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TechLife.Model.DuLieuDuLich
{
   public class DuLieuDuLichCSLTSearchRequest
    {
        [Display(Name = "Từ khóa")]
        public string Keyword { get; set; }
        
        [Display(Name = "")]
        public int LoaiHinhId { get; set; }
        public List<SelectListItem> LoaiHinhItems { get; set; } = new List<SelectListItem>();
        public int HangSao { get; set; }


    }
}
