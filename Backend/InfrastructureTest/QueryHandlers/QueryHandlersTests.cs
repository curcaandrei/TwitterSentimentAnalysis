using System.Collections.Generic;
using Application.Commands.CreateTweet;
using Application.Commands.DeleteTweet;
using Application.Commands.RequestTweet;
using Application.Commands.UpdateTweet;
using Application.Features.ExternalTwitterAPI.GetTweetFromURL;
using Application.Features.ExternalTwitterAPI.LogInUser.GetTwitterAuth;
using Application.Features.Jwt;
using Application.Features.RequestTweet;
using Application.Features.Tweets.GetAllTweets;
using Application.Features.Tweets.GetOneTweet;
using Application.Features.Tweets.PredictTweetSentiment;
using Application.Persistence;
using AutoMapper;
using Domain.Entities;
using FakeItEasy;
using Microsoft.Extensions.Options;
using Moq;
using Persistence.MongoDb;
using Persistence.Repositories;
using Persistence.TwitterExternalAPI;
using Xunit;

namespace InfrastructureTest.QueryHandlers
{
    public class QueryHandlersTests
    {
        protected readonly Mock<BaseRepository<Tweet>> _baseRepository;
        protected readonly Mock<MlRepository> _mlRepository;
        protected readonly Mock<TweetRepository> _tweetsRepository;
        protected readonly Mock<ExternalTwitterRepository> _externalTwitterRepository;
        protected readonly Mock<RequestTweetRepository> _requestTweetRepository;
        private readonly Mock<BaseRepository<UserRole>> _asyncRepository;

        protected Tweet _tweet;
        protected readonly Mock<TwitterHelper> _twitterHelper;
        protected readonly Mock<TwitterSettings> _settings;
        
        protected readonly Mock<MongoDbContext> _mockContext;
        protected readonly Mock<MongoSettings> _mongoSettings;

        protected readonly IMapper _mapper;
        public QueryHandlersTests()
        {
            _settings = new Mock<TwitterSettings>("DycmxCAZlwwx2b5eEflU5Sl1w",
                "jJr4emCb35iQPPB8WdTaJPsbfidZvCED6jpUxdQf3T4r6z5Qs0",
                "995542379410153472-8tuUoglasaaQw1O95njkv9b44E6pjy0", "U4vnZTfaUqoeztJaHJzZ6IYm94qt9Dand2S7Ew45VlpZa");
            IOptions<TwitterSettings> options = Options.Create(_settings.Object);
            _twitterHelper = new Mock<TwitterHelper>(options);
            
            _mongoSettings = new Mock<MongoSettings>("mongodb+srv://admin:admin@proiectdotnet.8hto9.mongodb.net/TwitterDB?retryWrites=true&w=majority","TwitterDB");
            IOptions<MongoSettings> mongoOptions = Options.Create(_mongoSettings.Object);
            _mockContext = new Mock<MongoDbContext>(mongoOptions);
            _tweet = new Tweet();
            
            _externalTwitterRepository = new Mock<ExternalTwitterRepository>(_twitterHelper.Object);
            _baseRepository = new Mock<BaseRepository<Tweet>>(_mockContext.Object);
            _mlRepository = new Mock<MlRepository>();
            _tweetsRepository = new Mock<TweetRepository>(_mockContext.Object);
            _requestTweetRepository = new Mock<RequestTweetRepository>(_mockContext.Object);
            _asyncRepository = new Mock<BaseRepository<UserRole>>(_mockContext.Object);
            _mapper = A.Fake<IMapper>();
        }
        
        
        [Fact]
        public void CreateUpdateAndDeleteTweetHandlerTest()
        {
            var createCommand = new CreateTweetCommand();
            var createHandler = new CreateTweetCommandHandler(_tweetsRepository.Object,_mapper);
            var createRes = createHandler.Handle(createCommand, default);
            Assert.NotNull(createRes.Result.ToString());

            var updateCmd = new UpdateTweetCommand(createRes.Result.ToString(), new Dictionary<string, float>());
            var updateHandler = new UpdateTweetCommandHandler(_tweetsRepository.Object);
            var updateRes = updateHandler.Handle(updateCmd, default);
            Assert.Equal(1,updateRes.Result.ModifiedCount);

            var deleteCmd = new DeleteTweetCommand(createRes.Result.ToString());
            var deleteHandler = new DeleteTweetCommandHandler(_tweetsRepository.Object);
            var deleteRes = deleteHandler.Handle(deleteCmd, default);
            Assert.Equal(1, deleteRes.Result.DeletedCount);
        }

        [Fact]
        public void GetTweetHandlerTest()
        {
            var getFromUrlCmd = new GetTweetFromUrlQuery("1462843218446503940");
            var getFromUrlHandler = new GetTweetFromUrlQueryHandler(_externalTwitterRepository.Object);
            var res = getFromUrlHandler.Handle(getFromUrlCmd, default);
            Assert.NotNull(res.Result);
        }

        [Fact]
        public void GetUrlAuthHandlerTest()
        {
            var cmd = new GetTwitterAuthQuery();
            var handler = new GetTwitterAuthQueryHandler(_externalTwitterRepository.Object);
            var res = handler.Handle(cmd, default);
            Assert.NotNull(res.Result);
        }

        [Fact]
        public void GetPageHandlerTest()
        {
            var cmd = new GetTweetsQuery(1);
            var handler = new GetTweetsQueryHandler(_baseRepository.Object);
            var res = handler.Handle(cmd, default);
            
            Assert.Equal(10, res.Result.Count);
        }

        [Fact]
        public void GetOneHandlerTest()
        {
            var cmd = new GetOneTweetQuery("619b7c337c3468160b0021c1");
            var handle = new GetOneTweetQueryHandler(_tweetsRepository.Object);

            var res = handle.Handle(cmd, default);
            
            Assert.NotNull(res.Result);
        }

        [Fact]
        public void PredictHandlerTest()
        {
            var cmd = new PredictTweetSentimentQuery("I'm so sad today");
            var handler = new PredictTweetSentimentQueryHandler(_mlRepository.Object);

            var res = handler.Handle(cmd, default);
            
            Assert.True(res.Result["sad"] > res.Result["happy"]);
        }

        [Fact]
        public void RequestHandlerTest()
        {
            Tweet tweet = new Tweet();
            var cmd = new RequestToAddTweetQuery(tweet);
            var handler = new RequestToAddTweetQueryHandler(_requestTweetRepository.Object);

            var res = handler.Handle(cmd, default);

            var cmdDelete = new DeleteTweetRequestCommand(tweet.Id.ToString());
            var cmdHandler = new DeleteTweetRequestCommandHandler(_requestTweetRepository.Object);
            var resDelete = cmdHandler.Handle(cmdDelete, default);
            Assert.Equal(1, resDelete.Result.DeletedCount);

        }
        
        [Fact]
        public void GetAllRequestsQueryHandlerTest()
        {
            var cmd = new GetAllRequestsQuery();
            var handler = new GetAllRequestsQueryHandler(_requestTweetRepository.Object);

            var res = handler.Handle(cmd, default);
            
            Assert.True(res.IsCompleted);

        }
        
        [Fact]
        public void DeleteTweetRequestCommandTest()
        {
            var cmd = new DeleteTweetRequestCommand("61c3817ead0f3ee2a51be90d");
            var handler = new DeleteTweetRequestCommandHandler(_requestTweetRepository.Object);

            var res = handler.Handle(cmd, default);
            
            Assert.True(res.IsCompleted);

        }

        [Fact]
        public void GetJwtQueryTest()
        {
            TweetSerializer tweetSerializer = new TweetSerializer();
            tweetSerializer.userId = "@curcaandrei99";
            tweetSerializer.data = new List<MiniTweetDto>()
            {
                new MiniTweetDto()
                {
                    id = "1470666379263107072",
                    text = "I do not feel very good"
                },
                new MiniTweetDto()
                {
                    id = "1469987067379728389",
                    text = "i'm so sad today"
                }
            };
            var cmd = new GetJwtQuery(tweetSerializer);
            var handler = new GetJwtQueryHandler(_asyncRepository.Object);

            var res = handler.Handle(cmd, default);
            
            Assert.True(res.IsCompleted);
        }

    }
}