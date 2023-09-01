using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shop_DataAccess.Repository.IRepository;

namespace Shop_DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        ICategoryRepository Category { get; }
        IProductRepository Product { get; }

        void Save();
    }
}