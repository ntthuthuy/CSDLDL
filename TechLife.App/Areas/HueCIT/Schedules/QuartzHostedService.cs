using Microsoft.Extensions.Hosting;
using Quartz;
using Quartz.Spi;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace TechLife.App.Areas.HueCIT.Schedules
{
    public class QuartzHostedService : IHostedService
    {
        private readonly ISchedulerFactory _schedulerFactory;
        private readonly IJobFactory _jobFactory;
        private readonly IEnumerable<JobSchedule> _jobSchedules;

        public QuartzHostedService(ISchedulerFactory schedulerFactory,
                                   IJobFactory jobFactory,
                                   IEnumerable<JobSchedule> jobSchedules)
        {
            _schedulerFactory = schedulerFactory;
            _jobFactory = jobFactory;
            _jobSchedules = jobSchedules;
        }

        public IScheduler Scheduler { get; set; }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            Scheduler = await _schedulerFactory.GetScheduler(cancellationToken);
            Scheduler.JobFactory = _jobFactory;

            await Scheduler.Start(cancellationToken);

            foreach (var _jobSchedule in _jobSchedules)
            {
                var job = CreateJob(_jobSchedule);
                var trigger = CreateTrigger(_jobSchedule);

                await Scheduler.ScheduleJob(job, trigger, cancellationToken);
            }
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await Scheduler?.Shutdown(cancellationToken);
        }

        private static IJobDetail CreateJob(JobSchedule jobSchedule)
        {
            return JobBuilder
                .Create(jobSchedule.JobType)
                .WithIdentity(jobSchedule.JobId + "_Job", "Automatic")
                .WithDescription(jobSchedule.JobName)
                .Build();
        }
        private static ITrigger CreateTrigger(JobSchedule jobSchedule)
        {
            return TriggerBuilder
                .Create()
                .WithIdentity(jobSchedule.JobId + "_Trigger", "Automatic")
                .WithCronSchedule(jobSchedule.CronExpression)
                .WithDescription(jobSchedule.JobName)
                .Build();
        }
    }
}
