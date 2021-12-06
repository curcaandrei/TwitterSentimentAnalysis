using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Domain.Dtos
{
    public class TweetDTO
    {
        public string Id { get; set; }
        public string Date { get; set; }
        public string User { get; set; }
        
        [AllowNull]
        public string Username { get; set; }
        public string Text { get; set; }
        public Dictionary<string, float> feels { get; set; }
    }
}