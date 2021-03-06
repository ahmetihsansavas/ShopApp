using ShopApp.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopApp.Business.Abstract
{
   public interface ICategoryService
    {
        Category GetById(int id);
        Category GetByIdWithProducts(int id);
        int GetCountProductById(int id);
        List<Category> GetAll();
        void Create(Category Entity);
        void Update(Category Entity);
        void Delete(Category Entity);
        
    }
}
