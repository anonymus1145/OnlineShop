using Microsoft.EntityFrameworkCore;

using Shop.Domain.Models;

namespace Shop.Application
{
    public interface IApplicationDatabase
    {
        DbSet<Category> Categories { get; }
        DbSet<Product> Products { get; }

        DbSet<TEntity>? CreateSet<TEntity>() where TEntity : class;
        int SaveChanges();
    }
}
