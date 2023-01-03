using Microsoft.Extensions.Logging;
using Quartz;
using System;
using System.Threading.Tasks;
using TechLife.App.Areas.HueCIT.Interface.Schedules;

namespace TechLife.App.Areas.HueCIT.Jobs
{
    [DisallowConcurrentExecution]
    public class HienTruongTheoLinhVucJob : IJob
    {
        private readonly ILogger<HienTruongTheoLinhVucJob> _logger;
        private readonly IPhanAnhHienTruongScheduleRepository _repository;
        public HienTruongTheoLinhVucJob(ILogger<HienTruongTheoLinhVucJob> logger,
                                    IPhanAnhHienTruongScheduleRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }
        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                _logger.LogInformation($"Thông báo: \n Thời gian bắt đầu đồng bộ {DateTime.Now} and JobType: {context.JobDetail.JobType}");

                // Param nhận từ [Job]_[Quartz]
                JobDataMap dataMap = context.JobDetail.JobDataMap;

                // Đồng bộ hiện trường theo id lĩnh vực
                // Loại bot dữ liệu nào có lĩnh vực không bằng lĩnh vực được chọn
                // (đã xử lý)
                await _repository.GetDataLinhVuc(dataMap.GetInt("id"), dataMap.GetBoolean("isEnableUpdate"));

                //(đang xử lý)
                await _repository.GetDataLinhVuc(dataMap.GetInt("id"), dataMap.GetBoolean("isEnableUpdate"));

                _logger.LogInformation($"Thông báo: \n Thời gian kết thúc đồng bộ {DateTime.Now} and JobType: {context.JobDetail.JobType}");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Thông báo: \n Đồng bộ thất bại: {DateTime.Now} and JobType: {context.JobDetail.JobType}");
                throw ex;
            }
        }
    }
}
