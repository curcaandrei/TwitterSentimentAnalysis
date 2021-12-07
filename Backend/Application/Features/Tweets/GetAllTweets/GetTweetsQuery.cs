using Domain.Dtos;
using MediatR;

namespace Application.Features.Tweets.GetAllTweets
{
    public class GetTweetsQuery : IRequest<System.Collections.Generic.List<TweetDTO>>
    {
        public int PageNr { get; set; }

        public GetTweetsQuery(int pageNr)
        {
            PageNr = pageNr;
        }
    }
}