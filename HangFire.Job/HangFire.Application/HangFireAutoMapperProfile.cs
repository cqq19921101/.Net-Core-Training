using AutoMapper;
using HangFire.Application.Contracts.FaceImageApi;

namespace HangFire.Application
{
    /// <summary>
    /// AutoMapper
    /// </summary>
    public class HangFireAutoMapperProfile : Profile
    {
        /// <summary>
        /// Create Entity Mapping
        /// </summary>
        public HangFireAutoMapperProfile()
        {
            //Face Image Interface Entity
            CreateMap<Domain.FaceImage.FaceImageApi, FaceImageApiDto>();
        }
    }
}
