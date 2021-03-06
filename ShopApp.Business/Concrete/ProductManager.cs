using ShopApp.Business.Abstract;
using ShopApp.DataAccess.Abstract;
using ShopApp.DataAccess.Concrete.EfCore;
using ShopApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShopApp.Business.Concrete
{
    public class ProductManager : IProductService
    {
        //business katmanının asıl amacı verininin eklenme kısıtlamalarını gerceklestirmek örn tc no 11 hane olmalı veya sonu cift olmalı vb.
        //eğer böyle bir kullanım yaparsak daha sonra dan efcore yerine mysql kullanırsak projeyi bastan sona 
        //değiştirmemiz gerekecek o yüzden bu kullanım yanlıs , yapmamız gereken interface üzerinden ulaşmak.
        //EfCoreProductDal _productDal = new EfCoreProductDal();
        private   IProductDal _productDal;
        public ProductManager(IProductDal productDal)
        {
            _productDal = productDal;

        }

       

        public void Create(Product Entity)
        {
            _productDal.Create(Entity);
        }

        public void Delete(Product Entity)
        {
            _productDal.Delete(Entity);
        }

        public List<Product> GetAll()
        {
            return _productDal.GetAll();
        }

        public Product GetById(int id)
        {
            return _productDal.GetById(id);
        }

        public Product GetByIdWithCategory(int id)
        {
           return _productDal.GetByIdWithCategory(id);
        }

        public int GetCountByCategory(string category)
        {
           return _productDal.GetCountByCategory(category);
        }

        public List<Product> GetPopularProducts()
        {
            return _productDal.GetPopularProducts();
        }

        public Product GetProductDetails(int id)
        {
            return _productDal.GetProductDetail(id);
        }

        public List<Product> GetProductsByCategory(string category,int page,int pageSize)
        {
           return _productDal.GetProductsByCategories(category,page,pageSize);
        }

        public void Update(Product Entity)
        {
            _productDal.Update(Entity);
        }

        public void Update(Product entity, int[] categoryIds)
        {
            _productDal.Update(entity,categoryIds);
        }

        public string ErrorMessage { get; set; }


        public bool Validate(Product entity)
        {
            var isValid = true;
            if (string.IsNullOrEmpty(entity.Name))
            {
                ErrorMessage += "ürün ismi girmelisiniz ";
                isValid = false;
                return isValid;

            }
            if (string.IsNullOrEmpty(entity.Description))
            {
                ErrorMessage += "ürün aciklamasi girmelisiniz ";
                isValid = false;
                return isValid;

            }
            return isValid;
   
        }

        public List<Product> GetProductsByName(string text)
        {
            return _productDal.GetProductsByName(text);
        }
    }
}
