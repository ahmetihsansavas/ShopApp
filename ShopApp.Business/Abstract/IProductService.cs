using ShopApp.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopApp.Business.Abstract
{
   public interface IProductService:IValidator<Product>
    {
        Product GetById(int id);
        Product GetProductDetails(int id);
        List<Product> GetAll();
        List<Product> GetPopularProducts();
        List<Product> GetProductsByCategory(string? category,int page, int pageSize);
        void Create(Product Entity);
        void Update(Product Entity);
        void Delete(Product Entity);
        int GetCountByCategory(string category);
        Product GetByIdWithCategory(int id);
        void Update(Product entity, int[] categoryIds);
        List<Product> GetProductsByName(string text);
    }
}
