using Microsoft.EntityFrameworkCore;
using ShopApp.DataAccess.Abstract;
using ShopApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace ShopApp.DataAccess.Concrete.EfCore
{
    public class EfCoreProductDal : EfCoreGenericRepository<Product, ShopContext>, IProductDal
    {
        public Product GetByIdWithCategory(int id)
        {
            using (var context = new ShopContext())
            {
                return context.Products.Where(i => i.Id == id)
                    .Include(i => i.ProductCategories)
                    .ThenInclude(i => i.Category)
                    .FirstOrDefault();
            }
        }

        public int GetCountByCategory(string category)
        {
            using (var context = new ShopContext())
            {
                var products = context.Products.AsQueryable();
                if (!string.IsNullOrEmpty(category))
                {
                    products = products
                                        .Include(i => i.ProductCategories)
                                        .ThenInclude(i => i.Category)
                                        .Where(i => i.ProductCategories.Any(a => a.Category.Name.ToLower() == category));
                }
                else
                {
                    return context.Products.Count();
                }
                return products.Count();
            }

        }

        public List<Product> GetPopularProducts()
        {
            throw new NotImplementedException();
        }

        public Product GetProductDetail(int id)
        {
            using (var context = new ShopContext())
            {
                //Lazy Loading yönt. ile aynı anda birden fazla sorgu gönderdik ve bu sayede hem product a hemde productcategories e aynı anda ulaştık.
                return context.Products.Where(i => i.Id == id)
                                       .Include(i => i.ProductCategories)
                                       .ThenInclude(i => i.Category)
                                       .FirstOrDefault();
            }
        }

        public List<Product> GetProductsByCategories(string category,int page, int pageSize)
        {
            using (var context = new ShopContext())
            {
                var products = context.Products.AsQueryable();
                if (!string.IsNullOrEmpty(category))
                {
                    products = products
                                        .Include(i => i.ProductCategories)
                                        .ThenInclude(i => i.Category)
                                        .Where(i => i.ProductCategories.Any(a=>a.Category.Name.ToLower() == category));
                 }
                //sayfada gösterilecek ürün sayısını ayarlama örn 1. sayfa için 1-1*3 =0 gösterilecek ilk 3 ürün
                return products.Skip((page-1)*pageSize).Take(pageSize).ToList();
            }
           
        }

        public List<Product> GetProductsByName(string text)
        {
            using (var context = new ShopContext())
            {
                
                if (!string.IsNullOrEmpty(text))
                {
                  var  products = context.Products.Where(i => i.Name.Contains(text)).ToList(); //contains verilen sözcükle benzer olanları getirir where birebir aynısını getirir
                   
                    return products;
                }
                else
                {
                    return null;
                }
                
            }
        }

        public void Update(Product entity, int[] categoryIds)
        {
            using (var context = new ShopContext())
            {
                var product = context.Products
                    .Include(i => i.ProductCategories)
                    .FirstOrDefault(i => i.Id == entity.Id);
                if (product!=null)
                {
                    product.Name = entity.Name;
                    product.ImageURL = entity.ImageURL;
                    product.Price = entity.Price;
                    product.Description = entity.Description;
                    product.ProductCategories = categoryIds.Select(catid => new ProductCategory()
                    {
                        CategoryId = catid,
                        ProductId = entity.Id,
                        

                    }).ToList();
                    context.SaveChanges();
                }
            }
        }
    }
}
