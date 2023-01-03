using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;

namespace TechLife.Common
{
    public static class Functions
    {
        public static string GetFullDiaPhuong(string sonha, string diachi, string xa, string huyen, string tinh = "")
        {
            string str = "";
            if (!String.IsNullOrEmpty(sonha))
            {
                str += sonha + " - ";
            }
            if (!String.IsNullOrEmpty(diachi))
            {
                str += diachi + " - ";
            }
            if (!String.IsNullOrEmpty(xa))
            {
                str += xa + " - ";
            }
            if (!String.IsNullOrEmpty(huyen))
            {
                str += huyen;
            }
            if (!String.IsNullOrEmpty(tinh))
            {
                str += " - " + tinh;
            }
            return str;
        }

        public static DateTime ConvertDateToSql(string date)
        {
            try
            {
                string str = "";
                if (date.IndexOf("/") > 0)
                {
                    string[] str_split = date.Split('/');
                    str += str_split[2] + "-" + str_split[1] + "-" + str_split[0];
                }
                DateTime date_orc = Convert.ToDateTime(str + " " + DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second);
                return date_orc;
            }
            catch (Exception ex)
            {
                return DateTime.MaxValue;
            }
        }

        public static string GetDatetimeToVn(DateTime? date)
        {
            if (date != null)
            {
                DateTime date2 = Convert.ToDateTime(date);
                if (date2.Year > 0001)
                    return date2.ToString("dd/MM/yyyy");
                else return String.Empty;
            }
            else return String.Empty;
        }

        public static string GetTimeToVn(DateTime date)
        {
            if (date.Year > 0001)
                return date.ToString("hh:mm dd/MM/yyyy");
            else return String.Empty;
        }
          
        public static string ConvertDecimalToVnd(decimal value)
        {
            var cul = System.Globalization.CultureInfo.GetCultureInfo("vi-VN");

            return Convert.ToDouble(value).ToString("#,### vnđ", cul.NumberFormat);
        }
        public static string ConvertDecimalVND(decimal value)
        {
            if (value == 0) return "0";
            var cul = CultureInfo.GetCultureInfo("en-us");

            return Convert.ToDecimal(value).ToString("#,### ", cul.NumberFormat);

        }
        public static string ConvertTimeVn(int gio, int phut)
        {
            string result = "";
            if (gio < 10) result += "0" + gio.ToString() + " giờ";
            else result += gio.ToString() + " giờ"; ;
            if (phut < 10) result += ":0" + phut.ToString() + " phút";
            else result += ":" + phut.ToString() + " phút";

            return result;
        }

        public static bool IsNumeric(object Expression)
        {
            double retNum;

            bool isNum = Double.TryParse(Convert.ToString(Expression), System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out retNum);
            return isNum;
        }

        public static int TongSoNgay(string tungay, string denngay)
        {
            DateTime tngay = Functions.ConvertDateToSql(tungay);
            DateTime dngay = Functions.ConvertDateToSql(denngay);
            TimeSpan Time = dngay - tngay;
            int songay = Time.Days;
            return songay;
        }

        public static List<T> ToListData<T>(this DataTable dataTable)
        {
            var dataList = new List<T>();
            dataList = JsonConvert.DeserializeObject<List<T>>(ToJsonData(dataTable));
            return dataList;
        }

        public static T ToData<T>(this DataTable dataTable) where T : new()
        {
            var dataList = new T();
            dataList = JsonConvert.DeserializeObject<T>(ToJsonData(dataTable));
            return dataList;
        }

        private static string ToJsonData(DataTable dataTable)
        {
            return JsonConvert.SerializeObject(dataTable);
        }
    }
}