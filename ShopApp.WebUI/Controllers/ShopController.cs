using Microsoft.AspNetCore.Mvc;
using ShopApp.Business.Abstract;
using ShopApp.Entities;
using ShopApp.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopApp.WebUI.Controllers
{
    public class ShopController : Controller
    {
        private IProductService _productService;
        public ShopController(IProductService productService)
        {
            _productService = productService;

        }
       

       
        public IActionResult Index(string text)
        {
            var products = new ProductListModel()
            {
                Products = _productService.GetAll(),
                Text = text
            };

            if (text!= null )
            {
                products.Products = _productService.GetProductsByName(text);
                products.Text = text;

                return View(products);
            }
            else 
            {
                return View(products);
            }
           
           
        }

        public IActionResult Details(int? id)
        {
            if (id==null)
            {
                return NotFound();
            }
            Product product = _productService.GetProductDetails((int)id);
            if (product == null)
            {
                return NotFound();
            }
            return View(new ProductDetailsModel()
            {
                Product = product,
                Categories = product.ProductCategories.Select(i => i.Category).ToList()


            }); ;
        }
        // /products/telefon?page=2 yani products ta ki kategoriye 1. sınıf vb. querystring
        public IActionResult List(string category, int page=1)
        {
            const int pageSize = 3;

            return View(new ProductListModel()
            {
                PageInfo = new PageInfo()
                {
                    TotalItems = _productService.GetCountByCategory(category),
                    ItemsPerPage = pageSize,
                    CurrentPage = page,
                    CurrentCategory = category
                    
                   

                },
                Products = _productService.GetProductsByCategory(category, page, pageSize)
               
            
            }
            
             ); 
        }
    }
}
