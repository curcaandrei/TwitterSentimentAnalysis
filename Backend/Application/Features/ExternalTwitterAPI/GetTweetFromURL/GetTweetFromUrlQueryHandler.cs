using System.Threading;
using System.Threading.Tasks;
using Application.Persistence;
using Domain.Entities;
using MediatR;

namespace Application.Features.ExternalTwitterAPI.GetTweetFromURL
{
    public class GetTweetFromUrlQueryHandler : IRequestHandler<GetTweetFromUrlQuery, Tweet>
    {
        private readonly IExternalTweetRepository _tweetRepository;
        public GetTweetFromUrlQueryHandler(IExternalTweetRepository tweetRepository)
        {
            _tweetRepository = tweetRepository;
        }

        public async Task<Tweet> Handle(GetTweetFromUrlQuery request, CancellationToken cancellationToken)
        {
            return await _tweetRepository.GetTweetById(request.Id);
        }
    }
}