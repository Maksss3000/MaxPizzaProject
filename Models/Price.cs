using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MaxPizzaProject.Models
{
    public class Price
    {
        public long Id { get; set; }

        public long ThePrice { get; set; }

      //  public Size Size { get; set; }

        //public long SizeId { get; set; }

        //Many to many  with Category?
        // public IEnumerable<Category> Categories { get; set; }

        public IEnumerable<Size> Sizes { get; set; }
    }
}
