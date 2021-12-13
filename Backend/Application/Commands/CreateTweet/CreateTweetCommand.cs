using System.Collections.Generic;
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
        public Dictionary<string, float> Feels { get; set; }

        public CreateTweetCommand(string text, string user, string date, Dictionary<string, float> feels)
        {
            Text = text;
            User = user;
            Date = date;
            Feels = feels;
        }

        public CreateTweetCommand()
        {
            
        }
    }
}