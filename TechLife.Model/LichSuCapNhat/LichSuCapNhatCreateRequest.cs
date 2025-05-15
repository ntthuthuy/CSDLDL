using System;
using TechLife.Model.DuLieuDuLich;

namespace TechLife.Model.LichSuCapNhat
{
    public class LichSuCapNhatCreateRequest
    {
        public DuLieuDuLichModel OldValue { get; set; }
        public DuLieuDuLichModel NewValue { get; set; }
        public Guid UpdateByUserId { get; set; }
        public int HoSoId { get; set; }
    }
}
