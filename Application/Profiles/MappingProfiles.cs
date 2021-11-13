using Application.Commands.CreateTweet;
using AutoMapper;
using Domain.Entities;

namespace Application.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Tweet, CreateTweetCommand>().ReverseMap();
        }
    }
}