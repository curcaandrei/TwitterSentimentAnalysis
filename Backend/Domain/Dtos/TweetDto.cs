using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Domain.Dtos
{
    public class TweetDto
    {
        public string Id { get; set; } = "No id";
        public string Date { get; set; } = "No date";
        public string User { get; set; } = "No user";
        
        [AllowNull]
        public string Username { get; set; }

        public string Text { get; set; } = "No text";
        
        [AllowNull]
        public Dictionary<string, float> Feels { get; set; }
    }
}