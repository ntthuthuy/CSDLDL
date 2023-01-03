using System;
using System.Collections.Generic;
using System.Text;

namespace TechLife.Model.HueCIT
{
    public class FilterRequest
    {
        public string Keyword { get; set; }
        public int namehdv { get; set; } = -1;
        public int loaithe { get; set; } = -1;
        public string TinhTrang { get; set; } = "";
        public int loaihinh { get; set; } = -1;
        public int hangsao { get; set; } = -1;
        public int huyen { get; set; } = -1;
        public int namecslt { get; set; } = -1;
        public int nameddl { get; set; } = -1;
        public int nameluhanh { get; set; } = -1;
        public int namenhahang { get; set; } = -1;
        public int namecsms { get; set; } = -1;
        public int phucvu { get; set; } = -1;
        public int loaidiadiem { get; set; } = -1;
        public int? dongboId { get; set; } = -1;
        public int? nguondongbo { get; set; } = -1;
    }

    public class FilterRequestPaging
    {
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string Keyword { get; set; }
        public int namehdv { get; set; } = -1;
        public int loaithe { get; set; } = -1;
        public string TinhTrang { get; set; } = "";
        public int loaihinh { get; set; } = -1;
        public int hangsao { get; set; } = -1;
        public int huyen { get; set; } = -1;
        public int namecslt { get; set; } = -1;
        public int nameddl { get; set; } = -1;
        public int nameluhanh { get; set; } = -1;
        public int namenhahang { get; set; } = -1;
        public int namecsms { get; set; } = -1;
        public int phucvu { get; set; } = -1;
        public int loaidiadiem { get; set; } = -1;
        public int? dongboId { get; set; } = -1;
        public int? nguondongbo { get; set; } = -1;
    }
}
