using System.Diagnostics.CodeAnalysis;
using MediatR;
using MongoDB.Bson;

namespace Application.Commands.CreateTweet
{
    public class CreateTweetCommand : IRequest<ObjectId>
    {
        public string Text { get; set; } = "No Text";
        public string User { get; set; } = "No user";
        public string Date { get; set; } = "No Date";

        [AllowNull]
        public System.Collections.Generic.Dictionary<string, float> Feels { get; set; }
    }
}