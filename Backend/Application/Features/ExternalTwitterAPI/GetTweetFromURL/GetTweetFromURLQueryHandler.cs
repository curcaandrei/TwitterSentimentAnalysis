using System.Threading;
using System.Threading.Tasks;
using Application.Persistence;
using Domain.Entities;
using MediatR;
using Tweetinvi.Models.V2;

namespace Application.Features.ExternalTwitterAPI.GetTweetFromURL
{
    public class GetTweetFromURLQueryHandler : IRequestHandler<GetTweetFromURLQuery, Tweet>
    {
        private readonly IExternalTweetRepository _tweetRepository;
        public GetTweetFromURLQueryHandler(IExternalTweetRepository tweetRepository)
        {
            _tweetRepository = tweetRepository;
        }

        public async Task<Tweet> Handle(GetTweetFromURLQuery request, CancellationToken cancellationToken)
        {
            return await _tweetRepository.GetTweetById(request.Id);
        }
    }
}