using System.Security.Cryptography.X509Certificates;
using Microsoft.Extensions.Options;
using Raven.Client.Documents;
using Raven.Client.Documents.Operations;
using Raven.Client.Exceptions.Database;
using Raven.Client.ServerWide;
using Raven.Client.ServerWide.Operations;

namespace Persistence.Context
{
    public class RavenDbContext : IRavenDbContext
    {
        private readonly DocumentStore _localstore;
        public IDocumentStore store => _localstore;

        private readonly PersistenceSettings _persistenceSettings;

        public RavenDbContext(IOptionsMonitor<PersistenceSettings> settings)
        {
            X509Certificate2 clientCertificate = new 
                X509Certificate2("..\\Persistence\\Context\\free.dotnetproiect.client.certificate.pfx");
            
            _persistenceSettings = settings.CurrentValue;

            _localstore = new DocumentStore()
            {
                Certificate = clientCertificate,
                Database = _persistenceSettings.DatabaseName,
                Urls = _persistenceSettings.Urls
            };

            _localstore.Initialize();

            EnsureDatabaseIsCreated();
        }

        public void EnsureDatabaseIsCreated()
        {
            try
            {
                _localstore.Maintenance.ForDatabase(_persistenceSettings.DatabaseName).Send(new GetStatisticsOperation());
            }
            catch (DatabaseDoesNotExistException)
            {
                _localstore.Maintenance.Server.Send(new CreateDatabaseOperation(new DatabaseRecord(_persistenceSettings.DatabaseName)));
            }
        }
    }
}