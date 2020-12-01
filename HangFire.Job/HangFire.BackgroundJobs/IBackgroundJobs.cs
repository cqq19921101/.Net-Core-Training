using Abp.Dependency;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HangFire.BackgroundJobs
{
    /// <summary>
    /// Async Excute Task
    /// </summary>
    public interface IBackgroundJobs : ITransientDependency
    {
        /// <summary>
        /// Excute Task
        /// </summary>
        /// <returns></returns>
        Task ExcuteAsync();
    }
}
