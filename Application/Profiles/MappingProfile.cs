using Application.Features.Tweets.Queries;
using AutoMapper;
using Domain.Entities;

namespace Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Tweet, TweetListVm>().ReverseMap();
        }
    }
}