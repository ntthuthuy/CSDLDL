using Microsoft.Extensions.Logging;
using Quartz;
using System;
using System.Threading.Tasks;
using TechLife.App.Areas.HueCIT.Interface;

namespace TechLife.App.Areas.HueCIT.Jobs
{
    [DisallowConcurrentExecution]
    public class VeDiTichJob : IJob
    {
        private readonly ILogger<VeDiTichJob> _logger;
        private readonly IVeDiTichRepository _vediTichRepository;
        private readonly IVeDiTichLoaiRepository _veDiTichLoaiRepository;
        private readonly IVeDiTichDiaDiemRepository _veDiTichDiaDiemRepository; 
        public VeDiTichJob(ILogger<VeDiTichJob> logger, 
                           IVeDiTichLoaiRepository veDiTichLoaiRepository, 
                           IVeDiTichDiaDiemRepository veDiTichDiaDiemRepository,
                           IVeDiTichRepository vediTichRepository)
        {
            _logger = logger;
            _veDiTichLoaiRepository = veDiTichLoaiRepository;
            _veDiTichDiaDiemRepository = veDiTichDiaDiemRepository;
            _vediTichRepository = vediTichRepository;
        }
        public async Task Execute(IJobExecutionContext context)
        {

            try
            {
                _logger.LogInformation($"Thong bao: Thoi gian bat dau dong bo {DateTime.Now} and JobType: {context.JobDetail.JobType}");

                await _veDiTichLoaiRepository.GetDataLoaiVe();

                await _veDiTichDiaDiemRepository.GetDataDiaDiem();

                await _vediTichRepository.GetDataVeDiTich();

                _logger.LogInformation($"Thong bao: Thoi gian ket thuc dong bo {DateTime.Now} and JobType: {context.JobDetail.JobType}");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Thông báo: \n Đồng bộ thất bại: {DateTime.Now} and JobType: {context.JobDetail.JobType}");
            }
        }
    }
}
