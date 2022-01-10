using Domain.Dtos;
using Domain.Entities;
using MediatR;

namespace Application.Features.RequestTweet
{
    public class RequestToAddTweetQuery: IRequest<Tweet>
    {
        public Tweet Tweet;

        public RequestToAddTweetQuery(Tweet tweet)
        {
            // Tweet normal_tweet = new Tweet();
            // normal_tweet.Username = tweet.Username;
            // normal_tweet.User = tweet.User;
            // normal_tweet.feels = tweet.Feels;
            // normal_tweet.Date = tweet.Date;
            Tweet = tweet;
        }

        public RequestToAddTweetQuery()
        {
            
        }
    }
}