using System.Collections.Generic;

namespace TechLife.Data.Entities
{
    public class MucDoThongThaoNgoaiNgu
    {
        public int Id { get; set; }
        public string MucDo { get; set; }
        public string MoTa { get; set; }
        public bool IsStatus { get; set; }
        public bool IsDelete { get; set; }
        public string NgonNguId { get; set; }
        public List<MucDoTTNNHoSo> DSMucDoTTNNHoSo { get; set; }
    }
}
