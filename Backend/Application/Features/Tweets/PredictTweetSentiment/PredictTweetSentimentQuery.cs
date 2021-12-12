using System.Collections.Generic;
using Domain.Dtos;
using MediatR;

namespace Application.Features.Tweets.PredictTweetSentiment
{
    public class PredictTweetSentimentQuery : IRequest<Dictionary<string, float>>
    {
        public string Text { get; set; }

        public PredictTweetSentimentQuery(string text)
        {
            Text = text;
        }
    }
}