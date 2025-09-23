using System;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Domain.Interfaces;

namespace GtMotive.Estimate.Microservice.Infrastructure.Persistence.MongoDb
{
    public sealed class MongoUnitOfWork : IUnitOfWork, IDisposable
    {
        private bool _disposed;

        public MongoUnitOfWork()
        {
            // MongoDB standalone
        }

        public async Task<int> Save()
        {
            // Just complete the task
            await Task.CompletedTask;
            return 1;
        }

        public void Dispose()
        {
            if (!_disposed)
            {
                _disposed = true;
                GC.SuppressFinalize(this);
            }
        }
    }
}
