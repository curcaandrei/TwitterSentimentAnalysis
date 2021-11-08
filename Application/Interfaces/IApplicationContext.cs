using System.Data.Entity;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IApplicationContext 
    {
        DbSet<Domain.Entities.Tweet> Products { get; set; }

        Task<int> SaveChangesAsync();
    }
}