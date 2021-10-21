
using System.Collections.Generic;
using MaxPizzaProject.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace MaxPizzaProject.Blazor
{
    public partial class Toppings
    {

        [Inject]
        public IToppingRepository ToppContext { get; set; }

        [Inject]
        public IPizzaRepository PizzaContext { get; set; }

        public decimal PizzaPrice { get; set; }

        public long PizzaSizeId { get; set; }

        public string PizzaSize { get; set; }

        public string SelectedToppingCategoryName { get; set; }

        public void ChangePizzaSize(long sizeId, long categoryId, string pizzaSize)
        {
            PizzaPrice = PizzaContext.GetPizzaPrice(sizeId, categoryId);
            PizzaSize = pizzaSize;
            PizzaSizeId = sizeId;
        }

        public string GetClass(string size)
        {
            return PizzaSize == size ? "success" : "outline-dark";
        }

        public string GetToppClass(string topCat)
        {
            return SelectedToppingCategoryName == topCat ? "success" : "outline-dark";
        }

        [Parameter]
        public long PizzaId { get; set; }

        public Pizza SelectedPizza => PizzaContext.GetPizzaById(PizzaId);

        public decimal ToppPrice { get; set; } = 0;

        public IEnumerable<Category> ToppCategories => ToppContext.GetToppCategories();


        IEnumerable<Topping> ToppingsOfSpecCategory { get; set; }

        public IEnumerable<Topping> SeeToppings(MouseEventArgs e, long catId,string topCatName)
        {
            SelectedToppingCategoryName = topCatName;
            ToppingsOfSpecCategory = ToppContext.GetToppingsByCategory(catId);

            return ToppingsOfSpecCategory;
        }

        public decimal ToppingPrice(long toppCatId, long sizeId)
        {
           
            ToppPrice = ToppContext.GetToppPrice(toppCatId, sizeId);
            return ToppPrice;
        }
    }
}
