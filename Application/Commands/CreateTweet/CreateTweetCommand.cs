using System;
using MediatR;

namespace Application.Commands.CreateTweet
{
    public class CreateTweetCommand : IRequest<Guid>
    {
        public string Text { get; set; }
        
    }
}