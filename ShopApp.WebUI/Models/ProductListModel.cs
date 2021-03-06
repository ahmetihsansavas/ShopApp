using ShopApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopApp.WebUI.Models
{
    public class PageInfo
    {
        public int TotalItems { get; set; }
        public int ItemsPerPage { get; set; }
        public int CurrentPage { get; set; }
        public string CurrentCategory { get; set; }

        public int TotalPage()
        {
            //örn; toplam 10 ürün varsa ve biz her sayfada 3 ürün göst. 10/3 =3.3=>4 e tamamlanacak
            return (int)Math.Ceiling((decimal)TotalItems/ItemsPerPage);
        }

    }
    public class ProductListModel
    {
        public PageInfo PageInfo { get; set; }
        public List<Product> Products { get; set; }
        public List<Category> Categories { get; set; }
        public string Text { get; set; }
    }
}
