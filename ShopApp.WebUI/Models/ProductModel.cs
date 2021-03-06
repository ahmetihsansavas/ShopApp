using ShopApp.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShopApp.WebUI.Models
{
    public class ProductModel
    {
        public int Id { get; set; }
        [Required] 
        [StringLength(60,MinimumLength = 10, ErrorMessage  ="Ürün ismi minimum 10 karakter ve maksimum 60 karakter olmalıdır.")]
        public string Name { get; set; }
        [Required]
        public string ImageURL { get; set; }
        [Required]
        [StringLength(60, MinimumLength = 10, ErrorMessage = "Ürün acıklaması minimum 10 karakter ve maksimum 60 karakter olmalıdır.")]
        public string Description { get; set; }

        [Required(ErrorMessage ="Fiyat Belirtiniz")]
        [Range(1,250000)]
        public int Price { get; set; }

        public List<Category> SelectedCategory { get; set; }
    }
}
