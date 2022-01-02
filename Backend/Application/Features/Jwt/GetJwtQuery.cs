using System;
using Domain.Entities;
using MediatR;

namespace Application.Features.Jwt
{
    public class GetJwtQuery : IRequest<Object>
    {
        public TweetSerializer Data { get; set; }

        public GetJwtQuery(TweetSerializer data)
        {
            Data = data;
        }
    }
}