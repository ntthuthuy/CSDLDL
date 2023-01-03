using System.Collections.Generic;
using TechLife.Model.HoSoVanBan;

namespace TechLife.Model.DuLieuDuLich
{
    public class DuLieuDuLichCreateRequest
    {
        public DuLieuDuLichModel DuLieuDuLich { get; set; }
        public ImageUploadRequest Images { get; set; }
        public DocumentUploadRequest Files { get; set; }
        public List<HoSoVanBanCreateRequets> Docs { get; set; }
    }

    public class DuLieuDuLichCreateExtRequest
    {
        public DuLieuDuLichModel DuLieuDuLich { get; set; }
        public ImageUploadExtRequest Images { get; set; }
        public DocumentUploadRequest Files { get; set; }
        public List<HoSoVanBanCreateRequets> Docs { get; set; }
    }
}