using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Persistence;
using Domain.Entities;
using MediatR;

namespace Application.Features.Tweets.GetAllTweets
{
    public class GetTweetsQueryHandler : IRequestHandler<GetTweetsQuery, List<Tweet>>
    {
        private readonly IAsyncRepository<Tweet> _repository;

        public GetTweetsQueryHandler(IAsyncRepository<Tweet> repository)
        {
            _repository = repository;
        }

        public async Task<List<Tweet>> Handle(GetTweetsQuery request, CancellationToken cancellationToken)
        {
            var allTweets = await _repository.ListAllAsync(request.PageNr);
            return allTweets.ToList();
        }
    }
}