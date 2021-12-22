using System.Collections.Generic;
namespace Domain.Entities
{
    public class TweetSerializer
    {
        public string userId { get; set; } = "";
        public List<MiniTweetDto> data { get; set; } = new List<MiniTweetDto>();
    }
}