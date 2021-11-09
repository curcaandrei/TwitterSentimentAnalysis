using Raven.Client.Documents;

namespace Persistence.Context
{
    public interface IRavenDbContext
    {
        public IDocumentStore store { get; }
    }
}