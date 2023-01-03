using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using TechLife.Common.Enums.HueCIT;
using TechLife.Model;

namespace TechLife.App.Areas.HueCIT.Models
{
    public class FileUpload
    {
        public int ID { get; set; }
        public string TenFile { get; set; }
        public string DuongDan { get; set; }
        public string LoaiDoiTuong { get; set; }
        public int LoaiFile { get; set; }
        public string LoaiHinhDuLieu { get; set; }
        public DateTime NgayTao { get; set; }
        public bool TrangThai { get; set; }
        public string IDDoiTuong { get; set; }
    }

    public class FileUploadTargetRequestAdd
    {
        public string LoaiDoiTuong { get; set; }
        public string ID { get; set; }
    }

    public class FilesUploadRequest
    {
        public List<IFormFile> Images { get; set; }
    }
}
