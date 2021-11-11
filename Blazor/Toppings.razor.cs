
using System.Collections.Generic;
using System.Linq;
using MaxPizzaProject.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;


namespace MaxPizzaProject.Blazor
{
    public partial class Toppings
    {
      
        protected override void OnInitialized()
        {
            
            //Initialization of Size that selected.
            foreach (Size s in SelectedPizza.Category.Sizes.Reverse()){
                PizzaSizeId = s.Id;
                PizzaSize = s.TheSize;
            }

            PizzaPrice = PizzaContext.GetPizzaPrice(PizzaSizeId,SelectedPizza.CategoryId);
            TotalPrice = PizzaPrice;


            //Initialization of Topping Category that selected.
            foreach (Category c in ToppCategories.Reverse())
            {
                SelectedCatId = c.Id;
                SelectedToppingCategoryName = c.Name;
            }

            ToppingsOfSpecCategory = ToppContext.GetToppingsByCategory(SelectedCatId);
        }

        [Inject]
        public IToppingRepository ToppContext { get; set; }

        [Inject]
        public IPizzaRepository PizzaContext { get; set; }


        [Parameter]
        public long PizzaId { get; set; }

        public Pizza SelectedPizza => PizzaContext.GetPizzaById(PizzaId);

        public decimal ToppPrice { get; set; } 

        public IEnumerable<Category> ToppCategories => ToppContext.GetToppCategories();


        IEnumerable<Topping> ToppingsOfSpecCategory { get; set; }

        public decimal PizzaPrice { get; set; }

        public decimal TotalPrice { get; set; } 

        public long PizzaSizeId { get; set; }

        public string PizzaSize { get; set; }

        public long SelectedCatId { get; set; }

        public string SelectedToppingCategoryName { get; set; }

        
        Dictionary<string, int> allAddedToppings = new Dictionary<string, int>();
       
        public void ChangePizzaSize(long sizeId, long categoryId, string pizzaSize)
        {
            PizzaPrice = PizzaContext.GetPizzaPrice(sizeId, categoryId);
            PizzaSize = pizzaSize;
            PizzaSizeId = sizeId;
            TotalPrice = PizzaPrice;

            allAddedToppings.Clear();
        }

        public void AddTopping(string toppName)
        {
            decimal price = ToppingPrice(SelectedCatId, PizzaSizeId);
            TotalPrice += price;

            
            if (allAddedToppings.ContainsKey(toppName))
            {
                allAddedToppings[toppName] += 1;
            }
            else
            {
                allAddedToppings.Add(toppName, 1);
            }
          
        }

        public void RemoveTopping(string toppName)
        {
            if (allAddedToppings.ContainsKey(toppName))
            {
                int quantity = allAddedToppings[toppName];
                long catId = ToppContext.GetCategoryIdByToppingName(toppName);
                decimal toppPrice = ToppingPrice(catId, PizzaSizeId) * quantity;
                TotalPrice -= toppPrice;

                allAddedToppings.Remove(toppName);
            }
           
        }

        public string GetClass(string size)
        {
            return PizzaSize == size ? "success" : "outline-dark";
        }

        public string GetToppClass(string topCat)
        {
            return SelectedToppingCategoryName == topCat ? "success" : "outline-dark";
        }

        

        public void SeeToppings(MouseEventArgs e, long catId,string topCatName)
        {
            SelectedCatId = catId;
            SelectedToppingCategoryName = topCatName;
            ToppingsOfSpecCategory = ToppContext.GetToppingsByCategory(catId);

           // return ToppingsOfSpecCategory;
        }

        public decimal ToppingPrice(long toppCatId, long sizeId)
        {
           
            ToppPrice = ToppContext.GetToppPrice(toppCatId, sizeId);
            return ToppPrice;
        }
    }
}
