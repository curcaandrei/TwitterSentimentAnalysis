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
    public class GetTweetListQueryHandler : IRequestHandler<GetTweetListQuery, List<TweetListVm>>
    {
        private readonly IRepository<Tweet> _tweetRepository;
        private readonly IMapper _mapper;

        public GetTweetListQueryHandler(IMapper mapper, IRepository<Tweet> repository)
        {
            _tweetRepository = repository;
            _mapper = mapper;
        }
        
        public async Task<List<TweetListVm>> Handle(GetTweetListQuery request, CancellationToken cancellationToken)
        {
            GetTweetListResponse response = new GetTweetListResponse();
            
            var allTweets = ( await Task.FromResult(_tweetRepository.SelectAll())).OrderBy(x => x.Id);

            if (allTweets != null)
            {
                response.Tweets = _mapper.Map<TweetListVm>(allTweets);
            }
            else
            {
                response.Success = false;
                response.Message = "List of Tweets is null";
            }
            return _mapper.Map<List<TweetListVm>>(allTweets);
        }
    }
}