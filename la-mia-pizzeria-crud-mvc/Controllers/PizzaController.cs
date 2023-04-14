using la_mia_pizzeria_crud_mvc.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace la_mia_pizzeria_crud_mvc.Controllers
{
    public class PizzaController : Controller
    {
        public IActionResult Index()
        {
            ViewData["Title"] = "Homepage";
            using var ctx = new PizzaContext();

            var menu = ctx.Pizzas.ToArray();
            if (!ctx.Pizzas.Any())
            {
                ViewData["Message"] = "Nessun risultato trovato";
            }
            return View("Index", menu);
        }

        public IActionResult Show(long id)
        {
            using var ctx = new PizzaContext();
            var menuItem = ctx.Pizzas.Include(p => p.Category).Include(p => p.Ingredients).First(p => p.Id == id);

            return View(menuItem);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PizzaFormModel data) 
        {
            if (!ModelState.IsValid)
            {
                using (PizzaContext ctx = new PizzaContext())
                {
                    data.Categories = ctx.Categories.ToList();
                    List<Ingredient> ingredients = ctx.Ingredients.ToList();
                    List<SelectListItem> listIngredients = new List<SelectListItem>();

                    foreach (Ingredient ingredient in ingredients)
                    {
                        listIngredients.Add(new SelectListItem()
                        {
                            Text = ingredient.Name,
                            Value = ingredient.Id.ToString(),
                        });
                    }
                    data.Ingredients = listIngredients;
                }
                return View("Create", data);
            }

            using (PizzaContext ctx = new PizzaContext())
            {
                Pizza newPizza = new Pizza();
                newPizza.Img = data.Pizza.Img;
                newPizza.Name = data.Pizza.Name;
                newPizza.Description = data.Pizza.Description;
                newPizza.Price = data.Pizza.Price;
                newPizza.CategoryId = data.Pizza.CategoryId;
                newPizza.Ingredients = new List<Ingredient>();

                if (data.SelectedIngredients != null)
                {
                    foreach (string selectedIngredientId in data.SelectedIngredients)
                    {
                        int selectedIntIngredientId = int.Parse(selectedIngredientId);
                        Ingredient ingredient = ctx.Ingredients.Where(p => p.Id == selectedIntIngredientId).FirstOrDefault();
                        newPizza.Ingredients.Add(ingredient);
                    }
                }

                ctx.Pizzas.Add(newPizza);
                ctx.SaveChanges();

                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            using (var ctx = new PizzaContext())
            {
                List<Category> categories = ctx.Categories.ToList();
                List<Ingredient> ingredients = ctx.Ingredients.ToList();

                PizzaFormModel model = new PizzaFormModel();
                model.Pizza = new Pizza();
                model.Categories = categories;
                List<SelectListItem> listIngredients = new List<SelectListItem>();
                foreach (Ingredient ingredient in ingredients)
                {
                    listIngredients.Add(new SelectListItem()
                    {
                       Text = ingredient.Name, Value = ingredient.Id.ToString(),
                    });
                }
                model.Ingredients = listIngredients;

                return View("Create", model);
            }
        }

        [HttpGet]
        public IActionResult Update(long id)
        {
            using (PizzaContext ctx = new PizzaContext())
            {
                Pizza _pizza = ctx.Pizzas.Where(pizza => pizza.Id == id).FirstOrDefault();

                if (_pizza == null)
                {
                    return NotFound();
                }
                else
                {
                    return View("Update", _pizza);
                }
            }
        }

        [HttpPost]
        public IActionResult Update(long id, Pizza pizza) 
        {
            if (!ModelState.IsValid)
            {
                return View("Update", pizza);
            }

            using (PizzaContext ctx = new PizzaContext())
            {
                Pizza _pizza = ctx.Pizzas.Where(pizza => pizza.Id == id).FirstOrDefault();

                if (_pizza == null)
                {
                    return NotFound();
                }
                _pizza.Name = pizza.Name;
                _pizza.Description = pizza.Description;
                _pizza.Price = pizza.Price;
                _pizza.Img = pizza.Img;

                ctx.SaveChanges();
                return RedirectToAction("Index");
            }
        }

        public IActionResult Delete(long id) 
        {
            using (PizzaContext ctx = new PizzaContext())
            {
                Pizza _pizza = ctx.Pizzas.Where(pizza => pizza.Id == id).FirstOrDefault();

                if (_pizza == null)
                {
                    return NotFound();
                }

                ctx.Pizzas.Remove(_pizza);
                ctx.SaveChanges();
                return RedirectToAction("Index");
            }
        }
    }
}