using System;
using System.Collections.Generic;
using System.Text;

namespace TechLife.Model.TinTuc
{
    public class TinTucVm
    {
        public int Id { get; set; }
        public int ChuyenMucId { get; set; }
        public List<ChuyenMucVm> DSChuyenMucKhac { get; set; }
        public int HoSoId { get; set; }
        public string NgonNguId { get; set; }
        public string TieuDe { get; set; }
        public string URL { get; set; }
        public string AnhDaiDien { get; set; }
        public string MoTaAnh { get; set; }
        public string MoTa { get; set; }
        public string TuKhoa { get; set; }
        public string NoiDung { get; set; }
        public string NguonTin { get; set; }
        public string TacGia { get; set; }
        public string TacQuyen { get; set; }
        public string Tag { get; set; }
        public string NguonNgonNguId { get; set; }
        public bool IsTinBaiChiaSe { get; set; }
        public bool IsTinTieuDiem { get; set; }
        public bool IsTinKhuyenMai { get; set; }
        public bool IsTinLeHoi { get; set; }
        public bool IsStatus { get; set; }
        public DateTime? NgayCongBo { get; set; }
        public DateTime? NgayKetThuc { get; set; }
        public DateTime NgayTao { get; set; }

        public DateTime? NgayDienRa { get; set; }
        public int ThuTuHienThi { get; set; }
        public int TrangThai { get; set; }
        public int NguoiDangId { get; set; }
        public int NguoiDuyetId { get; set; }
        public int TongDanhGia { get; set; }
        public int LuotXem { get; set; }
    }
}
