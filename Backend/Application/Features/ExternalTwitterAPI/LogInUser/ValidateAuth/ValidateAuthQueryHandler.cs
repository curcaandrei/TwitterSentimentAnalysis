using System.Threading;
using System.Threading.Tasks;
using Application.Persistence;
using MediatR;

namespace Application.Features.ExternalTwitterAPI.LogInUser.ValidateAuth
{
    public class ValidateAuthQueryHandler : IRequestHandler<ValidateAuthQuery, string>
    {
        private readonly IExternalTweetRepository _tweetRepository;

        public ValidateAuthQueryHandler(IExternalTweetRepository tweetRepository)
        {
            _tweetRepository = tweetRepository;
        }

        public async Task<string> Handle(ValidateAuthQuery request, CancellationToken cancellationToken)
        {
            return await _tweetRepository.ValidateAuth(request.QueryStringValue);
        }
    }
}