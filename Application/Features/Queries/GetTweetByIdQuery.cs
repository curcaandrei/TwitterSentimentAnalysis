using System;
using Domain.Entities;
using MediatR;

namespace Application.Features.Queries
{
    public class GetTweetByIdQuery : IRequest<Tweet>
    {
        public Guid Id { get; set; }
    }
}