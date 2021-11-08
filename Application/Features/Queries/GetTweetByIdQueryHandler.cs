using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Features.Queries
{
    public class GetTweetByIdQueryHandler : IRequestHandler<GetTweetByIdQuery, Tweet>
    {
        private readonly ITwitterRepository repository;
        
        public GetTweetByIdQueryHandler(ITwitterRepository repository)
        {
            this.repository = repository;
        }
        
        public async Task<Tweet> Handle(GetTweetByIdQuery request, CancellationToken cancellationToken)
        {
            var tweet = await repository.GetByIdAsync(request.Id);
            if (tweet == null)
            {
                throw new Exception("Tweet doesn't exist");
            }

            return tweet;
        }
    }
}