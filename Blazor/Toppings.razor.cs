using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        [Inject]
        public PizzeriaDbContext Context { get; set; }
        
        [Inject]
        public ISnackRepository SnackContext { get; set; }
       
        [Parameter]
        public long ProductId { get; set; }

        
        public Product SelectedProduct => Context.Products.FirstOrDefault(p => p.Id == ProductId);
        
            
        public decimal ToppPrice { get; set; } 

        public IEnumerable<Category> ToppCategories => ToppContext.GetToppCategories();


        IEnumerable<Topping> ToppingsOfSpecCategory { get; set; }

        public decimal ProductPrice { get; set; }

        public decimal TotalPrice { get; set; } 

        public long ProductSizeId { get; set; }

        public string ProductSize { get; set; }

        public long SelectedCatId { get; set; }

        public string SelectedToppingCategoryName { get; set; }

        
        Dictionary<string, int> allAddedToppings = new Dictionary<string, int>();
       
        public void ChangeProductSize(long sizeId, long categoryId, string pizzaSize)
        {
            ProductPrice = PizzaContext.GetPizzaPrice(sizeId, categoryId);
            ProductSize = pizzaSize;
            ProductSizeId = sizeId;
            TotalPrice = ProductPrice;

            allAddedToppings.Clear();

           
        }

        public void AddTopping(string toppName)
        {
            decimal price = ToppingPrice(SelectedCatId, ProductSizeId);
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
                decimal toppPrice = ToppingPrice(catId, ProductSizeId) * quantity;
                TotalPrice -= toppPrice;

                allAddedToppings.Remove(toppName);
            }
           
        }

        public string GetClass(string size)
        {
            return ProductSize == size ? "success" : "outline-dark";
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

          
        }

        public decimal ToppingPrice(long toppCatId, long sizeId)
        {
           
            ToppPrice = ToppContext.GetToppPrice(toppCatId, sizeId);
            return ToppPrice;
        }
    }
}
