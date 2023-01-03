using System;
using System.Collections.Generic;
using System.Text;

namespace TechLife.Model.DuLieuDuLich
{
    public class DuLieuDuLichPortalRequest
    {
        public string Id { get; set; }
        //[Display(Name = "Tên cơ sở lưu trú")]
        //[Required(ErrorMessage = "Vui lòng nhập tên cơ sở lưu trú")]
        public string Name { get; set; }
        public int OrganId { get; set; }
        //[Display(Name = "Loại hình kinh doanh")]
        public int TypeOfBusinessId { get; set; }
        //[Display(Name = "Tên viết tắt")]
        public string Abbreviation { get; set; }
        //[Display(Name = "Địa chỉ")]
        public string Address { get; set; }
        //[Display(Name = "Số điện thoại")]
        public string PhoneNumber { get; set; }
        public string Fax { get; set; }
        //[EmailAddress(ErrorMessage = "Vui lòng nhập lại email")]
        public string Email { get; set; }
        public string Website { get; set; }
        //[Display(Name = "Họ và tên (Người đại diện)")]
        public string DelegateName { get; set; }
        //[Display(Name = "Chức vụ")]
        public string Posistion { get; set; }
        //[Display(Name = "Số điện thoại")]
        public string DelegatePhoneNumber { get; set; }
        //[Display(Name = "Giờ mở cửa")]
        public string OpenTime { get; set; }
        //[Display(Name = "Giờ đóng cửa")]
        public string CloseTime { get; set; }
        public string Coordinates { get; set; }
        public string CreateByUser { get; set; }
        //[Display(Name = "Danh sách tiện nghi")]
        public List<AmenityVm> Amenities { get; set; }
    }
    public class AmenityVm
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsSelect { get; set; }
    }
}
