using Domain.Dtos;
using Domain.Entities;
using MediatR;

namespace Application.Features.RequestTweet
{
    public class RequestToAddTweetQuery: IRequest<TweetDto>
    {
        public TweetDto Tweet;

        public RequestToAddTweetQuery(TweetDto tweet)
        {
            Tweet = tweet;
        }

        public RequestToAddTweetQuery()
        {
            
        }
    }
}