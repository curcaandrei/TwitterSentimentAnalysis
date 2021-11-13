using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Features.Tweets.Commands
{
    public class AddTweetCommand : IRequest<Guid>
    {
        public string Text { get; set; }
        
        public class AddTweetCommandHandler : IRequestHandler<AddTweetCommand, Guid>
        {
            private readonly IApplicationDbContext _dbContext;

            public AddTweetCommandHandler(IApplicationDbContext dbContext)
            {
                _dbContext = dbContext;
            }

            public async Task<Guid> Handle(AddTweetCommand request, CancellationToken cancellationToken)
            {
                Tweet tweet = new Tweet();
                tweet.Text = request.Text;
                _dbContext.Tweets.Add(tweet);
                await _dbContext.SaveChangesAsync();
                return tweet.Id;
            }
        }
    }
}