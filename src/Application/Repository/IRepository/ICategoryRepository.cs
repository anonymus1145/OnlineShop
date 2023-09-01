using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shop_DataAccess.Repository.IRepository;
using Shop_Models.Models;

namespace Shop_DataAccess.Repository
{
    public interface ICategoryRepository  : IRepository<Category>
    {
        void Update(Category obj);

    }
}