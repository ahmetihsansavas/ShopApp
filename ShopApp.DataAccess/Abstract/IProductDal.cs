using ShopApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace ShopApp.DataAccess.Abstract
{
    public interface IProductDal : IRepository<Product>
    {
        List<Product> GetPopularProducts();
        Product GetProductDetail(int id);

        List<Product> GetProductsByCategories(string? category,int page, int pageSize);
        int GetCountByCategory(string? category);
        Product GetByIdWithCategory(int id);
        void Update(Product entity, int[] categoryIds);
        List<Product> GetProductsByName(string text);
    }
}
