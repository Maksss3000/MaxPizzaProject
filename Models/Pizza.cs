using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MaxPizzaProject.Models
{
    public class Pizza
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public Category Category { get; set; }

        public long CategoryId { get; set; }




    }
}
