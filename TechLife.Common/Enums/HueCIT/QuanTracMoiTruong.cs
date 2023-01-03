using System;
using System.Collections.Generic;
using System.Text;

namespace TechLife.Common.Enums.HueCIT
{
    public class QuanTracMoiTruong
    {
        public enum TrangThaiQuanTrac : int
        {
            [StringValue(@"Tốt")]
            Good = 1,
            [StringValue(@"Vừa phải")]
            Moderate = 2,
            [StringValue(@"Nhạy cảm")]
            UnhealthyforSensitiveGroups = 3,
            [StringValue(@"Không khỏe mạnh")]
            Unhealthy = 4,
            [StringValue(@"Rất không khỏe mạnh")]
            VeryUnhealthy = 5,
            [StringValue(@"Nguy hiểm")]
            Hazardous = 6
        }
    }
}
