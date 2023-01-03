using Microsoft.Extensions.Logging;
using Quartz;
using System.Threading.Tasks;
using System;
using TechLife.App.Areas.HueCIT.Interface.Schedules;

namespace TechLife.App.Areas.HueCIT.Jobs
{
    [DisallowConcurrentExecution]
    public class DiemGiaoDichJob : IJob
    {
        private readonly ILogger<DiemGiaoDichJob> _logger;
        private readonly IDiemGiaoDichScheduleRepository _diemGiaoDichScheduleRepository;
        public DiemGiaoDichJob(ILogger<DiemGiaoDichJob> logger,
                               IDiemGiaoDichScheduleRepository diemGiaoDichScheduleRepository)
        {
            _logger = logger;
            _diemGiaoDichScheduleRepository = diemGiaoDichScheduleRepository;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                _logger.LogInformation($"Thông báo: \n Thời gian bắt đầu đồng bộ {DateTime.Now} and JobType: {context.JobDetail.JobType}");

                await _diemGiaoDichScheduleRepository.GetData();

                _logger.LogInformation($"Thông báo: \n Thời gian kết thúc đồng bộ {DateTime.Now} and JobType: {context.JobDetail.JobType}");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Thông báo: \n Đồng bộ thất bại: {DateTime.Now} and JobType: {context.JobDetail.JobType}");
            }
        }
    }
}

