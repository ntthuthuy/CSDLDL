namespace TechLife.Model.ChuyenDi
{
    public class HanhTrinhChuyenDiUpdateRequest
    {
        public int Id { get; set; }
        public int DiaDiemId { get; set; }
        public string MoTa { get; set; }
        public int Ngay { get; set; }
        public int Gio { get; set; }
        public int Phut { get; set; }
    }
}