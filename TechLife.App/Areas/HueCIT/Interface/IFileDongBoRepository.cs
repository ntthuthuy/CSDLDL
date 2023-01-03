using System.Collections.Generic;
using System.Threading.Tasks;
using TechLife.App.Areas.HueCIT.Models;

namespace TechLife.App.Areas.HueCIT.Interface
{
    public interface IFileDongBoRepository
    {
        Task<List<FileUpload>> GetsDongBo(string table, string id, int nguondongbo);
        Task<int> DeleteWithparentDongBo(string table, string id, int nguondongbo);
    }
}
