using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Data.Entities.Product
{
    public class Product
    {

        public int Id { get; set; }
        public string PersianName { get; set; }
        public string EnglishName { get; set; }
        public string Price { get; set; }

        public int ProductGroupId { get; set; }
        public ProductGroup ProductGroup { get; set; }
        public List<ProductDetail> ProductDetails { get; set; }
        public List<ProductPicture> ProductPictures { get; set; }
        public List<ProductComment> ProductComments { get; set; }

    }
}
