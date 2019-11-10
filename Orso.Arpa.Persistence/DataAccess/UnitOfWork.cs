using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Orso.Arpa.Domain.Interfaces;
using Orso.Arpa.Persistence.DataAccess;

namespace Orso.Arpa.Infrastructure.DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ArpaContext _context;

        public UnitOfWork(ArpaContext context)
        {
            _context = context;
        }

        public async Task<bool> CommitAsync()
        {
            try
            {
                var rowsAffected = await _context.SaveChangesAsync();
                return rowsAffected > 0;
            }
            catch (DbUpdateException dbUpdateException)
            {
                throw new Exception("Problem saving changes", dbUpdateException);
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }
    }
}
