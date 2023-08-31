using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Shop_Models.Models;

namespace Shop_DataAccess.Repository.IRepository
{
     public interface IProductRepository : IRepository<Product>
    {
        void Update(Product obj);
    }
}