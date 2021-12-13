using System.Diagnostics.CodeAnalysis;
using Application.Commands.CreateTweet;
using AutoMapper;
using Domain.Entities;
using Tweetinvi.Models.V2;

namespace Application.Profiles
{
    [ExcludeFromCodeCoverage]
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Tweet, CreateTweetCommand>().ReverseMap();
            CreateMap<Tweet, TweetV2>().ReverseMap();
        }
    }
}