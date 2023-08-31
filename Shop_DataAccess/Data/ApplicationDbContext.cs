using Microsoft.EntityFrameworkCore;
using Shop_Models.Models;

namespace Shop_DataAccess.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }
        //Create a DbSet for Models -> Category
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

        //We override the  default function
        //Using this builder we can use to see data
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Ecrane", DisplayOrder = 100},
                new Category { Id = 2, Name = "Capace Spate", DisplayOrder = 100},
                new Category { Id = 3, Name = "Acumulatori", DisplayOrder = 100},
                new Category { Id = 4, Name = "Programare Reparatii", DisplayOrder = 10}
            );

            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    Nume = "Capac spate",
                    ModelCompatibil = "N970",
                    Producator = "SAMSUNG",
                    Descriere = "Capac baterie original, Service Pack pentru Samsung Galaxy Note 10",
                    CodProdus = "GH82-20528A",
                    Culoare = "Aura Black",
                    ListPrice = 180,
                    Price = 170,
                    Price3 = 160,
                    CategoryId = 2,
                    ImageUrl =""
                },
                 new Product
                {
                    Id = 2,
                    Nume = "Capac spate",
                    ModelCompatibil = "N970",
                    Producator = "SAMSUNG",
                    Descriere = "Capac baterie original, Service Pack pentru Samsung Galaxy Note 10",
                    CodProdus = "GH82-20528C",
                    Culoare = "Aura Glow",
                    ListPrice = 180,
                    Price = 170,
                    Price3 = 160,
                    CategoryId = 2,
                    ImageUrl =""
                },
                new Product
                {
                    Id = 3,
                    Nume = "Capac spate",
                    ModelCompatibil = "N970",
                    Producator = "SAMSUNG",
                    Descriere = "Capac baterie original, Service Pack pentru Samsung Galaxy Note 10",
                    CodProdus = "GH82-20528B",
                    Culoare = "Aura White",
                    ListPrice = 180,
                    Price = 170,
                    Price3 = 160,
                    CategoryId = 2,
                    ImageUrl =""
                }
            );
        }
    }
}