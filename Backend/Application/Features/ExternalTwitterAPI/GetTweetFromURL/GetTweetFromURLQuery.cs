using Domain.Entities;
using MediatR;

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