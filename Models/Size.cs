using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MaxPizzaProject.Models
{
    public class Size
    {
        
        public long Id { get; set; }

        public string TheSize { get; set; }

        public IEnumerable<Category> Categories { get; set; }

        public List<CategorySize> CategoriesSizes { get; set; } = new List<CategorySize>();

    }
}
