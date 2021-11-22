using Application.Commands.CreateTweet;
using Application.Commands.UpdateTweet;
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