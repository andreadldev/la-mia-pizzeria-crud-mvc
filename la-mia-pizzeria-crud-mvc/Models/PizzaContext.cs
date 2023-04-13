using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace la_mia_pizzeria_crud_mvc.Models
{using Microsoft.EntityFrameworkCore;
    public class PizzaContext : DbContext
    {
        public DbSet<Pizza> Pizzas { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=PizzaMenu;Integrated Security=True;TrustServerCertificate=true;Encrypt=false;");
        }

        public void Seed()
        {
            if (!Pizzas.Any())
            {
                var menuList = new Pizza[]
                {
                    new(
                        img: "https://banner2.cleanpng.com/20180910/lpw/kisspng-california-style-pizza-pizza-margherita-sicilian-p-margarita-pizza-ceparanos-ny-pizza-5b9676c2e3e403.4168630415365874589335.jpg",
                        name: "Margherita",
                        description: "Lorem ipsum dolor sit amet, consectetur adipisci elit.",
                        price: 5.00
                    ),
                    new(
                        img: "https://png2.cleanpng.com/sh/ab961eeff2810fcce6d49f209e8b6073/L0KzQYm3UsAzN6V0iZH0aYP2gLBuTgNqa5pxgdN3LYDsisvoTfl1aZ1uedC2Y4Xsg7r1hb1xbaF1feR4bnmwhLL5lPUubp1mReJucIDogrF1ib1xcatBeZ8AYUe3crTqV8I1bmdpUZCCMkC3QIS6UME2OWg6UasENEO0RYK7TwBvbz==/kisspng-sicilian-pizza-italian-cuisine-pepperoni-tarte-fla-pepperoni-pizza-5a74bcc724f6d9.7204033015175999431514.png",
                        name: "Diavola",
                        description: "Lorem ipsum dolor sit amet, consectetur adipisci elit.",
                        price: 5.00
                    ),
                    new(
                        img: "https://img.freepik.com/premium-photo/pepperoni-pizza-cheese-pizza-food-pizza-pizza-pepperoni-mozzarella-mozzarella-cheese-cheese_812450-5.jpg?w=740",
                        name: "Peperoni",
                        description: "Lorem ipsum dolor sit amet, consectetur adipisci elit.",
                        price: 6.00
                    ),
                    new(
                        img: "https://as2.ftcdn.net/v2/jpg/05/63/37/57/1000_F_563375714_jB0mvLRvNbdqpNFBtvXqsBV2BwEPUsrK.webp",
                        name: "California",
                        description: "Lorem ipsum dolor sit amet, consectetur adipisci elit.",
                        price: 6.50
                    ),
                    new(
                        img: "https://img.freepik.com/premium-photo/delicious-classic-italian-pizza-with-mozzarella_79762-2653.jpg?w=740",
                        name: "Prosciutto",
                        description: "Lorem ipsum dolor sit amet, consectetur adipisci elit.",
                        price: 6.50
                    ),
                    new(
                        img: "https://www.pngkit.com/png/full/254-2542315_hot-veggie-pizza.png",
                        name: "Vegetariana",
                        description: "Lorem ipsum dolor sit amet, consectetur adipisci elit.",
                        price: 7.00
                    )
                };
                Pizzas.AddRange(menuList);
            }

            if (!Categories.Any())
            {
                var categoryList = new Category[]
                {
                                            new()
                        {
                            Name = "Classiche",
                        },
                        new()
                        {
                            Name = "Vegetariane"
                        },
                        new()
                        {
                            Name = "Di Mare"
                        }
                    };
                Categories.AddRange(categoryList);
            }
                SaveChanges();
            }
        }
    }

