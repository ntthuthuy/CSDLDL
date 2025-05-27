namespace TechLife.Data.Entities
{
    public class HoatDongKinhDoanh
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string DVT { get; set; }
        public decimal ChinhThucThangTruoc { get; set; }
        public decimal UocThangHienTai { get; set; }
        public decimal LuyKeTuDauNam { get; set; }
        public decimal DuTinhUocThangSau { get; set; }
        public int? ParentId { get; set; }
        public bool IsDelete { get; set; }
    }
}
