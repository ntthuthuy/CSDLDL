using Microsoft.Extensions.Logging;
using Quartz;
using System.Threading.Tasks;
using System;
using TechLife.App.Areas.HueCIT.Interface.Schedules;

namespace TechLife.App.Areas.HueCIT.Jobs
{
    [DisallowConcurrentExecution]
    public class DiemVeSinhJob : IJob
    {
        private readonly ILogger<DiemVeSinhJob> _logger;
        private readonly IDiemVeSinhScheduleRepository _diemVeSinhScheduleRepository;
        public DiemVeSinhJob(ILogger<DiemVeSinhJob> logger,
                         IDiemVeSinhScheduleRepository diemVeSinhScheduleRepository)
        {
            _logger = logger;
            _diemVeSinhScheduleRepository = diemVeSinhScheduleRepository;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                _logger.LogInformation($"Thông báo: \n Thời gian bắt đầu đồng bộ {DateTime.Now} and JobType: {context.JobDetail.JobType}");

                await _diemVeSinhScheduleRepository.GetData();

                _logger.LogInformation($"Thông báo: \n Thời gian kết thúc đồng bộ {DateTime.Now} and JobType: {context.JobDetail.JobType}");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Thông báo: \n Đồng bộ thất bại: {DateTime.Now} and JobType: {context.JobDetail.JobType}");
            }
        }
    }
}
