using Microsoft.EntityFrameworkCore;
using ShopApp.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopApp.DataAccess.Concrete.EfCore
{
    public class ShopContext : DbContext
    {
        //shop context hangi db sağlayıcısını bilmiyor o yüzden belirtmenk zorundayız örn mysql mssql
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=ShopDb;integrated security=true;");
        }
        //ProductCategory Class'ında iki tane primary key olmak zorunda bu yüzden on model creating metodunu kull.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductCategory>().HasKey(c=> new{c.CategoryId,c.ProductId });
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        //cart alanından sonra ekledik... PM Console Add-Migration AddingCartEntities -Context ShopContext ,Update-Database -Context ShopContext
        public DbSet<Cart> Carts { get; set; }

        //siparis alanından sonra ekledik add-migration AddingOrderEntities -Context ShopContext ,Update-Database -Context ShopContext
        public DbSet<Order> Orders { get; set; }
       

    }
}
