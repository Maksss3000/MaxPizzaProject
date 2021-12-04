using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MaxPizzaProject.Models
{
  public interface ISnackRepository

    {
        public IEnumerable<Snack> Snacks { get; }
     //   public Pizza GetPizzaById(long id);

        public IEnumerable<Snack> GetSnacksByCategoryName(string catName);
        //public IEnumerable<Pizza> GetPizzasByCategoryId(long id);
       // public decimal GetPizzaPrice(long sizeId, long categoryId);

        public IEnumerable<Category> GetAllSnackCategories();

        public Snack GetSnackById(long snackId);

       // Product GetProduct(long prodId);
       // public void EditPizza(Pizza p);
       // public void AddPizza(Pizza p);

        //void DeleteProduct(Product product);
    }
}
