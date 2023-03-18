using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace TechLife.SSO
{
    public static class MaDangNhap
    {
        public static bool TaoMaDangNhap(string code, string token)
        {
            try
            {
                string strPath = HttpContext.Current.Server.MapPath($"~/MaDangNhap/{DateTime.Now.Year}{DateTime.Now.Month}/");
                string strPathDelete = HttpContext.Current.Server.MapPath($"~/MaDangNhap/{DateTime.Now.Year}{(DateTime.Now.Month - 1)}");
                DirectoryInfo directory = new DirectoryInfo(strPathDelete);
                if (directory.Exists)
                {
                    directory.Delete(true);
                }
                if (!Directory.Exists(strPath))
                {
                    Directory.CreateDirectory(strPath);
                }
                strPath = strPath + code + ".txt";
                if (!File.Exists(strPath))
                {
                    File.Create(strPath).Dispose();
                }
                using (StreamWriter sw = File.AppendText(strPath))
                {
                    sw.WriteLine(token);
                    sw.Flush();
                    sw.Close();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static string GetMaDangNhap(string code)
        {
            try
            {
                string filePath = HttpContext.Current.Server.MapPath($"~/MaDangNhap/{DateTime.Now.Year}{DateTime.Now.Month}/" + code + ".txt");
                if (!File.Exists(filePath))
                {
                    return string.Empty;
                }

                string token = File.ReadAllText(filePath);
                File.Delete(filePath);
                return token;
            }
            catch
            {
                return string.Empty;
            }

        }
    }
}