using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Data.Entities.Product
{
   public class ProductGroup
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public bool IsDelete { get; set; }

        public List<Product> Products { get; set; }
    }
}
