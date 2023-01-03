using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using TechLife.Model.HoSoVanBan;
using TechLife.Model.LoaiPhong;

namespace TechLife.Model.DuLieuDuLich
{

    public class DuLieuDuLichUpdateRequest
    {
        public DuLieuDuLichModel DuLieuDuLich { get; set; }

        public ImageUploadRequest Images { get; set; }
        public DocumentUploadRequest Files { get; set; }
        public List<HoSoVanBanCreateRequets> Docs { get; set; }
        public List<LoaiPhongGiuong> DSLoaiPhongGiuong { get; set; }
    }

    //HueCIT
    public class DuLieuDuLichUpdateExtRequest
    {
        public DuLieuDuLichModel DuLieuDuLich { get; set; }

        public ImageUploadExtRequest Images { get; set; }
        public DocumentUploadRequest Files { get; set; }
        public List<HoSoVanBanCreateRequets> Docs { get; set; }
    }
}
