using Microsoft.Extensions.Logging;
using Quartz;
using System;
using System.Threading.Tasks;
using TechLife.App.Areas.HueCIT.Interface;

namespace TechLife.App.Areas.HueCIT.Jobs
{
    [DisallowConcurrentExecution]
    public class AmThucJob : IJob
    {
        private readonly ILogger<AmThucJob> _logger;
        private readonly IAmThucRepository _amThucRepository;
        public AmThucJob(ILogger<AmThucJob> logger,
                         IAmThucRepository amThucRepository)
        {
            _logger = logger;
            _amThucRepository = amThucRepository;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                _logger.LogInformation($"Thông báo: \n Thời gian bắt đầu đồng bộ {DateTime.Now} and JobType: {context.JobDetail.JobType}");

                await _amThucRepository.GetData();

                _logger.LogInformation($"Thông báo: \n Thời gian kết thúc đồng bộ {DateTime.Now} and JobType: {context.JobDetail.JobType}");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Thông báo: \n Đồng bộ thất bại: {DateTime.Now} and JobType: {context.JobDetail.JobType}");
            }
        }
    }
}
