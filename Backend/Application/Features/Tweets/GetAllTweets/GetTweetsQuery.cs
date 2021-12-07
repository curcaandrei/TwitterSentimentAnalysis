using Domain.Dtos;
using MediatR;

namespace Application.Features.Tweets.GetAllTweets
{
    public class GetTweetsQuery : IRequest<System.Collections.Generic.List<TweetDto>>
    {
        public int PageNr { get; set; }

        public GetTweetsQuery(int pageNr)
        {
            PageNr = pageNr;
        }
    }
}