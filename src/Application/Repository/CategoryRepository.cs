using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

using Shop.Domain.Models;

namespace Shop.Application.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public IApplicationDatabase _db;
        public CategoryRepository(IApplicationDatabase db) : base(db)
        {
            _db = db;
        }

        public void Update(Category obj)
        {
            _db.Categories.Update(obj);
        }
    }
}
