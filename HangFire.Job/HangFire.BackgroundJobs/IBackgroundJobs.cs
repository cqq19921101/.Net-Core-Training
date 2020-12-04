using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

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
        Task ExecuteAsync();
    }
}
