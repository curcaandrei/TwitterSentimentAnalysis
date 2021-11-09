using System.Collections.Generic;
using Domain.Entities;
using MediatR;

namespace Application.Features.Tweets.Queries
{
    public class GetTweetListQuery : IRequest<List<Tweet>>
    {
        
    }
}