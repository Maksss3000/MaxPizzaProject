using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MaxPizzaProject.Models
{
    public class Size
    {
        public long Id { get; set; }

        public IEnumerable<Category> Categories { get; set; }

        //Many to many with Price?
        public IEnumerable<Price> Prices { get; set; }

    }
}
