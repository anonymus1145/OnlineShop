using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Shop.Application.Repository.IRepository;
using Shop.Domain.Models;

namespace Shop.Application.Repository
{
    public interface ICategoryRepository : IRepository<Category>
    {
        void Update(Category obj);

    }
}
