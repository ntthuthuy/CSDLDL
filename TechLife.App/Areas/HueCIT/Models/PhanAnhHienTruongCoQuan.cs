using System;
using System.Collections.Generic;

namespace TechLife.App.Areas.HueCIT.Models
{
    public class PhanAnhHienTruongCoQuan
    {
        public string Id { get; set; }
        public string TenCoQuan { get; set; }
    }

    #region ĐỒNG BỘ
    public class PhanAnhHienTruongCoQuanDongBo
    {
        public string UniqueCode { get; set; }
        public string Address { get; set; }
        public object OrganizationParentId { get; set; }
        public object Mission { get; set; }
        public string OfficePhone { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public int Orders { get; set; }
        public string Fax { get; set; }
        public int OrganizationLevel { get; set; }
        public object OrganizationLevelID { get; set; }
        public string OperatedWebsite { get; set; }
        public string Culture { get; set; }
        public string OrganizationName { get; set; }
        public object OrganizationAbbrName { get; set; }
        public object Introduction { get; set; }
        public bool Enabled { get; set; }
        public bool IsManageArea { get; set; }
        public object RefLanguageID { get; set; }
        public bool UseParentSite { get; set; }
        public string ID { get; set; }
        public object OwnerCode { get; set; }
        public int ModuleId { get; set; }
        public int CreatedByUserId { get; set; }
        public int LastModifiedByUserId { get; set; }
        public DateTime CreatedOnDate { get; set; }
        public object LastModifiedOnDate { get; set; }
    }

    public class DanhSachPhanAnhHienTruongCoQuanDongBo
    {
        public int code { get; set; }
        public string message { get; set; }
        public object description { get; set; }
        public List<PhanAnhHienTruongCoQuanDongBo> data { get; set; }
        public object optionValues { get; set; }
    }
    #endregion
}
