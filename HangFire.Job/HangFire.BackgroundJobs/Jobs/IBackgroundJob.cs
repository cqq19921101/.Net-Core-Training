﻿using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace HangFire.BackgroundJobs.Jobs
{
    public interface IBackgroundJob : ITransientDependency
    {
        /// <summary>
        /// 执行任务
        /// </summary>
        /// <returns></returns>
        Task ExecuteAsync();
    }
}