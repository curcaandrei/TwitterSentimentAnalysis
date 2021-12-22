using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Application.Persistence;
using Domain.Dtos;
using Domain.Entities;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json;
using Persistence.MongoDb;

namespace Persistence.Repositories
{
    public class BaseRepository<T> : MlRepository, IAsyncRepository<T> where T : class
    {
        private readonly IMongoDbContext _context;
        public BaseRepository(IMongoDbContext dbContext)
        {
            _context = dbContext;
        }
        
        public async Task<List<TweetDto>> ListAllAsync(int pageNr)
        {
            List<T> tweets =  _context.GetCollection<T>(typeof(T).Name).Find(_ => true).Skip((pageNr - 1) * 10).Limit(10).ToList();
            List<TweetDto> tweetDtos = new List<TweetDto>();
            
            foreach (var variable in tweets)
            {
                Tweet? t = variable as Tweet;
                TweetDto tweetDto = new TweetDto();
                if (t != null)
                {
                    tweetDto.Feels = t.feels;
                    tweetDto.Date = t.Date;
                    tweetDto.Id = t.Id.ToString();
                    tweetDto.Text = t.Text;
                    tweetDto.User = t.User;
                    tweetDto.Username = t.Username;
                    tweetDtos.Add(tweetDto);
                }
            }

            return await Task.FromResult(tweetDtos);
        }

        public async Task<T> AddAsync(T entity)
        {
            await _context.GetCollection<T>(typeof(T).Name).InsertOneAsync(entity);
            return entity;
        }
        
        public UpdateResult UpdateAsync(string id, Dictionary<string, float>  feels)
        {
            var objectId = new ObjectId(id);
            return _context.GetCollection<T>(typeof(T).Name).UpdateOne(Builders<T>.Filter.Eq("_id", objectId),
                Builders<T>.Update.Set("feels", feels));
        }

        public DeleteResult DeleteAsync(string id)
        {
            var objectId = new ObjectId(id);
            return _context.GetCollection<T>(typeof(T).Name).DeleteOne(Builders<T>.Filter.Eq("_id", objectId));
        }

        public UserRole GetUserRole(string userId)
        {
            var res = _context.GetCollection<UserRole>(typeof(UserRole).Name).Find( x => x.userId.Equals(userId)).ToList();
            return res[0];
        }

        public object GetJwtToken(string tweets)
        {
            TweetSerializer tweetSerializer = JsonConvert.DeserializeObject<TweetSerializer>(tweets);
            
            foreach (var variable in tweetSerializer.data)
            {
                MiniTweet tweet = new MiniTweet();
                tweet.text = variable.text;
                tweet.userId = tweetSerializer.userId;
                tweet.tweetId = variable.id;

                if (_context.GetCollection<MiniTweet>(typeof(MiniTweet).Name).Find(x => x.tweetId.Equals(tweet.tweetId)).ToList()
                    .Count == 0)
                {
                    _context.GetCollection<MiniTweet>(typeof(MiniTweet).Name).InsertOneAsync(tweet);
                }
            }
            
            string key = "turbo-secret-key"; //Secret key which will be used later during validation    
            var issuer = "http://twitter-sentiment-analyser.com";  //normally this will be your site URL    
            
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));    
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);    
            
            //Create a List of Claims, Keep claims name short    
            var permClaims = new List<Claim>();    
            permClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));    
            permClaims.Add(new Claim("userId", tweetSerializer.userId));
            permClaims.Add(new Claim(ClaimTypes.Role, GetUserRole(tweetSerializer.userId).role));
            
            var token = new JwtSecurityToken(issuer,
                issuer,
                permClaims,
                expires: DateTime.Now.AddDays(1), 
                signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public List<MiniTweet> GetMyTweets(string userId)
        {
            return _context.GetCollection<MiniTweet>(typeof(MiniTweet).Name).Find(x => x.userId.Equals(userId))
                .ToList();
        }
    }
}