using System;
using System.Collections.Generic;

namespace TechLife.Common
{
    public class PagingRequestBase
    {
        public int PageIndex { get; set; } = SystemConstants.pageIndex;

        public int PageSize { get; set; } = SystemConstants.pageSize;
    }

    public class PagedResultBase
    {
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
    }
    public class GetPagingRequest : PagingRequestBase
    {
        public string Keyword { get; set; }
        public int? NguonDongBo { get; set; }
        public int namehdv { get; set; } = -1;
        public int loaithe { get; set; } = -1;
        public string TinhTrang { get; set; } = "";
    }
    public class TinTucPagingRequest : PagingRequestBase
    {
        public int ChuyenMucId { get; set; }
        public string Keyword { get; set; }
        public string Loai { get; set; }
        public string TuNgay { get; set; }
        public string DenNgay { get; set; }
    }
    public class ThanhTraPagingRequest : PagingRequestBase
    {
        public int HoSoId { get; set; }
        public int KetLuanId { get; set; }
        public string Keyword { get; set; }

    }
    public class HoSoFromRequets : GetPagingRequest
    {
        public int loaihinh { get; set; } = -1;
        public int hangsao { get; set; } = -1;
        public int huyen { get; set; } = -1;
        public int namecslt { get; set; } = -1;
        public int nameddl { get; set; } = -1;
        public int nameluhanh { get; set; } = -1;
        public int namenhahang { get; set; } = -1;
        public int namecsms { get; set; } = -1;
        public int nguon { get; set; } = -1;
    }
    public class RptFromRequets : GetPagingRequest
    {
        public string diaban { get; set; }
        public string loaihinh { get; set; }
        public string hangsao { get; set; }
        public string tiennghi { get; set; }
        public double giamin { get; set; } = -1;
        public double giamax { get; set; } = -1;
        public int diemdanhgia { get; set; } = -1;
        public int datchuan { get; set; } = -1;
        public int luotxem { get; set; } = 0;
        public string diemnoibat { get; set; } = "";
        public string diaphuong { get; set; } = "";
        public int diemmoinhat { get; set; } = 0;
    }
    public class UserFromRequets : GetPagingRequest
    {
        public int type { get; set; } = 0;
    }
    public class TourRequets : GetPagingRequest
    {
        public int luhanh { get; set; } = -1;
        public string loaihinh { get; set; }
        public string gia { get; set; }
        public string tungay { get; set; }
        public string denngay { get; set; }
    }
    public class PagedResult<T> : PagedResultBase
    {
        public object index;

        public List<T> Items { set; get; }
    }

    public class DanhMucDuLieuThongKeFormRequets : PagingRequestBase
    {
        public string Search { get; set; }
    }

    public class HoatDongKinhDoanhFormRequest : PagingRequestBase
    {
        public string Search { get; set; }
        public int Thang { get; set; }
    }
}
