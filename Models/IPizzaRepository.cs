using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MaxPizzaProject.Models
{
    public interface IPizzaRepository
    {
        public IEnumerable<Pizza> Pizzas { get; }
        public Pizza GetPizzaById(long id);

        public decimal GetPizzaPrice(long sizeId, long categoryId);
    }
}
