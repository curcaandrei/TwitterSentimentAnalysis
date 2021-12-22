using System;
using MediatR;

namespace Application.Features.Jwt
{
    public class GetJwtQuery : IRequest<Object>
    {
        public string Data { get; set; }

        public GetJwtQuery(string data)
        {
            Data = data;
        }
    }
}