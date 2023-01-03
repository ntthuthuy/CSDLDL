using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using TechLife.Model.Tour;
namespace TechLife.Model
{
    public class HanhTrinhModel
    {
        public int Id { get; set; }
        public int TourId { get; set; }
        public int Ngay { get; set; }
        public int Gio { get; set; }
        public int Phut { get; set; }
        public int ThoiGian { get; set; }
        public int NoiDenId { get; set; }
        public string Mota { get; set; }
        public bool IsStatus { get; set; }
        public string DiaDiem { get; set; }
        public TourVm Tour { get; set; }
        public List<FileUploadModel> DSHinhAnh { get; set; }
        public ImageUploadRequest Images { get; set; }
       
    }
}
