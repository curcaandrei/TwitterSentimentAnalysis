using Application.Features.Tweets.Queries;

namespace Application.Responses.Tweets
{
    public class GetTweetListResponse : BaseResponse
    {
        public GetTweetListResponse() : base()
        {
            
        }

        public TweetListVm Tweets { get; set; }
    }
}