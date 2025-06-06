namespace TechLife.Data.Entities
{
    public class GiayPhep
    {
        public int Id { get; set; }
        public string Ten { get; set; }
        public string LinhVucId { get; set; }
        public string NgonNguId { get; set; }

        public virtual NgonNgu NgonNgu { get; set; }
    }
}
