using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HangFire.BackgroungJob.Service
{
    public interface IBackgroundJob
    {
        Task ExcuteAsync();
    }
}
