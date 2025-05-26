using System;
using TechLife.Model.DuLieuDuLich;

namespace TechLife.Model.LichSuCapNhat
{
    public class LichSuCapNhatModel
    {
        public int Id { get; set; }
        public DuLieuDuLichModel OldValue { get; set; }
        public DuLieuDuLichModel NewValue { get; set; }
        public DateTime? UpdatedAt { get; set; }
        //public string UpdateByUserId { get; set; }
        public UserModel UpdateByUser { get; set; }
        public int HoSoId { get; set; }
    }
}
