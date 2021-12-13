using MediatR;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Application.Commands.DeleteTweet
{
    public class DeleteTweetCommand : IRequest<DeleteResult>
    {
        public ObjectId Id { get; set; }
        
        public DeleteTweetCommand(string id)
        {
            this.Id = ObjectId.Parse(id);
        }        
    }
}