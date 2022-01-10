﻿using System.Threading.Tasks;
using Domain;
using Domain.Dtos;
using Domain.Entities;
using MongoDB.Driver;

namespace Application.Persistence
{
    public interface IRequestTweetRepository
    {
        public Task<Tweet> AddAsync(Tweet entity);
        
        public DeleteResult DeleteAsync(string id);

        public void RetrainAlgorithm(string id);
    }
}