using Microsoft.EntityFrameworkCore;
using ShopApp.DataAccess.Concrete.EfCore;
using ShopApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopApp.WebUI.Models
{
    public class CategoryListModel
    {
        public List<Category> Categories { get; set; }
        public int Quantity { get; set; }
        public int GetCountProductById(int id)
        {
            List<Product> product;
            using (var context = new ShopContext())
            {

                return context.Categories.Where(i => i.Id == id)
                     .Include(i => i.ProductCategories)
                     .ThenInclude(i => i.Product)
                     .Count();
            }
        }

    }
}
