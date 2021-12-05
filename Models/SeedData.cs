using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace MaxPizzaProject.Models
{
    public static class SeedData
    {
        public static void Seed(PizzeriaDbContext context)
        {
            //If there is some waiting Migrations.
            if (context.Database.GetPendingMigrations().Any())
            {

                //Using All Waiting Migrations.(Like Update)
                //dotnet ef database update --context [DBContext]
                context.Database.Migrate();
            }

            if (!context.Pizzas.Any())
            {
               
                Size small = new Size
                {
                    TheSize = "Small",
                };

                Size medium = new Size
                {
                    TheSize = "Medium"
                };

                Size large = new Size
                {
                    TheSize = "Large",

                };

                Size smallDrink = new Size
                {
                    TheSize = "0.5L"
                };

                Size largeDrink = new Size
                {
                    TheSize = "1.5L"
                };

               

                Category classic = new Category
                {
                   
                    Description = "Classic Pizza",
                    Name = "Classic",
                    Type = "Pizza"

                   
                };

                Category hotty = new Category
                {
                   
                    Description = "Ruleta Pizza",
                    Name = "Ruleta",
                    Type = "Pizza"
                   
                };

                Category mix = new Category
                { 
                    Description = "Pizza Mix",
                    Name = "Mix",
                    Type = "Pizza"
                  
                };

               

                Category fish = new Category
                {   
                    Description = "Pizza with Fresh Fish",
                    Name = "Fish",
                    Type = "Pizza"
                 
                };

                Category meatTopping = new Category
                {
                    Description = "Meat Topping",
                    Name = "Meat",
                    Type = "Topping"
                };

                Category cheeseTopping = new Category
                {
                    Description = "Cheese Topping",
                    Name = "Cheese",
                    Type = "Topping"

                };

                Category drink = new Category
                {

                    Name = "Drink",
                    Type = "Drink"

                };

                Category iceCream = new Category
                {
                    Name = "Ice-Cream",
                    Type = "Snack"
                };

                Topping peperony = new Topping
                {
                    InStock = true,
                    Name = "Peperony",
                    Category = meatTopping,


                };

                Topping bakar = new Topping
                {
                    InStock = true,
                    Name = "Bakar",
                    Category = meatTopping
                };

                Topping parmezan = new Topping
                {
                    InStock = true,
                    Name = "Parmezan",
                    Category = cheeseTopping
                };



                context.Categories.AddRange(classic, mix, hotty,
                                            cheeseTopping, meatTopping, fish, drink,iceCream);
                context.Toppings.AddRange(peperony, parmezan, bakar);




                CategorySize catSizeS1 = new CategorySize
                {
                    Size = small,
                    Price = 25m
                };

                CategorySize catSizeS2 = new CategorySize
                {
                    Size = small,
                    Price = 35m
                };

                CategorySize iceCreamCatSize = new CategorySize
                {
                    Size = small,
                    Price = 10m
                };

                CategorySize[] sizes = new CategorySize[]
                {
                    new CategorySize
                    {
                        Size=small,
                        Price=85
                    },
                    new CategorySize
                    {
                        Size=large,
                        Price=100
                    }
                };


                CategorySize[] sizesForFish = new CategorySize[]
               {
                    new CategorySize
                    {
                        
                        Size=small,
                        Price=75
                    },
                    new CategorySize
                    {
                        Size=large,
                        Price=85
                    },

               };


                CategorySize[] sizesForDrink = new CategorySize[]
               {
                    new CategorySize
                    {
                        Size=smallDrink,
                        Price=7.90M
                    },
                    new CategorySize
                    {
                        Size=largeDrink,
                        Price=12.90M
                    },

               };

                drink.CategoriesSizes.AddRange(sizesForDrink);
                fish.CategoriesSizes.AddRange(sizesForFish);
                classic.CategoriesSizes.Add(catSizeS1);
                classic.CategoriesSizes.Add(new CategorySize { Size = large, Price = 55m });
                mix.CategoriesSizes.Add(new CategorySize { Size = small, Price = 25m });
                mix.CategoriesSizes.Add(new CategorySize { Size = large, Price = 55m });
                hotty.CategoriesSizes.AddRange(sizes);

                cheeseTopping.CategoriesSizes.Add(new CategorySize { Size = small, Price = 5 });
                cheeseTopping.CategoriesSizes.Add(new CategorySize { Size = large, Price = 10 });
                

                meatTopping.CategoriesSizes.Add(new CategorySize { Size = small, Price = 6 });
                meatTopping.CategoriesSizes.Add(new CategorySize { Size = large, Price = 10 });


                iceCream.CategoriesSizes.Add(iceCreamCatSize);
               

                context.Drinks.AddRange(new Drink
                {
                    Name = "Coca-Cola",
                    ImagePath = "img/Drinks/cola.jpg",
                    Category = drink,
                    InStock = true,

                },


                new Drink
                {
                    Name = "Sprite",
                    ImagePath = "img/Drinks/sprite.jpg",
                    Category = drink,
                    InStock = true,
                },

                new Drink
                {
                    Name = "Fanta",
                    ImagePath = "img/Drinks/fanta.jpg",
                    Category = drink,
                    InStock = true,

                }

                );


                context.Snacks.Add(new Snack
                {
                    Name="Chocolate-Ice-Creame",
                    Category = iceCream,
                    ImagePath = "img/Snacks/chocolateIceCream.jpg",
                    InStock = true,
                    Description = "Choclate-Ice-Cream"

                });

                context.Pizzas.AddRange(new Pizza
                {
                    Name="Margarita",
                    ImagePath= "img/Pizzas/margarita.jpg",
                    Category =classic,
                    InStock=true,
                    Description="Tasty Classic Pizza"
                    
                },
                new Pizza
                {
                    Name="MaxMeat",
                    ImagePath = "img/Pizzas/maxmeat.jpg",
                    Category =mix,
                    InStock=true,
                    Description="Super Tasty Pizza with Cheacken meat."
                    
                },

                new Pizza
                {
                    Name="MaxVegetables",
                    ImagePath= "img/Pizzas/maxvegetables.jpg",
                    Category =mix,
                    InStock=true,
                    Description="Pizza with Fresh Vegetables."
                }
                ,
                new Pizza
                {
                    Name="MaxHot",
                    ImagePath= "img/Pizzas/maxhot.jpg",
                    Category =hotty,
                    InStock=true,
                    Description="Very Spicy Pizza , with Roasted Red Peppers."

                },
                 new Pizza
                 {
                     Name = "TunaFresh",
                     ImagePath = "img/Pizzas/fish.jpg",
                     Category = fish,
                     InStock = true,
                     Description = "Pizza with fresh Tuna."

                 },
                  new Pizza
                  {
                      Name = "MaxOlives",
                      ImagePath = "img/Pizzas/maxolives.jpg",
                      Category = mix,
                      InStock = true,
                      Description = "Pizza with our special Cream fresh , olives and tomatoes ."
                  }

                );

                context.SaveChanges();
            }
        }

    }
}
