using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Tweet> Tweets { get; set; }
        Task<int> SaveChangesAsync();
    }
}