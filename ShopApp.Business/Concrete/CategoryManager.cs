using ShopApp.Business.Abstract;
using ShopApp.DataAccess.Abstract;
using ShopApp.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopApp.Business.Concrete
{
    public class CategoryManager : ICategoryService
    {
        private ICategoryDal _categoryDal;
        public CategoryManager(ICategoryDal categoryDal)
        {
            _categoryDal = categoryDal;
        }
        public void Create(Category Entity)
        {
            _categoryDal.Create(Entity);
        }

        public void Delete(Category Entity)
        {
            _categoryDal.Delete(Entity);
        }

        public List<Category> GetAll()
        {
           return _categoryDal.GetAll();
        }

        public Category GetById(int id)
        {
          return  _categoryDal.GetById(id);
        }

        public Category GetByIdWithProducts(int id)
        {
            return _categoryDal.GetByIdWithProducts(id);
        }

        public int GetCountProductById(int id)
        {
           return _categoryDal.GetCountProductById(id);
        }

        public void Update(Category Entity)
        {
             _categoryDal.Update(Entity);
        }
    }
}
