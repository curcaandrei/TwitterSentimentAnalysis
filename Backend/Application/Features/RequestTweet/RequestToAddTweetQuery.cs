using Domain.Entities;
using MediatR;

namespace Application.Features.RequestTweet
{
    public class RequestToAddTweetQuery: IRequest<Tweet>
    {
        public readonly Tweet Tweet;

        public RequestToAddTweetQuery(Tweet tweet)
        {
            Tweet = tweet;
        }
    }
}