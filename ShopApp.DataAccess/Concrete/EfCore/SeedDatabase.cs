using Microsoft.EntityFrameworkCore;
using ShopApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShopApp.DataAccess.Concrete.EfCore
{
    public static class SeedDatabase
    {
        public static void Seed()
        {
            var context = new ShopContext();
            if (context.Database.GetPendingMigrations().Count()==0)
            {
                if (context.Categories.Count()==0)
                {
                    context.Categories.AddRange(Categories);

                }
                if (context.Products.Count() == 0)
                {
                    context.Products.AddRange(Products);
                    context.AddRange(ProductCategory);
                }
                context.SaveChanges();
            }
            
        }  
            private static Category[] Categories = 
            {
                new Category{Name="Telefon"},
                new Category{Name="TV"},
                new Category{Name="Tablet"},
                new Category{Name="Masaüstü Bilgisayar"},
                new Category{Name="Laptop"}

            };
        private static Product[] Products =
    {
              new Product(){ Name= "Samsung S6" , ImageURL="1.jpg" , Price=2500 ,Description="<p>güzel bir telefon</p>"},
              new Product(){ Name= "Samsung S6 S" , ImageURL="2.jpg" , Price=2500,Description="<p>güzel bir telefon</p>"},
              new Product(){ Name= "Samsung S7" , ImageURL="3.jpg" , Price=2500,Description="<p>güzel bir telefon</p>"},
              new Product(){ Name= "Samsung S8" , ImageURL="4.jpg" , Price=2500,Description="<p>güzel bir telefon</p>"},
              new Product(){ Name= "Samsung S9" , ImageURL="5.jpg" , Price=2500,Description="<p>güzel bir telefon</p>"},
              new Product(){ Name= "Samsung S10" , ImageURL="6.jpg" , Price=2500,Description="<p>güzel bir telefon</p>"},
              new Product(){ Name= "Samsung S11" , ImageURL="7.jpg" , Price=2500,Description="<p>güzel bir telefon</p>"},
              new Product(){ Name= "Samsung S12" , ImageURL="8.jpg" , Price=2500,Description="<p>güzel bir telefon</p>"},
              new Product(){ Name= "Samsung S6" , ImageURL="9.jpg" , Price=2500,Description="<p>güzel bir telefon</p>"},
              new Product(){ Name= "Samsung Akıllı TV" , ImageURL="10.jpg" , Price=12500,Description="<p>güzel bir telefon</p>"},
              new Product(){ Name= "Samsung Akıllı TV 2" , ImageURL="11.jpg" , Price=22500,Description="<p>güzel bir telefon</p>"},
              new Product(){ Name= "Samsung Akıllı TV 3" , ImageURL="12.jpg" , Price=32500,Description="<p>güzel bir telefon</p>"},
              new Product(){ Name= "Samsung Tablet S" , ImageURL="13.jpg" , Price=2500,Description="<p>güzel bir telefon</p>"},
              new Product(){ Name= "Samsung Tablet 1" , ImageURL="14.jpg" , Price=2500,Description="<p>güzel bir telefon</p>"}
            };


        private static ProductCategory[] ProductCategory = {
            new ProductCategory(){Product = Products[0] , Category=Categories[0]},
            new ProductCategory(){Product = Products[1] , Category=Categories[0]},
            new ProductCategory(){Product = Products[2] , Category=Categories[0]},
            new ProductCategory(){Product = Products[3] , Category=Categories[0]},
            new ProductCategory(){Product = Products[4] , Category=Categories[0]},
            new ProductCategory(){Product = Products[5] , Category=Categories[0]},
            new ProductCategory(){Product = Products[6] , Category=Categories[0]},
            new ProductCategory(){Product = Products[7] , Category=Categories[0]},
            new ProductCategory(){Product = Products[8] , Category=Categories[0]},
            new ProductCategory(){Product = Products[9] , Category=Categories[1]},
            new ProductCategory(){Product = Products[10] , Category=Categories[1]},
            new ProductCategory(){Product = Products[11] , Category=Categories[1]},
            new ProductCategory(){Product = Products[12] , Category=Categories[2]},
            new ProductCategory(){Product = Products[13] , Category=Categories[2]}


        };




    }
}
