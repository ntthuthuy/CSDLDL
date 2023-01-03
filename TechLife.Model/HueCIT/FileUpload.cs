using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace TechLife.Model.HueCIT
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

    public class FileUploadRequest
    {
        public List<IFormFile> Files { get; set; }
    }
}
