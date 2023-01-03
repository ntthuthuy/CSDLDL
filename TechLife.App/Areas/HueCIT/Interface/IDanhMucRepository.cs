using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechLife.App.Areas.HueCIT.Models;

namespace TechLife.App.Areas.HueCIT.Interface
{
    public interface IDanhMucRepository
    {
        #region LOẠI ĐỊA ĐIỂM ĂN UỐNG
        Task<IEnumerable<LoaiDiaDiemAnUong>> GetsLoaiDiaDiemAnUong();
        Task<LoaiDiaDiemAnUong> UpdateLoaiDiaDiemAnUongDongBoID(int id, int? dongboId);
        Task<LoaiDiaDiemAnUong> GetLoaiDiaDiemAnUong(int id);
        Task<LoaiDiaDiemAnUong> GetLoaiDiaDiemAnUongDongBo(int? dongboId);
        Task<LoaiDiaDiemAnUong> InsertLoaiDiaDiemAnUong(LoaiDiaDiemAnUong data);
        Task<LoaiDiaDiemAnUong> UpdateLoaiDiaDiemAnUong(LoaiDiaDiemAnUong data);
        Task<int> DeleteLoaiDiaDiemAnUong(int id);
        #endregion

        #region LOẠI ẨM THỰC ĐỊA ĐIỂM ĂN UỐNG
        Task<IEnumerable<LoaiAmThucDiaDiemAnUong>> GetsLoaiAmThucDiaDiemAnUong();
        Task<LoaiAmThucDiaDiemAnUong> GetLoaiAmThucDiaDiemAnUong(int id);
        Task<LoaiAmThucDiaDiemAnUong> InsertLoaiAmThucDiaDiemAnUong(LoaiAmThucDiaDiemAnUong data);
        Task<LoaiAmThucDiaDiemAnUong> UpdateLoaiAmThucDiaDiemAnUong(LoaiAmThucDiaDiemAnUong data);
        Task<int> DeleteLoaiAmThucDiaDiemAnUong(int id);
        #endregion

        #region LOẠI ẨM THỰC
        Task<IEnumerable<LoaiAmThuc>> GetsLoaiAmThuc();
        Task<LoaiAmThuc> GetLoaiAmThuc(int id);
        Task<LoaiAmThuc> GetByDongBoID(int? dongBoID);
        Task<LoaiAmThuc> GetByTenLoai(string tenloai);
        Task<LoaiAmThuc> InsertLoaiAmThuc(LoaiAmThuc data);
        Task<LoaiAmThuc> UpdateLoaiAmThuc(LoaiAmThuc data);
        Task<LoaiAmThuc> EditDongBoID(int id, int? dongboId);
        Task<int> DeleteLoaiAmThuc(int id);
        #endregion

        #region CHỦ ĐỀ SỰ KIỆN
        Task<IEnumerable<ChuDeSuKien>> GetsChuDeSuKien();
        Task<ChuDeSuKien> GetChuDeSuKien(int id);
        Task<ChuDeSuKien> InsertChuDeSuKien(ChuDeSuKien data);
        Task<ChuDeSuKien> UpdateChuDeSuKien(ChuDeSuKien data);
        Task<int> DeleteChuDeSuKien(int id);
        #endregion
        
        #region DANH MỤC
        Task<IEnumerable<DanhMuc>> GetsDanhMuc(int id);
        Task<DanhMuc> GetDanhMuc(int id);
        Task<DanhMuc> InsertDanhMuc(DanhMuc data);
        Task<DanhMuc> UpdateDanhMuc(DanhMuc data);
        Task<int> DeleteDanhMuc(int id);
        #endregion

        #region LOẠI LỄ HỘI
        Task<IEnumerable<LoaiLeHoiTrinhDien>> GetsLoaiLeHoi();
        Task<LoaiLeHoiTrinhDien> GetLoaiLeHoi(int id);
        Task<LoaiLeHoiTrinhDien> GetLoaiLeHoiByDongBo(int? id);
        Task<LoaiLeHoiModel> AddLoaiLeHoi(LoaiLeHoiModel model);
        Task<LoaiLeHoiModel> EditLoaiLeHoi(LoaiLeHoiModel model);
        Task<int> DeleteLoaiLeHoi(int id);
        #endregion

        #region LOẠI ĐIỂM GIAO DỊCH
        Task<IEnumerable<LoaiDiemGiaoDichTrinhDien>> GetsLoaiDiemGiaoDich();
        Task<LoaiDiemGiaoDichTrinhDien> GetLoaiDiemGiaoDich(int id);
        Task<LoaiDiemGiaoDichTrinhDien> GetLoaiDiemGiaoDichByDongBo(int? id);
        Task<LoaiDiemGiaoDich> AddLoaiDiemGiaoDich(LoaiDiemGiaoDich data);
        Task<LoaiDiemGiaoDich> EditLoaiDiemGiaoDich(LoaiDiemGiaoDich data);
        Task<int> DeleteLoaiDiemGiaoDich(int id);
        #endregion

    }
}
