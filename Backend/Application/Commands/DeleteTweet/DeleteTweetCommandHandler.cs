using System.Threading;
using System.Threading.Tasks;
using Application.Persistence;
using Domain.Entities;
using MediatR;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Application.Commands.DeleteTweet
{
    public class DeleteTweetCommandHandler : IRequestHandler<DeleteTweetCommand, DeleteResult>
    {
        private readonly ITweetsRepository _repository;

        public DeleteTweetCommandHandler(ITweetsRepository repository)
        {
            _repository = repository;
        }

        public Task<DeleteResult> Handle(DeleteTweetCommand request, CancellationToken cancellationToken)
        {
            var tweet = _repository.DeleteAsync(request.Id.ToString());
            return Task.FromResult<DeleteResult>(tweet);
        }
    }
}