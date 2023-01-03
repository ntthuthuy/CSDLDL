using Microsoft.Extensions.Logging;
using Quartz;
using System;
using System.Threading.Tasks;
using TechLife.App.Areas.HueCIT.Interface.Schedules;

namespace TechLife.App.Areas.HueCIT.Jobs
{
    [DisallowConcurrentExecution]
    public class PhanAnhHienTruongJob : IJob
    {
        private readonly ILogger<PhanAnhHienTruongJob> _logger;
        private readonly IPhanAnhHienTruongScheduleRepository _repository;
        public PhanAnhHienTruongJob(ILogger<PhanAnhHienTruongJob> logger,
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

                await _repository.GetLinhVuc();

                await _repository.GetCoQuan();

                await _repository.GetData();

                await _repository.GetDataWait();

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
