using System.Collections.Generic;
using Domain.Entities;
using MediatR;

namespace Application.Features.Queries
{
    public class GetAllTweetsQuery : IRequest<IEnumerable<Tweet>>
    {
        
    }
}