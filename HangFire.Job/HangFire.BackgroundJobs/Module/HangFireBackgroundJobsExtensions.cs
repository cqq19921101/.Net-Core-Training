using Hangfire;
using HangFire.BackgroundJobs.Jobs.FaceImageApi;
using System;

namespace HangFire.BackgroundJobs.Module
{
    /// <summary>
    /// Generate Task Cron Type
    /// </summary>
    public static class HangFireBackgroundJobsExtensions
    {
        public static void ExcuteTest(this IServiceProvider service)
        {
            //var job = service.GetService<FaceImageApiJob>();

            //RecurringJob.AddOrUpdate("测试", () => job.ExecuteAsync(), HangFireCronType.Hour(1, 3));

        }
    }
}
