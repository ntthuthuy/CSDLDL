using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using TechLife.Common;
using TechLife.Common.Enums;
using TechLife.Data;
using TechLife.Data.Entities;
using TechLife.Model;
using TechLife.Model.HoSoVanBan;
using TechLife.Model.HuongDanVien;
using TechLife.Service.Common;

namespace TechLife.Service
{
    public interface IHuongDanVienService
    {
        Task<List<HuongDanVienModel>> GetAll(string key = "");

        Task<PagedResult<HuongDanVienModel>> GetPaging(GetPagingRequest request);

        Task<HuongDanVienModel> GetById(int id);

        Task<ApiResult<HuongDanVienModel>> Create(HuongDanVienModel request);

        Task<ApiResult<int>> Update(int id, HuongDanVienModel request);

        Task<ApiResult<int>> Delete(int id);

        Task<ApiResult<bool>> UploadImage(int id, ImageUploadRequest request);

        Task<ApiResult<bool>> ImportHuongDanVien(List<HuongDanVienImport> request);
    }

    public class HuongDanVienService : IHuongDanVienService
    {
        private readonly TLDbContext _context;
        private readonly IQuaTrinhHoatDongService _quaTrinhHoatDongService;
        private readonly IFileUploadService _fileUploadService;

        private readonly IStorageService _storageService;
        private const string USER_CONTENT_FOLDER_NAME = "user-content";

        public HuongDanVienService(TLDbContext context,
            IQuaTrinhHoatDongService quaTrinhHoatDongService
            , IStorageService storageService
            , IFileUploadService fileUploadService)
        {
            _context = context;
            _quaTrinhHoatDongService = quaTrinhHoatDongService;
            _storageService = storageService;
            _fileUploadService = fileUploadService;
        }

        public async Task<ApiResult<HuongDanVienModel>> Create(HuongDanVienModel request)
        {
            var huongDanVien = new HuongDanVien()
            {
                IsDelete = request.IsDelete,
                IsStatus = request.IsStatus,
                HoVaTen = request.HoVaTen,
                CMND = request.CMND,
                DiaChi = request.DiaChi,
                Email = request.Email,
                GioiTinh = request.GioiTinh,
                HoKhau = request.HoKhau,
                LoaiTheId = request.LoaiTheId,
                NgayCapCMND = request.NgayCapCMND,
                NgayCapThe = request.NgayCapThe,
                NgayHetHan = request.NgayHetHan,
                NoiCapCMND = request.NoiCapCMND,
                SoDienThoai = request.SoDienThoai,
                SoTheHDV = request.SoTheHDV,
                CongTyLuHanhId = request.CongTyLuHanhId,
                NoiCapThe = request.NoiCapThe

            };
            _context.HuongDanVien.Add(huongDanVien);
            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                request.Id = huongDanVien.Id;
                if (request.DSQuaTrinhHD != null & request.DSQuaTrinhHD.Count() > 0)
                {
                    foreach (var h in request.DSQuaTrinhHD)
                    {
                        _context.QuaTrinhHoatDong.Add(new QuaTrinhHoatDong()
                        {
                            HDVId = request.Id,
                            HoatDong = h.HoatDong,
                            KetQua = h.KetQua,
                            ThoiGian = h.ThoiGian
                        });
                    }
                }
                if (request.LoaiHinhId != null && request.LoaiHinhId.Count > 0)
                {
                    foreach (var x in request.LoaiHinhId)
                    {
                        _context.HuongDanVienLoaiHinh.Add(new HuongDanVienLoaiHinh()
                        {
                            HuongDanVienId = huongDanVien.Id,
                            LoaiHinhId = x
                        });
                    }
                }
                if (request.NgonNguId != null && request.NgonNguId.Count > 0)
                {
                    foreach (var x in request.NgonNguId)
                    {
                        _context.HuongDanVienNgonNgu.Add(new HuongDanVienNgonNgu()
                        {
                            HuongDanVienId = huongDanVien.Id,
                            NgonNguId = x
                        });
                    }
                }
                if (request.DSVanBan != null && request.DSVanBan.Count() > 0)
                {
                    foreach (var d in request.DSVanBan)
                    {
                        _context.HoSoVanBan.Add(new HoSoVanBan()
                        {
                            NoiCap = d.NoiCap,
                            TenGoi = d.TenGoi,
                            HosoId = request.Id,
                            NgayCap = DateTime.Now,
                            NgayHetHan = DateTime.Now,
                            MaSo = "",
                            FilePath = d.FilePath,
                            FileName = d.FileName,
                            GiayPhepId = d.GiayPhepId,
                            IsStatus = d.IsStatus,
                            Loai = LoaiFile.hosohuongdanvien.ToString()
                        });
                    }
                }

                await _context.SaveChangesAsync();

                return new ApiSuccessResult<HuongDanVienModel>(request, "Thêm hướng dẫn viên thành công");
            }
            return new ApiErrorResult<HuongDanVienModel>("Thêm lỗi!");
        }

        public async Task<ApiResult<int>> Delete(int id)
        {
            var huongDanVien = await _context.HuongDanVien.Where(x => x.Id == id).ToListAsync();
            if (huongDanVien == null || huongDanVien.Count() <= 0)
            {
                return new ApiErrorResult<int>("Không tìm dữ liệu cần xóa!");
            }

            var obj = huongDanVien.FirstOrDefault();
            obj.IsDelete = true;

            _context.HuongDanVien.Update(obj);

            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                return new ApiSuccessResult<int>(id, "Xóa hướng dẫn viên " + obj.HoVaTen + " thành công");
            }
            return new ApiErrorResult<int>("Xóa lỗi!");
        }

        public async Task<List<HuongDanVienModel>> GetAll(string key = "")
        {
            var query = from m in _context.HuongDanVien
                        where m.IsDelete == false 
                        && (!String.IsNullOrEmpty(key) ? m.HoVaTen.Contains(key)|| m.SoTheHDV.Contains(key) : 1 == 1)
                        select new HuongDanVienModel
                        {
                            Id = m.Id,
                            HoVaTen = m.HoVaTen,
                            GioiTinh = m.GioiTinh,
                            SoTheHDV = m.SoTheHDV,
                            SoDienThoai = m.SoDienThoai,
                            CMND = m.CMND,
                            DiaChi = m.DiaChi,
                            NoiCapCMND = m.NoiCapCMND,
                            NgayHetHan = m.NgayHetHan,
                            Email = m.Email,
                            HoKhau = m.HoKhau,
                            LoaiTheId = m.LoaiTheId,
                            NgayCapCMND = m.NgayCapCMND,
                            NgayCapThe = m.NgayCapThe,
                            IsDelete = m.IsDelete,
                            IsStatus = m.IsStatus,
                            DSQuaTrinhHD = m.DSQuaTrinhHoatDong.Select(v => new QuaTrinhHoatDongModel()
                            {
                                HDVId = v.HDVId,
                                HoatDong = v.HoatDong,
                                Id = v.Id,
                                KetQua = v.KetQua,
                                ThoiGian = v.ThoiGian
                            }).ToList(),
                            Images = _fileUploadService.GetImageByHoSoId(m.Id, LoaiFile.hosohuongdanvien.ToString()).Result
                        };

            return await query.ToListAsync();
        }

        public async Task<HuongDanVienModel> GetById(int id)
        {
            //var huongDanVien = await _context.HuongDanVien.Where(x => x.Id == id).ToListAsync();

            var query = from m in _context.HuongDanVien
                        where m.IsDelete == false && m.Id == id
                        select new HuongDanVienModel()
                        {
                            Id = m.Id,
                            HoVaTen = m.HoVaTen,
                            GioiTinh = m.GioiTinh,
                            NgaySinh = m.NgaySinh,
                            SoTheHDV = m.SoTheHDV,
                            SoDienThoai = m.SoDienThoai,
                            CMND = m.CMND,
                            DiaChi = m.DiaChi,
                            NoiCapCMND = m.NoiCapCMND,
                            NgayHetHan = m.NgayHetHan,
                            Email = m.Email,
                            HoKhau = m.HoKhau,
                            LoaiTheId = m.LoaiTheId,
                            NgayCapCMND = m.NgayCapCMND,
                            NgayCapThe = m.NgayCapThe,
                            IsDelete = m.IsDelete,
                            IsStatus = m.IsStatus,
                            DSQuaTrinhHD = m.DSQuaTrinhHoatDong.Select(v => new QuaTrinhHoatDongModel()
                            {
                                HDVId = v.HDVId,
                                HoatDong = v.HoatDong,
                                Id = v.Id,
                                KetQua = v.KetQua,
                                ThoiGian = v.ThoiGian
                            }).ToList(),
                            DSVanBan = _context.HoSoVanBan.Where(v => v.HosoId == m.Id && v.Loai == LoaiFile.hosohuongdanvien.ToString()).Select(x => new HoSoVanBanVm()
                            {
                                FileName = x.FileName,
                                FilePath = x.FilePath,
                                Id = x.Id,
                                MaSo = x.MaSo,
                                NgayCap = x.NgayCap,
                                NgayHetHan = x.NgayHetHan,
                                NoiCap = x.NoiCap,
                                TenGoi = x.TenGoi,
                                GiayPhepId = x.GiayPhepId,
                                IsStatus = x.IsStatus
                            }).ToList(),
                            NoiCapThe = m.NoiCapThe,
                            LoaiHinhId = _context.HuongDanVienLoaiHinh.Where(v => v.HuongDanVienId == m.Id).Select(v => v.LoaiHinhId).ToList(),
                            NgonNguId = _context.HuongDanVienNgonNgu.Where(v => v.HuongDanVienId == m.Id).Select(v => v.NgonNguId).ToList(),
                            Images = _fileUploadService.GetImageByHoSoId(m.Id, LoaiFile.hosohuongdanvien.ToString()).Result
                        };

            if (query == null || query.Count() <= 0)
            {
                throw new TLException($"Cannot find with id {id}");
            }

            var model = await query.FirstOrDefaultAsync();

            return model;
        }

        public async Task<PagedResult<HuongDanVienModel>> GetPaging(GetPagingRequest request)
        {
            var query = from m in _context.HuongDanVien
                        where (request.namehdv == -1 || m.Id == request.namehdv) && m.IsDelete == false && (request.loaithe == -1 || m.LoaiTheId == request.loaithe ) 
                              || (request.TinhTrang == "" || request.TinhTrang == "Thẻ hết hạn" || request.TinhTrang == "Thẻ còn hạn")
                        select new { m };

            if (!string.IsNullOrEmpty(request.Keyword))
                query = query.Where(x => x.m.HoVaTen.Contains(request.Keyword));

            if (request.TinhTrang == "Thẻ hết hạn")
            {
                query = query.Where(V => V.m.NgayHetHan <= DateTime.Now);
            }
            else if (request.TinhTrang == "Thẻ còn hạn")
            {
                query = query.Where(V => V.m.NgayHetHan > DateTime.Now);
            }
            else
            {
                request.TinhTrang = "Chưa cấp thẻ";
            }

            //3. Paging
            int totalRow = query.Count();

            var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new HuongDanVienModel()
                {
                    Id = x.m.Id,
                    HoVaTen = x.m.HoVaTen,
                    GioiTinh = x.m.GioiTinh,
                    SoTheHDV = x.m.SoTheHDV,
                    SoDienThoai = x.m.SoDienThoai,
                    CMND = x.m.CMND,
                    DiaChi = x.m.DiaChi,
                    NoiCapCMND = x.m.NoiCapCMND,
                    NgayHetHan = x.m.NgayHetHan,
                    Email = x.m.Email,
                    HoKhau = x.m.HoKhau,
                    LoaiTheId = x.m.LoaiTheId,
                    LoaiThe = x.m.LoaiTheId == 1 ? "Thẻ nội địa" : "Thẻ quốc tế",
                    NgayCapCMND = x.m.NgayCapCMND,
                    NgayCapThe = x.m.NgayCapThe,
                    IsDelete = x.m.IsDelete,
                    IsStatus = x.m.IsStatus,
                    NoiCapThe = x.m.NoiCapThe,
                    LoaiHinhId = _context.HuongDanVienLoaiHinh.Where(v => v.HuongDanVienId == x.m.Id).Select(v => v.LoaiHinhId).ToList(),
                    NgonNguId = _context.HuongDanVienNgonNgu.Where(v => v.HuongDanVienId == x.m.Id).Select(v => v.HuongDanVienId).ToList(),
                    Images = _fileUploadService.GetImageByHoSoId(x.m.Id, LoaiFile.hosohuongdanvien.ToString()).Result
                }).ToListAsync();

            //4. Select and projection
            var pagedResult = new PagedResult<HuongDanVienModel>()
            {
                TotalRecords = totalRow,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                Items = data
                
            };
            return pagedResult;
        }

        public async Task<ApiResult<int>> Update(int id, HuongDanVienModel request)
        {
            var HuongDanVien = await _context.HuongDanVien.Where(x => x.Id == id).ToListAsync();
            if (HuongDanVien == null || HuongDanVien.Count() <= 0)
            {
                return new ApiErrorResult<int>("Không tìm thấy dữ liệu!");
            }

            var model = HuongDanVien.FirstOrDefault();

            model.HoVaTen = request.HoVaTen;
            model.GioiTinh = request.GioiTinh;
            model.SoTheHDV = request.SoTheHDV;
            model.SoDienThoai = request.SoDienThoai;
            model.CMND = request.CMND;
            model.DiaChi = request.DiaChi;
            model.NoiCapCMND = request.NoiCapCMND;
            model.NgayHetHan = request.NgayHetHan;
            model.Email = request.Email;
            model.HoKhau = request.HoKhau;
            model.LoaiTheId = request.LoaiTheId;
            model.NgayCapCMND = request.NgayCapCMND;
            model.NgayCapThe = request.NgayCapThe;

            if (request.DSQuaTrinhHD != null & request.DSQuaTrinhHD.Count() > 0)
            {
                var qthd = _context.QuaTrinhHoatDong.Where(v => v.HDVId == model.Id);
                foreach (var d in qthd)
                {
                    _context.QuaTrinhHoatDong.Remove(d);
                }
                foreach (var h in request.DSQuaTrinhHD)
                {
                    _context.QuaTrinhHoatDong.Add(new QuaTrinhHoatDong()
                    {
                        HDVId = request.Id,
                        HoatDong = h.HoatDong,
                        KetQua = h.KetQua,
                        ThoiGian = h.ThoiGian
                    });
                }
            }
            if (request.LoaiHinhId != null && request.LoaiHinhId.Count > 0)
            {
                var loaihinh = _context.HuongDanVienLoaiHinh.Where(v => v.HuongDanVienId == model.Id);
                foreach (var d in loaihinh)
                {
                    _context.HuongDanVienLoaiHinh.Remove(d);
                }
                foreach (var x in request.LoaiHinhId)
                {
                    _context.HuongDanVienLoaiHinh.Add(new HuongDanVienLoaiHinh()
                    {
                        HuongDanVienId = model.Id,
                        LoaiHinhId = x
                    });
                }
            }
            if (request.NgonNguId != null && request.NgonNguId.Count > 0)
            {
                var ngongu = _context.HuongDanVienNgonNgu.Where(v => v.HuongDanVienId == model.Id);
                foreach (var d in ngongu)
                {
                    _context.HuongDanVienNgonNgu.Remove(d);
                }
                foreach (var x in request.NgonNguId)
                {
                    _context.HuongDanVienNgonNgu.Add(new HuongDanVienNgonNgu()
                    {
                        HuongDanVienId = model.Id,
                        NgonNguId = x
                    });
                }
            }
            if (request.DSVanBan != null && request.DSVanBan.Count() > 0)
            {
                var dichvu = _context.GiayPhep.Where(v => v.LinhVucId.Contains(Convert.ToInt32(LinhVucKinhDoanh.HDV).ToString())).ToList();

                foreach (var d in dichvu)
                {
                    var lstVanBan = _context.HoSoVanBan.Where(v => v.HosoId == request.Id && v.GiayPhepId == d.Id && v.Loai == LoaiFile.hosohuongdanvien.ToString()).ToList();
                    if (lstVanBan != null && lstVanBan.Count > 0)
                    {
                        var obj = lstVanBan.FirstOrDefault();
                        var value = request.DSVanBan.Where(v => v.GiayPhepId == obj.GiayPhepId).ToList();
                        var objRequest = value.FirstOrDefault();
                        obj.FileName = objRequest.FileName;
                        obj.FilePath = objRequest.FilePath;
                        obj.NoiCap = objRequest.NoiCap;
                        obj.TenGoi = objRequest.TenGoi;
                        obj.IsStatus = objRequest.IsStatus;
                        _context.HoSoVanBan.Update(obj);
                    }
                    else
                    {
                        var objRequest = request.DSVanBan.Single(v => v.GiayPhepId.Equals(d.Id));

                        var obj = new HoSoVanBan()
                        {
                            FileName = objRequest.FileName,
                            FilePath = objRequest.FilePath,
                            NoiCap = objRequest.NoiCap,
                            TenGoi = objRequest.TenGoi,
                            GiayPhepId = d.Id,
                            NgayCap = DateTime.Now,
                            NgayHetHan = DateTime.Now,
                            HosoId = request.Id,
                            IsDelete = false,
                            IsStatus = objRequest.IsStatus,
                            MaSo = "",
                            Loai = LoaiFile.hosohuongdanvien.ToString()
                        };
                        _context.HoSoVanBan.Add(obj);
                    }
                }
            }
            _context.HuongDanVien.Update(model);

            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                return new ApiSuccessResult<int>(id, "Cập nhật hướng dẫn viên thành công");
            }
            return new ApiErrorResult<int>("Sửa lỗi!");
        }

        public async Task<ApiResult<bool>> UploadImage(int id, ImageUploadRequest request)
        {
            try
            {
                if (request.Images != null)
                {
                    foreach (var d in request.Images)
                    {
                        var image = new FileUpload()
                        {
                            FileName = d.FileName,
                            FileUrl = await this.SaveFile(d),
                            IsImage = true,
                            IsStatus = true,
                            Type = LoaiFile.hosohuongdanvien.ToString(),
                            Id = id
                        };
                        _context.FileUploads.Add(image);
                    }
                }
                var result = await _context.SaveChangesAsync();
                if (result > 0)
                {
                    return new ApiSuccessResult<bool>();
                }
                else
                {
                    return new ApiErrorResult<bool>("Upload file lỗi");
                }
            }
            catch (Exception ex)
            {
                return new ApiErrorResult<bool>(ex.Message);
            }
        }

        private async Task<string> SaveFile(IFormFile file)
        {
            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            await _storageService.SaveFileAsync(file.OpenReadStream(), fileName);
            return "/" + USER_CONTENT_FOLDER_NAME + "/" + fileName;
        }

        public async Task<ApiResult<bool>> ImportHuongDanVien(List<HuongDanVienImport> request)
        {
            try
            {
                if (request != null && request.Count > 0)
                {
                    foreach (var obj in request)
                    {
                        bool isGioiTinh = !String.IsNullOrEmpty(obj.GioiTinh) ? obj.GioiTinh.Trim() == "Nam" ? true : false : false;
                        var huongDanVien = new HuongDanVien()
                        {
                            HoVaTen = obj.HoVaTen,
                            CMND = obj.CMND,
                            DiaChi = obj.DiaChi,
                            Email = obj.Email,
                            GioiTinh = isGioiTinh,
                            HoKhau = obj.HoKhau,
                            LoaiTheId = obj.LoaiTheId,
                            NgayCapCMND = obj.NgayCapCMND,
                            NgayCapThe = obj.NgayCapThe,
                            NgayHetHan = obj.NgayHetHan,
                            NoiCapCMND = obj.NoiCapCMND,
                            SoDienThoai = obj.SoDienThoai,
                            SoTheHDV = obj.SoTheHDV,
                            CongTyLuHanhId = 0,
                            NoiCapThe = obj.NoiCapThe,
                            NgaySinh = obj.NgaySinh
                        };
                        _context.HuongDanVien.Add(huongDanVien);
                    }
                    var result = await _context.SaveChangesAsync();
                    if (result > 0)
                    {
                        return new ApiSuccessResult<bool>(true, "Import dữ liệu thành công!");
                    }
                    else
                        return new ApiErrorResult<bool>("Import dữ liệu không thành công");
                }
                else
                    return new ApiSuccessResult<bool>(true, "Không có dữ liệu trong file upload");
            }
            catch (Exception ex)
            {
                return new ApiErrorResult<bool>(ex.Message);
            }
        }
    }
}