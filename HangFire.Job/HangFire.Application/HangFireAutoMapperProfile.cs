using AutoMapper;
using HangFire.Application.Contracts.FaceImageApi;

namespace HangFire.Application
{
    public class HangFireAutoMapperProfile : Profile
    {
        public HangFireAutoMapperProfile()
        {
            CreateMap<Domain.FaceImage.FaceImageApi, FaceImageApiDto>();

        }
    }
}