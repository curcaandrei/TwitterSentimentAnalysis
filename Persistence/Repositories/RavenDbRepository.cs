using System;
using System.Collections.Generic;
using System.Linq;
using Application.Interfaces;
using Persistence.Context;

namespace Persistence.Repositories
{
public class RavenDbRepository<TEntity> : IRepository<TEntity>
    {
        private readonly IRavenDbContext _context;

        public RavenDbRepository(IRavenDbContext context)
        {
            _context = context;
        }
        public void CreateOrUpdate(TEntity obj)
        {
            try
            {
                using var session = _context.store.OpenSession();
                session.Store(obj);
                session.SaveChanges();
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null) throw new RepositoryException(ex.Message, ex.InnerException);
            }
        }

        public void Delete(string id)
        {
            try
            {
                using var session = _context.store.OpenSession();
                var element = session.Load<TEntity>(id);
                session.Delete(element);
                session.SaveChanges();
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null) throw new RepositoryException(ex.Message, ex.InnerException);
            }
        }

        public TEntity SelectById(string id)
        {

                using var session = _context.store.OpenSession();
                var element = session.Load<TEntity>(id);
                return element;
        }

        public IEnumerable<TEntity> SelectAll()
        {
            try
            {

                using var session = _context.store.OpenSession();

                var elements = session
                    .Query<TEntity>()
                    .ToList();

                return elements;
            }
            catch (Exception ex)
            {
                throw new RepositoryException(ex.Message, ex.InnerException);
            }
        }
    }
    public class RepositoryException : Exception
    {
        public RepositoryException(string message, Exception exception) : base(message, exception)
        {

        }
    }
}