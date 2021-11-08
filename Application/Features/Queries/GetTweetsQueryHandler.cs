using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Features.Queries
{
    public class GetTweetsQueryHandler : IRequestHandler<GetAllTweetsQuery, IEnumerable<Tweet>>
    {
        private readonly ITwitterRepository repository;

        public GetTweetsQueryHandler(ITwitterRepository repository)
        {
            this.repository = repository;
        }

        public async Task<IEnumerable<Tweet>> Handle(GetAllTweetsQuery request, CancellationToken cancellationToken)
        {
            return await repository.GetAllAsync();
        }
    }
}