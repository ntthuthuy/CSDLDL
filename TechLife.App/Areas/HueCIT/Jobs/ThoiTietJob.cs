using Microsoft.Extensions.Logging;
using Quartz;
using System;
using System.Threading.Tasks;
using TechLife.App.Areas.HueCIT.Interface;

namespace TechLife.App.Areas.HueCIT.Jobs
{
    [DisallowConcurrentExecution]
    public class ThoiTietJob : IJob
    {
        private readonly ILogger<ThoiTietJob> _logger;
        private readonly IThoiTietRepository _repositoryThoiTiet;
        private readonly IThoiTietSymbolRepository _repositoryThoiTietSymbol;
        public ThoiTietJob(ILogger<ThoiTietJob> logger,
                           IThoiTietRepository repositoryThoiTiet,
                           IThoiTietSymbolRepository repositoryThoiTietSymbol)
        {
            _logger = logger;
            _repositoryThoiTiet = repositoryThoiTiet;
            _repositoryThoiTietSymbol = repositoryThoiTietSymbol;
        }

        public async Task Execute(IJobExecutionContext context)
        {

            try
            {
                _logger.LogInformation($"Thông báo: \n Thời gian bắt đầu đồng bộ {DateTime.Now} and JobType: {context.JobDetail.JobType}");

                await _repositoryThoiTietSymbol.GetDataSymbol();

                await _repositoryThoiTiet.GetData();

                _logger.LogInformation($"Thông báo: \n Thời gian kết thúc đồng bộ {DateTime.Now} and JobType: {context.JobDetail.JobType}");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Thông báo: \n Đồng bộ thất bại: {DateTime.Now} and JobType: {context.JobDetail.JobType}");
            }
        }
    }
}
