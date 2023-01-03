using System;
using System.Collections.Generic;
using System.Text;

namespace TechLife.Model
{
    public class FileUploadModel
    {
        public int FileId { get; set; }
        public string FileName { get; set; }
        public string FileUrl { get; set; }
        public string Type { get; set; }
        public DateTime? NgayTao { get; set; }
        public int Id { get; set; }
        public bool IsImage { get; set; }
        public bool IsStatus { get; set; }
        //HueCIT
        public int? FileType { get; set; }
        public int? NguonDongBo { get; set; }
        public string MoTa { get; set; }
    }
}
