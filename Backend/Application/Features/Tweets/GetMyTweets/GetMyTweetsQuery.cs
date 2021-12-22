using Domain.Entities;
using MediatR;

namespace Application.Features.Tweets.GetMyTweets
{
    public class GetMyTweetsQuery: IRequest<System.Collections.Generic.List<MiniTweet>>
    {
        public string userId { get; set; }

        public GetMyTweetsQuery(string user)
        {
            userId = user;
        }
    }
}