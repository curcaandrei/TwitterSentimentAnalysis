using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Dtos;
using Domain.Entities;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Application.Persistence
{
    public interface IAsyncRepository<T> where T : class
    {
        Task<List<TweetDTO>> ListAllAsync(int pageNr);

        Task<T> AddAsync(T entity);

        UpdateResult UpdateAsync(string id,  Dictionary<string, float> feels);

        DeleteResult DeleteAsync(string id);
    }
}