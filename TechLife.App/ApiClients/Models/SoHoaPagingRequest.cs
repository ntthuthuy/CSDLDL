namespace TechLife.App.ApiClients.Models
{
    public class SoHoaPagingRequest<T>
    {
        public string serviceid { get; set; }
        public T thamso { get; set; }
        public int page { get; set; }
        public int perpage { get; set; }
        public bool bangcon { get; set; }
    }
}
