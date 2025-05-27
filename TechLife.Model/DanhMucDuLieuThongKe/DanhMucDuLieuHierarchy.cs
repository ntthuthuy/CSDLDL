namespace TechLife.Model.DanhMucDuLieuThongKe
{
    public class DanhMucDuLieuHierarchy
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string IdString { get; set; }
        public string Parents { get; set; }
        public int Level { get; set; }
        public int Order { get; set; }
    }
}
