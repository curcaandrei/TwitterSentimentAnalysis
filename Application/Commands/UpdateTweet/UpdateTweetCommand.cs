using System.Collections.Generic;
using Domain.Entities;
using MediatR;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Application.Commands.UpdateTweet
{
    public class UpdateTweetCommand : IRequest<UpdateResult>
    {
        public ObjectId Id { get; set; }
        public  Dictionary<string, float> t { get; set; }
        
        public UpdateTweetCommand(string id, Dictionary<string, float> t)
        {
            this.t = t;
            this.Id = ObjectId.Parse(id);
        }    
    }
}