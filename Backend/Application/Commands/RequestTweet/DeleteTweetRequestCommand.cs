using MediatR;
using MongoDB.Driver;

namespace Application.Commands.RequestTweet
{
    public class DeleteTweetRequestCommand : IRequest<DeleteResult>
    {
        public string Id { get; set; }
        
        public DeleteTweetRequestCommand(string id)
        {
            Id = id;
        }        
    }
}