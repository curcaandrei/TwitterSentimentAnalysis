using System.Threading;
using System.Threading.Tasks;
using Application.Persistence;
using Domain;
using MediatR;

namespace Application.Commands.AcceptRequest
{
    public class AcceptRequestCommandHandler : IRequestHandler<AcceptRequestCommand, string>
    {
        private readonly IRequestTweetRepository _repository;

        public AcceptRequestCommandHandler(IRequestTweetRepository repository)
        {
            _repository = repository;
        }
        
        public Task<string> Handle(AcceptRequestCommand request, CancellationToken cancellationToken)
        {
            _repository.RetrainAlgorithm(request.Id);
            return Task.FromResult(request.Id);
        }
    }
}