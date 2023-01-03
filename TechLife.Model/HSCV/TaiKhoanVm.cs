using System;
using System.Collections.Generic;
using System.Text;

namespace TechLife.Model.HSCV
{
    public class TaiKhoanVm
    {
        public string ID { get; set; }
        public string UID { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string FullName { get; set; }
        public string LastName { get; set; }
        public string OfficePhone { get; set; }
        public string Email { get; set; }
        public string OrganizationId { get; set; }
        public string OrganizationName { get; set; }
        public string Title { get; set; }
        public int? Sex { get; set; }
        public int Orders { get; set; }
        public string DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public DateTime? DOB { get; set; }
    }
}
