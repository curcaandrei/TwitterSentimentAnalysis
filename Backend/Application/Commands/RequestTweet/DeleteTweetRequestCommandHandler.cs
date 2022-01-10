using System.Threading;
using System.Threading.Tasks;
using Application.Persistence;
using MediatR;
using MongoDB.Driver;

namespace Application.Commands.RequestTweet
{
    public class DeleteTweetRequestCommandHandler : IRequestHandler<DeleteTweetRequestCommand, DeleteResult>
    {
        private readonly IRequestTweetRepository _repository;

        public DeleteTweetRequestCommandHandler(IRequestTweetRepository repository)
        {
            _repository = repository;
        }

        public Task<DeleteResult> Handle(DeleteTweetRequestCommand request, CancellationToken cancellationToken)
        {
            var tweet = _repository.DeleteAsync(request.Id);
            return Task.FromResult<DeleteResult>(tweet);
        }
    }
}