using ShopApp.DataAccess.Abstract;
using ShopApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace ShopApp.DataAccess.Concrete.Memory
{
    public class MemoryProductDal : IProductDal
    {
        public void Create(Product entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Product entity)
        {
            throw new NotImplementedException();
        }

        public List<Product> GetAll(Expression<Func<Product, bool>> filter = null)
        {
            var products = new List<Product>()
          {
              new Product(){Id=1 , Name= "Samsung S6" , ImageURL="1.jpg" , Price=2500},
              new Product(){Id=1 , Name= "Samsung S6 S" , ImageURL="2.jpg" , Price=2500},
              new Product(){Id=1 , Name= "Samsung S7" , ImageURL="3.jpg" , Price=2500},
              new Product(){Id=1 , Name= "Samsung S8" , ImageURL="4.jpg" , Price=2500},
              new Product(){Id=1 , Name= "Samsung S9" , ImageURL="5.jpg" , Price=2500},
              new Product(){Id=1 , Name= "Samsung S10" , ImageURL="6.jpg" , Price=2500},
              new Product(){Id=1 , Name= "Samsung S11" , ImageURL="7.jpg" , Price=2500},
              new Product(){Id=1 , Name= "Samsung S12" , ImageURL="8.jpg" , Price=2500}
          };
            return products;
        }

        public Product GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Product GetByIdWithCategory(int id)
        {
            throw new NotImplementedException();
        }

        public int GetCountByCategory(string category)
        {
            throw new NotImplementedException();
        }

        public Product GetOne(Expression<Func<Product, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public List<Product> GetPopularProducts()
        {
            throw new NotImplementedException();
        }

        public Product GetProductDetail(int id)
        {
            throw new NotImplementedException();
        }

        public List<Product> GetProductsByCategories(string category)
        {
            throw new NotImplementedException();
        }

        public List<Product> GetProductsByCategories(string category, int page)
        {
            throw new NotImplementedException();
        }

        public List<Product> GetProductsByCategories(string category, int page, int pageSize)
        {
            throw new NotImplementedException();
        }

        public List<Product> GetProductsByName(string text)
        {
            throw new NotImplementedException();
        }

        public void Update(Product entity)
        {
            throw new NotImplementedException();
        }

        public void Update(Product entity, int[] categoryIds)
        {
            throw new NotImplementedException();
        }
    }
}
