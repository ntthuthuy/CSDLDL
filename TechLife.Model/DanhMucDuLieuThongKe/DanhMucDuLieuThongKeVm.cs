namespace TechLife.Model.DanhMucDuLieuThongKe
{
    public class DanhMucDuLieuThongKeVm
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string DVT { get; set; }
        public int? ParentId { get; set; }
        public bool IsParent { get; set; }
        public bool IsDelete { get; set; }
        public int Level { get; set; }
    }
}
