using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TechLife.Model.DuLieuDuLich
{
    public class DuLieuDuLichNhaHangSearchRequest
    {
        [Display(Name = "Từ khóa")]
        public string Keyword { get; set; }       

        [Display(Name = "Ten dịch vụ")]
        public int LoaiId { get; set; }


    }
}
