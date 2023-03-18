using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechLife.SSO
{
    public class KetQuaXacThuc
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public User UserObj { get; set; }

    }
    public class User
    {
        public string TaiKhoan { get; set; }
        public string HoVaTen { get; set; }
        public string TenDonVi { get; set; }
    }
    public static class XacThucSso
    {
        public static KetQuaXacThuc XacThucDangNhap(string token)
        {
            try
            {
                if (string.IsNullOrEmpty(token))
                    return new KetQuaXacThuc()
                    {
                        IsSuccess = false,
                        Message = "Xác thực không thành công! Mã không hợp lệ.",
                        UserObj = null
                    };
                else
                {
                    var decrypt = HashUtil.Decrypt(token);
                    if (string.IsNullOrEmpty(decrypt))
                        return new KetQuaXacThuc()
                        {
                            IsSuccess = false,
                            Message = "Xác thực không thành công! Mã không hợp lệ.",
                            UserObj = null
                        };
                    else
                    {
                        var arr = decrypt.Split('|');
                        if (arr == null || arr.Length < 3)
                        {
                            return new KetQuaXacThuc()
                            {
                                IsSuccess = false,
                                Message = "Xác thực không thành công! Mã không hợp lệ."
                            };
                        }
                        else
                        {
                            DateTime time = Convert.ToDateTime(arr[3]);
                            if (DateTime.Now > time.AddMinutes(1))
                                return new KetQuaXacThuc()
                                {
                                    IsSuccess = false,
                                    Message = "Xác thực không thành công! Mã không hợp lệ.",
                                    UserObj = null
                                };
                            else
                                return new KetQuaXacThuc()
                                {
                                    IsSuccess = true,
                                    Message = "Xác thực thành công!",
                                    UserObj = new User()
                                    {
                                        TaiKhoan = arr[0],
                                        HoVaTen = arr[1],
                                        TenDonVi = arr[2],
                                    }
                                };
                        }
                    }
                }
            }
            catch
            {
                return new KetQuaXacThuc()
                {
                    IsSuccess = false,
                    Message = "Xác thực không thành công!",
                    UserObj = null
                };
            }

        }
    }
}
