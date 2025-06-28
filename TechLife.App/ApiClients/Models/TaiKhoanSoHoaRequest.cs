namespace TechLife.App.ApiClients.Models
{
    public class TaiKhoanSoHoaRequest
    {
        public int doituongid { get; set; }
        public int trangthai { get; set; }
        public int page { get; set; }
        public int perpage { get; set; }
        public string mdd { get; set; }
    }
}
