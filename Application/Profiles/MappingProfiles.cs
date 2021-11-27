using Application.Commands.CreateTweet;
using Application.Commands.UpdateTweet;
using Application.Features.ExternalTwitterAPI.GetTweetFromURL;
using AutoMapper;
using Domain.Entities;
using Tweetinvi.Core.DTO;
using Tweetinvi.Models.V2;

namespace Application.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Tweet, CreateTweetCommand>().ReverseMap();
            CreateMap<Tweet, TweetV2>().ReverseMap();
        }
    }
}