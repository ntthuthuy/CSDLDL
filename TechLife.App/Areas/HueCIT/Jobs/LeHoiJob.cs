using Microsoft.Extensions.Logging;
using Quartz;
using System;
using System.Threading.Tasks;
using TechLife.App.Areas.HueCIT.Interface.Schedules;

namespace TechLife.App.Areas.HueCIT.Jobs
{
    [DisallowConcurrentExecution]
    public class LeHoiJob : IJob
    {
        private readonly ILogger<LeHoiJob> _logger;
        private readonly ILeHoiScheduleRepositoty _leHoiScheduleRepositoty;
        public LeHoiJob(ILogger<LeHoiJob> logger, ILeHoiScheduleRepositoty leHoiScheduleRepositoty)
        {
            _logger = logger;
            _leHoiScheduleRepositoty = leHoiScheduleRepositoty;
        }
        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                _logger.LogInformation($"\n\nThong bao: Thoi gian bat dau dong bo {DateTime.Now} and JobType: {context.JobDetail.JobType}");

                await _leHoiScheduleRepositoty.GetData();

                _logger.LogInformation($"\n\nThong bao: \n Thoi gian ket thuc dong bo {DateTime.Now} and JobType: {context.JobDetail.JobType}");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Thông báo: \n Đồng bộ thất bại: {DateTime.Now} and JobType: {context.JobDetail.JobType}");
            }
        }
    }
}
