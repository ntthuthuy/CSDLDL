using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechLife.App.Areas.HueCIT.Models;
using TechLife.Common;

namespace TechLife.App.Areas.HueCIT.Interface
{
    public interface ITyGiaRepository
    {
        Task<IEnumerable<TyGia>> Gets(TyGiaRequest data);
        Task<TyGia> Get(int id);
        Task<TyGia> Add(TyGia data);
        Task<TyGia> Edit(TyGia data);
        Task<int> Delete(int id);
    }
}
