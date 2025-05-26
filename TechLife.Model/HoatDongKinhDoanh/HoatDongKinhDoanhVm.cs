namespace TechLife.Model.HoatDongKinhDoanh
{
    public class HoatDongKinhDoanhVM
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string DVT { get; set; }
        public decimal ChinhThucThangTruoc { get; set; }
        public decimal UocThangHienTai { get; set; }
        public decimal LuyKeDauNam { get; set; }
        public decimal DuTinhUocThangSau { get; set; }
        public int? ParentId { get; set; }
        public bool IsDelete { get; set; }
    }
}
