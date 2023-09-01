using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shop.Application.Repository.IRepository;

namespace Shop.Application.Repository.IRepository
{
    public interface IUnitOfWork
    {
        ICategoryRepository Category { get; }
        IProductRepository Product { get; }

        void Save();
    }
}