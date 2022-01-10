using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Persistence;
using Domain.Dtos;
using MediatR;

namespace Application.Features.RequestTweet
{
    public class GetAllRequestsQueryHandler: IRequestHandler<GetAllRequestsQuery, System.Collections.Generic.List<TweetDto>>
    {
        private readonly IRequestTweetRepository _repository;
        
        public GetAllRequestsQueryHandler(IRequestTweetRepository repository)
        {
            _repository = repository;
        }
        
        public async Task<List<TweetDto>> Handle(GetAllRequestsQuery request, CancellationToken cancellationToken)
        {
            var allTweets = await _repository.ListAllAsync();
            return System.Linq.Enumerable.ToList(allTweets);
        }
    }
}