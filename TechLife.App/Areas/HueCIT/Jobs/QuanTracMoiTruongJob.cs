using Microsoft.Extensions.Logging;
using Quartz;
using System;
using System.Threading.Tasks;
using TechLife.App.Areas.HueCIT.Interface;

namespace TechLife.App.Areas.HueCIT.Jobs
{
    [DisallowConcurrentExecution]
    public class QuanTracMoiTruongJob : IJob
    {
        private readonly ILogger<QuanTracMoiTruongJob> _logger;
        private readonly IQuanTracMoiTruongRepository _quanTracMoiTruongRepository;
        public QuanTracMoiTruongJob(ILogger<QuanTracMoiTruongJob> logger,
                                    IQuanTracMoiTruongRepository quanTracMoiTruongRepository)
        {
            _logger = logger;
            _quanTracMoiTruongRepository = quanTracMoiTruongRepository;
        }
        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                _logger.LogInformation($"Thông báo: \n Thời gian bắt đầu đồng bộ {DateTime.Now} and JobType: {context.JobDetail.JobType}");

                await _quanTracMoiTruongRepository.GetData();

                _logger.LogInformation($"Thông báo: \n Thời gian kết thúc đồng bộ {DateTime.Now} and JobType: {context.JobDetail.JobType}");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Thông báo: \n Đồng bộ thất bại: {DateTime.Now} and JobType: {context.JobDetail.JobType}");
            }
        }
    }
}
