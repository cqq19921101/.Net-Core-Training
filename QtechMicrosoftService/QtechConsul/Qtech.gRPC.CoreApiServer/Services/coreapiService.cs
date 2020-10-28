using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.Extensions.Logging;

namespace Qtech.gRPC.CoreApiServer
{
    public class coreapiService : coreapi.coreapiBase
    {
        private readonly ILogger<coreapiService> _logger;
        public coreapiService(ILogger<coreapiService> logger)
        {
            _logger = logger;
        }

        public override Task<coreapiReply> GetValue(coreapiRequest request, ServerCallContext context)
        {
            return Task.FromResult(new coreapiReply()
            {
                Corepara = new coreapiReply.Types.coreapiModel()
                {
                    Id = request.Id,
                    Name = "cqq",
                    Remark = "cqq Remark"
                }
            });
        }
    }
}
