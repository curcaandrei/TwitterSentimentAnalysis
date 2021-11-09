using System.Collections.Generic;
using Application.Features.Tweets.Queries;
using Domain.Entities;

namespace Application.Responses.Tweets
{
    public class GetTweetListResponse : BaseResponse
    {
        public GetTweetListResponse() : base()
        {
            
        }

        public List<Tweet> Tweets { get; set; }
    }
}