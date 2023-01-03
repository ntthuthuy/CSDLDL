using System;

namespace TechLife.App.Areas.HueCIT.Schedules
{
    public class JobSchedule
    {
        public string JobId { get; set; }
        public Type JobType { get; }
        public string JobName { get; }
        public string CronExpression { get; }
        public JobSchedule(string Id, Type jobType, string jobName, string cronExpression)
        {
            JobId = Id;
            JobType = jobType;
            JobName = jobName;
            CronExpression = cronExpression;
        }
    }
}
