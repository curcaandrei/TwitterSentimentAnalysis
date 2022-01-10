using Domain.Dtos;
using Domain.Entities;
using MediatR;

namespace Application.Features.RequestTweet
{
    public class GetAllRequestsQuery: IRequest<System.Collections.Generic.List<TweetDto>>
    {
        
    }
}