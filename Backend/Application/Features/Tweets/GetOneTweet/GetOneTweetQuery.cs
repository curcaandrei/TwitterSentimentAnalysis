using Domain.Dtos;
using MediatR;
using MongoDB.Bson;

namespace Application.Features.Tweets.GetOneTweet
{
    public class GetOneTweetQuery : IRequest<TweetDto>
    {
        public ObjectId Id { get; set; }
        
        public GetOneTweetQuery(string id)
        {
            this.Id = ObjectId.Parse(id);
        }
    }
}