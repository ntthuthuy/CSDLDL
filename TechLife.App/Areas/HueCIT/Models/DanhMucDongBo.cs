namespace TechLife.App.Areas.HueCIT.Models
{
    public class DanhMucDiemDuLichFormData
    {
        public string serviceid { get; set; }
        // 0: create, > 0: update
        public string diemdlid { get; set; }
        public string idloaihinh { get; set; }
        public string tenloaihinh { get; set; }
    }

    public class DanhMucEformidFormData
    {
        public string serviceid { get; set; }
        // 0: create, > 0: update
        public string eformid { get; set; }
        public string idloaihinh { get; set; }
        public string tenloaihinh { get; set; }
    }

    public class DanhMucCoSoMuaSamFormData
    {
        public string serviceid { get; set; }
        // 0: create, > 0: update
        public string cosoid { get; set; }
        public string idloaihinh { get; set; }
        public string tenloaihinh { get; set; }
    }

    public class DanhMucLuHanhFormData
    {
        public string serviceid { get; set; }
        // 0: create, > 0: update
        public string phanloaiid { get; set; }
        public string idloaihinh { get; set; }
        public string tenloaihinh { get; set; }
    }

    public class DanhMucResponse
    {
        public int code { get; set; }
        public string message { get; set; }
        public string data { get; set; }
    }
}
