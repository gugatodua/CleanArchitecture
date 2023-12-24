using Application;
using Microsoft.EntityFrameworkCore.Storage;

namespace Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TbcDbContext _tbcDbContext;
        private IDbContextTransaction _currentTransaction;

        public UnitOfWork(TbcDbContext tbcDbContext)
        {
            _tbcDbContext = tbcDbContext;
        }
        public async Task BeginTransactionAsync()
        {
            _currentTransaction = await _tbcDbContext.Database.BeginTransactionAsync();
        }
        public async Task CommitAsync()
        {
            try
            {
                await _tbcDbContext.SaveChangesAsync();
                await _currentTransaction.CommitAsync();
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    await _currentTransaction.DisposeAsync();
                    _currentTransaction = null;
                }
            }
        }

        public async Task RollbackTransactionAsync()
        {
            try
            {
                await _currentTransaction.RollbackAsync();
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    await _currentTransaction.DisposeAsync();
                    _currentTransaction = null;
                }
            }
        }
    }
}