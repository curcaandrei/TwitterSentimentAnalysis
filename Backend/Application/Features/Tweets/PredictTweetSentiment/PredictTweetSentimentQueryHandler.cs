using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Features.Tweets.GetOneTweet;
using Application.Persistence;
using Domain.Dtos;
using Domain.Entities;
using MediatR;

namespace Application.Features.Tweets.PredictTweetSentiment
{
    public class PredictTweetSentimentQueryHandler : IRequestHandler<PredictTweetSentimentQuery, Dictionary<string, float>>
    {
        
        private readonly IAsyncRepository<Tweet> _repository;

        public PredictTweetSentimentQueryHandler(IAsyncRepository<Tweet> repository)
        {
            _repository = repository;
        }
        
        public async Task<Dictionary<string, float>> Handle(PredictTweetSentimentQuery request, CancellationToken cancellationToken)
        {
            return await _repository.PredictSentiment(request.Text);
        }
    }
}