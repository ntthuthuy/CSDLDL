using System;
using System.Collections.Generic;
using System.Text;

namespace TechLife.Common
{
    public class SystemConstants
    {
        public const string MainConnectionString = "SolutionDb";
        public const string Alerts = "Alerts";
        public const string UrlBack = "UrlBack";
        public const string UrlIndex = "UrlIndex";

        public const int pageSize = 10;
        public const int pageIndex = 1;
        public class AppSettings
        {
            public const string UserInfo = "UserInfo";
            public const string DefaultLanguageId = "DefaultLanguageId";
            public const string Token = "Token";
            public const string BaseAddress = "BaseAddress";
            public const string HSCVAddress = "HSCVAddress";
            public const string SsoAddress = "SsoAddress";
            public const string UniqueCode = "00.23.H57";
            public const string UniqueCodeTrungTam = "01.23.H57";
        }

        public class ProductSettings
        {
            public const int NumberOfFeaturedProducts = 4;
            public const int NumberOfLatestProducts = 6;
        }

        public class ProductConstants
        {
            public const string NA = "N/A";
        }
    }
}
