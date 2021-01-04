using Hangfire;
using HangFire.BackgroundJobs.Jobs.FaceImageApi;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace HangFire.BackgroundJobs
{
    public static class HangFireBackgroundJobsExtensions
    {

        #region Test
        public static void UseTestMailJob(this IServiceProvider service)
        {
            var job = service.GetService<TestSendMail>();

            RecurringJob.AddOrUpdate("Cqq SendMail 测试", () => job.ExecuteAsync(), HangFireCronType.Minute(3));
        }

        #endregion

        #region FaceImage Api Job
        /// <summary>
        /// All Employee
        /// </summary>
        /// <param name="service"></param>
        public static void UseFaceImageApiJob_GetAllEmployee(this IServiceProvider service)
        {
            var job = service.GetService<GetAllEmployeeJob_FaceImageApi>();

            RecurringJob.AddOrUpdate("旷视人脸接口-同步全厂员工", () => job.ExecuteAsync(), HangFireCronType.Hour(5,5));
        }

        /// <summary>
        /// New Employee 
        /// </summary>
        /// <param name="service"></param>
        public static void UseFaceImageApiJob_GetNewEmployee(this IServiceProvider service)
        {
            var job = service.GetService<GetNewEmployeeJob_FaceImageApi>();

            RecurringJob.AddOrUpdate("旷视人脸接口-同步新入职员工", () => job.ExecuteAsync(), HangFireCronType.Hour(5,5));
        }

        /// <summary>
        /// Resigned Employee
        /// </summary>
        /// <param name="service"></param>
        public static void UseFaceImageApiJob_GetResignedEmployee(this IServiceProvider service)
        {
            var job = service.GetService<GetResignedEmployeeJob_FaceImageApi>();

            RecurringJob.AddOrUpdate("旷视人脸接口-同步离职员工", () => job.ExecuteAsync(), HangFireCronType.Hour(5,5));
        }

        /// <summary>
        /// Updated Employee
        /// </summary>
        /// <param name="service"></param>
        public static void UseFaceImageApiJob_GetUpdatedEmployee(this IServiceProvider service)
        {
            var job = service.GetService<GetUpdatedEmployeeJob_FaceImageApi>();

            RecurringJob.AddOrUpdate("旷视人脸接口-同步更新过资料员工", () => job.ExecuteAsync(), HangFireCronType.Hour(5,5));
        }

        #endregion

    }
}