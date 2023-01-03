using Microsoft.Extensions.Logging;
using Quartz;
using System;
using System.Threading.Tasks;
using TechLife.App.Areas.HueCIT.Interface.Schedules;

namespace TechLife.App.Areas.HueCIT.Jobs
{
    [DisallowConcurrentExecution]
    public class DoanhNghiepJob : IJob
    {
        private readonly ILogger<DoanhNghiepJob> _logger;
        private readonly IDoanhNghiepScheduleRepository _doanhNghiepScheduleRepository;
        public DoanhNghiepJob(ILogger<DoanhNghiepJob> logger,
                              IDoanhNghiepScheduleRepository doanhNghiepScheduleRepository)
        {
            _logger = logger;
            _doanhNghiepScheduleRepository = doanhNghiepScheduleRepository;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                _logger.LogInformation($"Thông báo: \n Thời gian bắt đầu đồng bộ {DateTime.Now} and JobType: {context.JobDetail.JobType}");

                // Đồng bộ danh sách danh mục doanh nghiệp
                await _doanhNghiepScheduleRepository.GetDataLoaiHinh();

                await _doanhNghiepScheduleRepository.GetDataLoaiVanBan();

                await _doanhNghiepScheduleRepository.GetDataTrangThai();

                await _doanhNghiepScheduleRepository.GetDataNganhNghe();


                // Đồng bộ doanh nghiệp xong sẽ trả về danh sách string mã doanh nghiệp
                // Nếu có : danh sách mã doanh nghiệp --> thêm mới văn bản 
                await _doanhNghiepScheduleRepository.GetDataDoanhNghiep();

                await _doanhNghiepScheduleRepository.GetDataVanBan();

                _logger.LogInformation($"Thông báo: \n Thời gian kết thúc đồng bộ {DateTime.Now} and JobType: {context.JobDetail.JobType}");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Thông báo: \n Đồng bộ thất bại: {DateTime.Now} and JobType: {context.JobDetail.JobType}");
            }
        }
    }
}
