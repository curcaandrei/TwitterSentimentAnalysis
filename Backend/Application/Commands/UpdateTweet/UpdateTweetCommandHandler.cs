using System.Threading;
using System.Threading.Tasks;
using Application.Persistence;
using MediatR;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Application.Commands.UpdateTweet
{
    public class UpdateTweetCommandHandler : IRequestHandler<UpdateTweetCommand, UpdateResult>
    {
        private readonly ITweetsRepository _tweetsRepository;

        public UpdateTweetCommandHandler(ITweetsRepository tweetsRepository)
        {
            _tweetsRepository = tweetsRepository;
        }

        public Task<UpdateResult> Handle(UpdateTweetCommand request, CancellationToken cancellationToken)
        {
            var t = _tweetsRepository.UpdateAsync(request.Id.ToString(), request.Feels);
            return Task.FromResult<UpdateResult>(t);
        }
    }
}