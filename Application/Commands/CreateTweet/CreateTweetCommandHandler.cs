using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Persistence;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Commands.CreateTweet
{
    public class CreateTweetCommandHandler : IRequestHandler<CreateTweetCommand, Guid>
    {
        private readonly ITweetsRepository _tweetsRepository;
        private readonly IMapper _mapper;
        public CreateTweetCommandHandler(ITweetsRepository tweetsRepository, IMapper mapper)
        {
            _tweetsRepository = tweetsRepository;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(CreateTweetCommand request, CancellationToken cancellationToken)
        {
            var @tweet = _mapper.Map<Tweet>(request);
            @tweet = await _tweetsRepository.AddAsync(@tweet);
            return @tweet.Id;
        }
    }
}