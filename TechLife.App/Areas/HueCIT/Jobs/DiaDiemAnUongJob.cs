using Microsoft.Extensions.Logging;
using Quartz;
using System;
using System.Threading.Tasks;
using TechLife.App.Areas.HueCIT.Interface.Schedules;
using TechLife.Data;
using TechLife.Service.HueCIT;

namespace TechLife.App.Areas.HueCIT.Jobs
{
    [DisallowConcurrentExecution]
    public class DiaDiemAnUongJob : IJob
    {
        private readonly ILogger<DiaDiemAnUongJob> _logger;
        private readonly IHoSoScheduleRepository _hoSoScheduleRepository;
        public DiaDiemAnUongJob(ILogger<DiaDiemAnUongJob> logger,
                                IHoSoScheduleRepository hoSoScheduleRepository)
        {
            _logger = logger;
            _hoSoScheduleRepository = hoSoScheduleRepository;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                _logger.LogInformation($"Thông báo: \n Thời gian bắt đầu đồng bộ {DateTime.Now} and JobType: {context.JobDetail.JobType}");

                await _hoSoScheduleRepository.GetDataDiaDiemAnUong();

                _logger.LogInformation($"Thông báo: \n Thời gian kết thúc đồng bộ {DateTime.Now} and JobType: {context.JobDetail.JobType}");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Thông báo: \n Đồng bộ thất bại: {DateTime.Now} and JobType: {context.JobDetail.JobType}");
            }
        }
    }
}
