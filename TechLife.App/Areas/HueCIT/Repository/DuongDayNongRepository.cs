using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using TechLife.App.Areas.HueCIT.Interface;
using TechLife.App.Areas.HueCIT.Models;
using TechLife.Common;
using TechLife.Common.Enums;
using TechLife.Common.Enums.HueCIT;

namespace TechLife.App.Areas.HueCIT.Repository
{
    public class DuongDayNongRepository : ConnectDB, IDuongDayNongRepository
    {
        private readonly SqlConnection _conn;
        public DuongDayNongRepository(IConfiguration configuration) : base(configuration)
        {
            _conn = IConnectData();
        }
        public async Task<IEnumerable<DuongDayNongTrinhDien>> Gets(DuongDayNongRequest data)
        {
            using (SqlConnection conn = IConnectData())
            {
                try
                {
                    await conn.OpenAsync();
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@NhomDonVi", data.NhomDonVi);
                    parameters.Add("@ID", data.DonVi);
                    IEnumerable<DuongDayNongTrinhDien> list = conn.Query<DuongDayNongTrinhDien>("SP_DuongDayNongGets", param: parameters, commandType: CommandType.StoredProcedure);
                    foreach (DuongDayNongTrinhDien l in list)
                    {
                        if (Enum.IsDefined(typeof(NhomDuongDayNong), l.NhomDonVi))
                        {
                            l.TenNhom = StringEnum.GetStringValue(l.NhomDonVi);
                        }
                        else
                        {
                            l.TenNhom = "Không xác định";
                        }
                    }

                    return list;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (conn != null)
                    {
                        conn.Close();
                    }
                }
            }
        }

        public async Task<DuongDayNongTrinhDien> Get(int id)
        {
            using (SqlConnection conn = IConnectData())
            {
                try
                {
                    await conn.OpenAsync();
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@ID", id);
                    DuongDayNongTrinhDien list = conn.QueryFirstOrDefault<DuongDayNongTrinhDien>("SP_DuongDayNongGet", parameters, commandType: CommandType.StoredProcedure);
                    if (Enum.IsDefined(typeof(NhomDuongDayNong), list.NhomDonVi))
                    {
                        list.TenNhom = StringEnum.GetStringValue(list.NhomDonVi);
                    }
                    else
                    {
                        list.TenNhom = "Không xác định";
                    }

                    return list;

                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (conn != null)
                    {
                        conn.Close();
                    }
                }
            }
        }

        public async Task<DuongDayNong> Add(DuongDayNong data)
        {
            using (SqlConnection conn = IConnectData())
            {
                try
                {
                    await conn.OpenAsync();
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@NhomDonVi", data.NhomDonVi);
                    parameters.Add("@DonViTiepNhan", data.DonViTiepNhan);
                    parameters.Add("@DienThoai", data.DienThoai);
                    parameters.Add("@DiaChi", data.DiaChi);
                    DuongDayNong list = await SqlMapper.QueryFirstOrDefaultAsync<DuongDayNong>(conn, "SP_DuongDayNongAdd", parameters, commandType: CommandType.StoredProcedure);
                    return list;

                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (conn != null)
                    {
                        conn.Close();
                    }
                }
            }
        }

        public async Task<DuongDayNong> Edit(DuongDayNong data)
        {
            using (SqlConnection conn = IConnectData())
            {
                try
                {
                    await conn.OpenAsync();
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@ID", data.ID);
                    parameters.Add("@NhomDonVi", data.NhomDonVi);
                    parameters.Add("@DonViTiepNhan", data.DonViTiepNhan);
                    parameters.Add("@DienThoai", data.DienThoai);
                    parameters.Add("@DiaChi", data.DiaChi);
                    DuongDayNong list = await SqlMapper.QueryFirstOrDefaultAsync<DuongDayNong>(conn, "SP_DuongDayNongEdit", parameters, commandType: CommandType.StoredProcedure);
                    return list;

                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (conn != null)
                    {
                        conn.Close();
                    }
                }
            }
        }

        public async Task<int> Delete(int id)
        {
            using (SqlConnection conn = IConnectData())
            {
                try
                {
                    await conn.OpenAsync();
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@ID", id);
                    int kq = await SqlMapper.ExecuteAsync(conn, "SP_DuongDayNongDelete", parameters, commandType: CommandType.StoredProcedure);
                    return kq;

                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (conn != null)
                    {
                        conn.Close();
                    }
                }
            }
        }

        public async Task<IEnumerable<DuongDayNongSearch>> GetsSearch()
        {
            using (SqlConnection conn = IConnectData())
            {
                try
                {
                    await conn.OpenAsync();
                    IEnumerable<DuongDayNongSearch> list = conn.Query<DuongDayNongSearch>("SP_DuongDayNongGetsSearch", commandType: CommandType.StoredProcedure);

                    return list;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (conn != null)
                    {
                        conn.Close();
                    }
                }
            }
        }
    }
}
