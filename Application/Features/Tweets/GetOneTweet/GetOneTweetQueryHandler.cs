using System.Threading;
using System.Threading.Tasks;
using Application.Persistence;
using Domain.Entities;
using MediatR;

namespace Application.Features.Tweets.GetOneTweet
{
    public class GetOneTweetQueryHandler : IRequestHandler<GetOneTweetQuery, Tweet>
    {
        private readonly ITweetsRepository _repository;

        public GetOneTweetQueryHandler(ITweetsRepository repository)
        {
            _repository = repository;
        }

        public async Task<Tweet> Handle(GetOneTweetQuery request, CancellationToken cancellationToken)
        {
            var tweet = await _repository.GetByIdAsync(request.Id);
            return tweet;
        }
    }
}