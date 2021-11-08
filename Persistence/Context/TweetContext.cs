using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Context
{
    public class TweetContext : DbContext
    {
        public TweetContext(DbContextOptions<TweetContext> options) : base(options)
        {
        }
        
        public DbSet<Tweet> Tweets { get ; set; }
        
        public async Task<int> SaveChangesAsync()
        {
            return await base.SaveChangesAsync();
        }
    }
}