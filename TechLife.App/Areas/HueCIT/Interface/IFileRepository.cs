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
    public interface IFileRepository
    {
        Task<List<FileUpload>> Gets(string table, string id);
        Task<int> Delete(int id);
        Task<int> DeleteWithparent(string table, string id);
    }
}
