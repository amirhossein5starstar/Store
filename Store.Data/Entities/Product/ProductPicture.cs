using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Data.Entities.Product
{
   public class ProductPicture
    {
        public int Id { get; set; }
        public string PictureName { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }

    }
}
