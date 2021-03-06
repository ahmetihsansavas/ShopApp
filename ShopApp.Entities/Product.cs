﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ShopApp.Entities
{
   public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string ImageURL { get; set; }
        public string Description { get; set; }

        public int Price { get; set; }

        public List<ProductCategory> ProductCategories { get; set; }
    }
}
