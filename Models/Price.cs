using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MaxPizzaProject.Models
{
    public class Price
    {
        public long Id { get; set; }

        public long Cost { get; set; }

        //Many to many  with Size?
        public IEnumerable<Size> Sizes { get; set; }
    }
}
