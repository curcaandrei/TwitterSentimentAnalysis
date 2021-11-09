using System.Collections.Generic;
using MediatR;

namespace Application.Features.Tweets.Queries
{
    public class GetTweetListQuery : IRequest<List<TweetListVm>>
    {
        
    }
}