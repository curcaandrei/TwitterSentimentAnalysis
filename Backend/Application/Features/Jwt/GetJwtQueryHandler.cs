using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Persistence;
using Domain.Entities;
using MediatR;

namespace Application.Features.Jwt
{
    public class GetJwtQueryHandler : IRequestHandler<GetJwtQuery, Object>
    {
        private readonly IAsyncRepository<UserRole> _repository;

        public GetJwtQueryHandler(IAsyncRepository<UserRole> repository)
        {
            _repository = repository;
        }

        public Task<object> Handle(GetJwtQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_repository.GetJwtToken(request.Data));
        }
    }
}