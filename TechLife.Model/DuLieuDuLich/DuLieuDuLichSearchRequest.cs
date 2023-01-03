using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
namespace TechLife.Model.DuLieuDuLich
{
   public class DuLieuDuLichSearchRequest
    {
        [Display(Name = "Từ khóa")]
        public string Keyword { get; set; }
        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public int TotalRecords { get; set; }

        public int PageCount
        {
            get
            {
                var pageCount = (double)TotalRecords / PageSize;
                return (int)Math.Ceiling(pageCount);
            }
        }

        [Display(Name = "CMND/CCCD")]
        public string CMND { get; set; }
        [Display(Name = "Loại thẻ")]
        public int LoaiTheId { get; set; }
        public List<SelectListItem> LoaiTheItems { get; set; } = new List<SelectListItem>();

        [Display(Name = "Tên ngoại ngữ")]
        public int NgonNguId { get; set; }
        public List<SelectListItem> NgonNguItems { get; set; } = new List<SelectListItem>();
        public string TinhTrang { get; set; }

    }
  
}
