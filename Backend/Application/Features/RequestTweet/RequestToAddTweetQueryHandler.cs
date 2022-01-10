using System.Threading;
using System.Threading.Tasks;
using Application.Persistence;
using Domain.Dtos;
using Domain.Entities;
using MediatR;

namespace Application.Features.RequestTweet
{
    public class RequestToAddTweetQueryHandler : IRequestHandler<RequestToAddTweetQuery, TweetDto>
    {
        private readonly IRequestTweetRepository _repository;
        
        public RequestToAddTweetQueryHandler(IRequestTweetRepository repository)
        {
            _repository = repository;
        }
        
        public Task<TweetDto> Handle(RequestToAddTweetQuery request, CancellationToken cancellationToken)
        {
            return _repository.AddAsync(request.Tweet);
        }
    }
}