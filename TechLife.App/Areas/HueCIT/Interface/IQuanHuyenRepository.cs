using System.Collections.Generic;
using System.Threading.Tasks;
using TechLife.App.Areas.HueCIT.Models;

namespace TechLife.App.Areas.HueCIT.Interface
{
    public interface IQuanHuyenRepository
    {
        Task<IEnumerable<QuanHuyen>> Gets();
    }
}
