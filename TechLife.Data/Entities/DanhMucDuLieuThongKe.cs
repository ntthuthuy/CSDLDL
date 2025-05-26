namespace TechLife.Data.Entities
{
    public class DanhMucDuLieuThongKe
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string DVT { get; set; }
        public int? ParentId { get; set; }
        public bool IsDelete { get; set; }
    }
}
