using System.Collections.Generic;
using Domain.Entities;
using MediatR;

namespace Application.Features.Tweets
{
    public class GetTweetsQuery : IRequest<List<Tweet>>
    {
        
    }
}