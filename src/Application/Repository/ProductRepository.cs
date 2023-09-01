using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using Shop.Application.Data;
using Shop.Application.Repository.IRepository;
using Shop.Domain.Models;

namespace Shop.Application.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ApplicationDbContext _db;
        public ProductRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public void Update(Product obj)
        {
            Product? objFromDb = _db.Products.FirstOrDefault(u => u.Id == obj.Id);
            if (objFromDb != null)
            {
                objFromDb.Nume = obj.Nume;
                objFromDb.ModelCompatibil = obj.ModelCompatibil;
                objFromDb.Producator = obj.Producator;
                objFromDb.Descriere = obj.Descriere;
                objFromDb.CodProdus = obj.CodProdus;
                objFromDb.Culoare = obj.Culoare;
                objFromDb.ListPrice = obj.ListPrice;
                objFromDb.Price = obj.Price;
                objFromDb.Price3 = obj.Price3;
                if (obj.ImageUrl != null)
                {
                    objFromDb.ImageUrl = obj.ImageUrl;
                }
            }
        }
    }
}
