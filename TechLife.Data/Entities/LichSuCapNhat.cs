using System;

namespace TechLife.Data.Entities
{
    public class LichSuCapNhat
    {
        public int Id { get; set; }

        public int HoSoId { get; set; }

        public string OldValue { get; set; }

        public string NewValue { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public string UpdateByUserId { get; set; }

        public virtual HoSo HoSo { get; set; }
    }
}
