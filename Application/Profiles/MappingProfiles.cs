using Application.Commands.CreateTweet;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;

namespace Application.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Tweet, CreateTweetCommand>().ReverseMap();
            CreateMap<Feelings, string>().ReverseMap();
        }
    }
}