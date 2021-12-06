using System.Threading;
using System.Threading.Tasks;
using Application.Persistence;
using MediatR;

namespace Application.Features.ExternalTwitterAPI.LogInUser.GetTwitterAuth
{
    public class GetTwitterAuthQueryHandler : IRequestHandler<GetTwitterAuthQuery, string>
    {
        private readonly IExternalTweetRepository _tweetRepository;

        public GetTwitterAuthQueryHandler(IExternalTweetRepository tweetRepository)
        {
            _tweetRepository = tweetRepository;
        }

        public async Task<string> Handle(GetTwitterAuthQuery request, CancellationToken cancellationToken)
        {
            return await _tweetRepository.PostToGetAuth();
        }
    }
}