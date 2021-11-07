using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MaxPizzaProject.Models
{
    public class Product
    {
        public long Id { get; set; }
        
        public string Name { get; set; }

        public bool InStock { get; set; }

        public string ImagePath { get; set; }

        public Category Category { get; set; }

        public long CategoryId { get; set; }

        public string Description { get; set; }
    }
}
