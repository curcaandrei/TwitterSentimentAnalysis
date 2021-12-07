using Domain.Entities;
using MediatR;
using Tweetinvi.Models.V2;

namespace Application.Features.ExternalTwitterAPI.GetTweetFromURL
{
    public class GetTweetFromUrlQuery : IRequest<Tweet>
    {
        public string Id { get; set; }
        
        public GetTweetFromUrlQuery(string id)
        {
            this.Id = id;
        }
    }
}