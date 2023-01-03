using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TechLife.Model.HueCIT
{
    //HoSo
    public class DongBoDieuHanhAdd
    {
        public ToaDo geometry { get; set; }
        public HoSoDongBoAdd attributes { get; set; }
    }

    public class DongBoDieuHanhEdit
    {
        public ToaDo geometry { get; set; }
        public HoSoDongBoEdit attributes { get; set; }
    }

    public class ToaDo
    {
        public double? x { get; set; }
        public double? y { get; set; }
        public SpatialReference spatialReference { get; set; }
    }

    public class SpatialReference
    {
        public int wkid { get; set; }
    }

    public class HoSoDongBoAdd
    {
        public int id { get; set; }
        public string ten { get; set; }
        public int linhvuckin { get; set; }
        public int loaihinhid { get; set; }
        public string sonha { get; set; }
        public string duongpho { get; set; }
        public string sodienthoa { get; set; }
    }

    public class HoSoDongBoEdit
    {
        public int objectid { get; set; }
        public int id { get; set; }
        public string ten { get; set; }
        public int linhvuckin { get; set; }
        public int loaihinhid { get; set; }
        public string sonha { get; set; }
        public string duongpho { get; set; }
        public string sodienthoa { get; set; }
    }

    public class CheckResponse
    { 
        public string objectIdFieldName { get; set; }
        public List<int> objectIds { get; set; }
    }

    //VeSinh
    public class DongBoDieuHanhDiemVeSinhAdd
    {
        public ToaDo geometry { get; set; }
        public DiemVeSinhDongBoAdd attributes { get; set; }
    }

    public class DongBoDieuHanhDiemVeSinhEdit
    {
        public ToaDo geometry { get; set; }
        public DiemVeSinhDongBoEdit attributes { get; set; }
    }
    public class DiemVeSinhDongBoAdd
    {
        public int id { get; set; }
        public string ten { get; set; }
        public string vitri { get; set; }
        public string hientrang { get; set; }
        public string mota { get; set; }
    }

    public class DiemVeSinhDongBoEdit
    {
        public int objectid { get; set; }
        public int id { get; set; }
        public string ten { get; set; }
        public string vitri { get; set; }
        public string hientrang { get; set; }
        public string mota { get; set; }
    }

    //DiemGiaoDich
    public class DongBoDieuHanhDiemGiaoDichAdd
    {
        public ToaDo geometry { get; set; }
        public DiemGiaoDichDongBoAdd attributes { get; set; }
    }

    public class DongBoDieuHanhDiemGiaoDichEdit
    {
        public ToaDo geometry { get; set; }
        public DiemGiaoDichDongBoEdit attributes { get; set; }
    }
    public class DiemGiaoDichDongBoAdd
    {
        public int id { get; set; }
        public string tendiadiem { get; set; }
        public string diachi { get; set; }
        public string dienthoai { get; set; }
        public string giophucvu { get; set; }
    }

    public class DiemGiaoDichDongBoEdit
    {
        public int objectid { get; set; }
        public int id { get; set; }
        public string tendiadiem { get; set; }
        public string diachi { get; set; }
        public string dienthoai { get; set; }
        public string giophucvu { get; set; }
    }

    //LeHoi
    public class DongBoDieuHanhLeHoiAdd
    {
        public ToaDo geometry { get; set; }
        public LeHoiDongBoAdd attributes { get; set; }
    }

    public class DongBoDieuHanhLeHoiEdit
    {
        public ToaDo geometry { get; set; }
        public LeHoiDongBoEdit attributes { get; set; }
    }
    public class LeHoiDongBoAdd
    {
        public string id { get; set; }
        public string tenlehoi { get; set; }
        public string diadiem { get; set; }
        public string noidung { get; set; }
    }

    public class LeHoiDongBoEdit
    {
        public int objectid { get; set; }
        public string id { get; set; }
        public string tenlehoi { get; set; }
        public string diadiem { get; set; }
        public string noidung { get; set; }
    }

    //PhanAnh
    public class DongBoDieuHanhPhanAnhAdd
    {
        public ToaDo geometry { get; set; }
        public PhanAnhDongBoAdd attributes { get; set; }
    }

    public class DongBoDieuHanhPhanAnhEdit
    {
        public ToaDo geometry { get; set; }
        public PhanAnhDongBoEdit attributes { get; set; }
    }
    public class PhanAnhDongBoAdd
    {
        public int id { get; set; }
        public string tieude { get; set; }
        public string diachisuki { get; set; }
    }

    public class PhanAnhDongBoEdit
    {
        public int objectid { get; set; }
        public int id { get; set; }
        public string tieude { get; set; }
        public string diachisuki { get; set; }
    }
}
