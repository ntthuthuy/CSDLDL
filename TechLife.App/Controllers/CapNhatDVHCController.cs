using ExcelDataReader;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using TechLife.Service;

namespace TechLife.App.Controllers
{
    public class CapNhatDVHCController : BaseController
    {
        private readonly IDuLieuDuLichService _duLieuDuLichService;
        private readonly IDiaPhuongService diaPhuongService;

        public CapNhatDVHCController(IUserService userService,
            IConfiguration configuration,
            IDuLieuDuLichService duLieuDuLichService,
            IDiaPhuongService diaPhuongService,
            ITrackingService trackingService = null) : base(userService, configuration, trackingService)
        {
            _duLieuDuLichService = duLieuDuLichService;
            this.diaPhuongService = diaPhuongService;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> UploadDiaChi(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("File không hợp lệ");

            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            using var stream = file.OpenReadStream();
            using var reader = ExcelDataReader.ExcelReaderFactory.CreateReader(stream);
            var result = reader.AsDataSet();

            var table = result.Tables[0];

            // Bỏ dòng tiêu đề (dòng đầu tiên)
            for (int i = 1; i < table.Rows.Count; i++)
            {
                var row = table.Rows[i];

                if (!int.TryParse(row[0]?.ToString(), out int id))
                    continue;

                var soNhaMoi = row[2]?.ToString()?.Trim();
                var duongToDanPho = row[3]?.ToString()?.Trim();
                var xaPhuong = row[4]?.ToString()?.Trim();

                if (!string.IsNullOrEmpty(xaPhuong))
                {
                    var diaPhuongId = await this.diaPhuongService.InsertDiaPhuongAndGetByName(xaPhuong);
                    if (diaPhuongId != 0)
                    {
                        var hoSo = new Model.DuLieuDuLich.DuLieuDuLichModel();
                        hoSo.SoNha = soNhaMoi;
                        hoSo.DuongPho = duongToDanPho;
                        hoSo.QuanHuyenId = diaPhuongId;
                        await _duLieuDuLichService.UpdateDiaPhuong(id, hoSo);
                    }
                }
            }

            return Ok("Cập nhật thành công");
        }

    }
}