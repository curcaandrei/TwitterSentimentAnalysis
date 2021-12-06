using System.Collections.Generic;
using Domain.Entities;
using MediatR;

namespace Application.Features.Tweets.GetAllTweets
{
    public class GetTweetsQuery : IRequest<List<Tweet>>
    {
        public int PageNr { get; set; }

        public GetTweetsQuery(int pageNr)
        {
            PageNr = pageNr;
        }
    }
}