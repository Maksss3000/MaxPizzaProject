using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MaxPizzaProject.Models
{
    public static class SeedData
    {
        public static void Seed(PizzeriaDbContext context)
        {
            //Если существуют ожидающие Миграции.
            if (context.Database.GetPendingMigrations().Any())
            {
                
                //Применение всех ожидающих миграций к БД.
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

                /*


                //context.Prices.AddRange(p1,p2,p3);
                context.Sizes.AddRange(small, large);

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

                CategorySize catSizeL1 = new CategorySize
                {
                    Size = large,
                    Price = 66m
                };


                CategorySize catSizeL2 = new CategorySize
                {
                    Size = large,
                    Price = 90m
                };

                */

                // context.CategoriesSizes.AddRange(catSizeL1, catSizeL2, catSizeS1, catSizeS2);
                //context.SaveChanges();

                Category classic = new Category
                {
                    //Sizes = new List<Size> { small, large },
                    Description = "Classic Pizza",
                    Name = "Classic",
                    Type="Pizza"
                    
                   // CategoriesSizes = new List<CategorySize>{ catSizeS2, catSizeL1 }
                   // CategoriesSizes = new CategorySize {}
                    
                    
                    //Prices=new List<Price> { p1,p3}
                };




               

                //tom.Enrollments.Add(new Enrollment { Course = algorithms, EnrollmentDate = DateTime.Now });
                //tom.Courses.Add(basics);

                
                Category hotty = new Category
                {
                    //Sizes = new List<Size> { small, large },
                    Description = "Ruleta Pizza",
                    Name = "Ruleta",
                    Type="Pizza"
                    //CategoriesSizes = new List<CategorySize> { catSizeS2, catSizeL1 }
                    // CategoriesSizes = new CategorySize {}


                    //Prices=new List<Price> { p1,p3}
                };

                Category mix = new Category
                {
                    
                   // Sizes = new List<Size> { small, large },
                    Description = "Pizza Mix",
                    Name = "Mix",
                    Type="Pizza"
                    //CategoriesSizes=new List<CategorySize> { catSizeS2,catSizeL1}
                    //Prices=new List<Price> { p1,p2}
                };

                Category fish = new Category
                {

                    // Sizes = new List<Size> { small, large },
                    Description = "Pizza with Fresh Fish",
                    Name = "Fish",
                    Type = "Pizza"
                    //CategoriesSizes=new List<CategorySize> { catSizeS2,catSizeL1}
                    //Prices=new List<Price> { p1,p2}
                };

                Category meatTopping = new Category
                {
                    Description = "Meat Topping",
                    Name = "Meat Topping",
                    Type="Topping"
                };

                Category cheeseTopping = new Category
                {
                    Description = "Cheese Topping",
                    Name = "Cheese",
                    Type="Topping"
                    
                };

                Category drink = new Category
                {
                  
                    Name = "Drink",
                    Type = "Drink"

                };


                Topping peperony = new Topping
                {
                    InStock = true,
                    Name = "Peperony",
                    Category=meatTopping,
                

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
                    Category=cheeseTopping
                };

                

                context.Categories.AddRange(classic, mix, hotty, 
                                            cheeseTopping, meatTopping,fish,drink);
                context.Toppings.AddRange(peperony, parmezan,bakar);
                

                

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



                //classic.CategoriesSizes.Add(catSizeS1);
                //mix.CategoriesSizes.Add(catSizeS1);
                drink.CategoriesSizes.AddRange(sizesForDrink);
                fish.CategoriesSizes.AddRange(sizesForFish);
                classic.CategoriesSizes.Add(catSizeS1);
                classic.CategoriesSizes.Add(new CategorySize { Size =large , Price = 55m });
                mix.CategoriesSizes.Add(new CategorySize { Size = small,  Price = 25m });
                mix.CategoriesSizes.Add(new CategorySize { Size = large, Price = 55m });
                hotty.CategoriesSizes.AddRange(sizes);

                cheeseTopping.CategoriesSizes.Add(new CategorySize { Size = small, Price = 5 });
                cheeseTopping.CategoriesSizes.Add(new CategorySize { Size = large, Price = 10 });
                //cheeseTopping.CategoriesSizes.Add(new CategorySize { Size = medium, Price = 7 });

                meatTopping.CategoriesSizes.Add(new CategorySize { Size = small, Price = 6 });
                meatTopping.CategoriesSizes.Add(new CategorySize { Size = large, Price = 10 });

                //classic.CategoriesSizes.Add(catSizeL1);

                //mix.CategoriesSizes.Add(catSizeS1);
                //mix.CategoriesSizes.Add(catSizeL2);

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
