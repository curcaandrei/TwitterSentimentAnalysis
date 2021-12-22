using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Persistence;
using Domain.Entities;
using MediatR;

namespace Application.Features.Tweets.GetMyTweets
{
    public class GetMyTweetsQueryHandler:  IRequestHandler<GetMyTweetsQuery, System.Collections.Generic.List<MiniTweet>>
    {
        private readonly IAsyncRepository<MiniTweet> _repository;

        public GetMyTweetsQueryHandler(IAsyncRepository<MiniTweet> repository)
        {
            _repository = repository;
        }
        
        public Task<List<MiniTweet>> Handle(GetMyTweetsQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_repository.GetMyTweets(request.userId));
        }
    }
}