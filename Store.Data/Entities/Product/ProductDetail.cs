using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Data.Entities.Product
{
    public class ProductDetail
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int  Code { get; set; }
        public string Description { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
