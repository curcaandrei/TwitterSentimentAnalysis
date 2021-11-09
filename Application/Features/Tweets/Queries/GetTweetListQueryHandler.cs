using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using Application.Responses.Tweets;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Tweets.Queries
{
    public class GetTweetListQueryHandler : IRequestHandler<GetTweetListQuery, List<Tweet>>
    {
        private readonly IRepository<Tweet> _tweetRepository;

        public GetTweetListQueryHandler(IRepository<Tweet> repository)
        {
            _tweetRepository = repository;
        }
        
        public async Task<List<Tweet>> Handle(GetTweetListQuery request, CancellationToken cancellationToken)
        {
            GetTweetListResponse response = new GetTweetListResponse();
            
            var allTweets = ( await Task.FromResult(_tweetRepository.SelectAll())).OrderBy(x => x.Id);

            if (allTweets != null)
            {
                response.Tweets = allTweets.ToList();
            }
            else
            {
                response.Success = false;
                response.Message = "List of Tweets is null";
            }
            return allTweets.ToList();
        }
    }
}