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

        private readonly IMlRepository _repository;

        public PredictTweetSentimentQueryHandler(IMlRepository repository)
        {
            _repository = repository;
        }
        
        public async Task<Dictionary<string, float>> Handle(PredictTweetSentimentQuery request, CancellationToken cancellationToken)
        {
            return await _repository.PredictSentiment(request.Text);
        }
    }
}