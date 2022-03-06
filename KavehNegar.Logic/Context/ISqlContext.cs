using KavehNegar.Logic.SharedKernel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace KavehNegar.Logic.Context
{
    public interface ISqlContext
    {
        int SaveChanges();
        Task<int> SaveChangesAsync();
        Task<int> SaveChangesAsync(System.Threading.CancellationToken cancellationToken);
        DbSet<TEntity> Set<TEntity>() where TEntity : BaseEntity;
        EntityEntry Entry(object entity);

        void Dispose();
    }
}
