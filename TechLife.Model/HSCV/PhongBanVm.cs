using System;
using System.Collections.Generic;
using System.Text;

namespace TechLife.Model.HSCV
{
    public class PhongBanVm
    {
        public string UniqueCode { get; set; }
        public string DepartmentName { get; set; }
        public string DepartmentAbbrName { get; set; }
        public object Culture { get; set; }
        public bool? Enabled { get; set; }
        public int? Orders { get; set; }
        public string OfficePhone { get; set; }
        public string Mission { get; set; }
        public string OrganizationId { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Website { get; set; }
        public string RefLanguageID { get; set; }
        public string ID { get; set; }
        public string OwnerCode { get; set; }
        public int? ModuleId { get; set; }
        public int? CreatedByUserId { get; set; }
        public int? LastModifiedByUserId { get; set; }
        public DateTime? CreatedOnDate { get; set; }
        public DateTime? LastModifiedOnDate { get; set; }
    }
    public class TrungTamVm
    {
        public string UniqueCode { get; set; }
        public string OrganizationName { get; set; }
    }
}
