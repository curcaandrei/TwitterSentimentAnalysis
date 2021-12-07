using MediatR;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Application.Commands.UpdateTweet
{
    public class UpdateTweetCommand : IRequest<UpdateResult>
    {
        public ObjectId Id { get; set; }
        public  System.Collections.Generic.Dictionary<string, float> Feels { get; set; }
        
        public UpdateTweetCommand(string id, System.Collections.Generic.Dictionary<string, float> feels)
        {
            this.Feels = feels;
            this.Id = ObjectId.Parse(id);
        }    
    }
}