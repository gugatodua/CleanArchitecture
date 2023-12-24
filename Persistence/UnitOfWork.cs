using Application;
using Persistence.Repositories;

namespace Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TbcDbContext _tbcDbContext;

        public UnitOfWork(TbcDbContext tbcDbContext)
        {
            _tbcDbContext = tbcDbContext;
        }

        public async Task CommitAsync()
        {
            await _tbcDbContext.SaveChangesAsync();
        }
    }
}