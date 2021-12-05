using Domain.Entities;
using MediatR;
using Tweetinvi.Models.V2;

namespace Application.Features.ExternalTwitterAPI.GetTweetFromURL
{
    public class GetTweetFromURLQuery : IRequest<Tweet>
    {
        public string Id { get; set; }
        
        public GetTweetFromURLQuery(string id)
        {
            this.Id = id;
        }
    }
}