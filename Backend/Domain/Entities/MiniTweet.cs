using Domain.Common;

namespace Domain.Entities
{
    public class MiniTweet : BaseEntity
    {
        public string tweetId { get; set; } = "";
        public string text { get; set; } = "";
        
        public string userId { get; set; } = "";
    }
}