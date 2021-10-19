using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MaxPizzaProject.Models
{
    public class CategorySize
    {
        public long CategoryId { get; set; }

        public long SizeId { get; set; }

        public decimal Price { get; set; }

        public Category Category { get; set; }

        public Size Size { get; set; }
    }
}
