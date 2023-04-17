using la_mia_pizzeria_crud_mvc.Models;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Roles = "ADMIN")]
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
        [Authorize(Roles = "ADMIN")]
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
        [Authorize(Roles = "ADMIN")]
        public IActionResult Update(long id)
        {
            using (PizzaContext ctx = new PizzaContext())
            {
                Pizza _pizza = ctx.Pizzas.Where(pizza => pizza.Id == id).Include(p => p.Ingredients).FirstOrDefault();

                if (_pizza == null)
                {
                    return NotFound();
                }
                else
                {
                    List<Category> categories = ctx.Categories.ToList();
                    List<Ingredient> ingredients = ctx.Ingredients.ToList();
                    List<SelectListItem> listIngredients = new List<SelectListItem>();

                    foreach (Ingredient ingredient in ingredients)
                    {
                        listIngredients.Add(new SelectListItem()
                        { Text = ingredient.Name, Value= ingredient.Id.ToString(), Selected = _pizza.Ingredients.Any(p => p.Id == ingredient.Id) });
                    }

                    PizzaFormModel model = new PizzaFormModel();
                    model.Pizza = _pizza;
                    model.Categories = categories;
                    model.Ingredients = listIngredients;

                    return View("Update", model);
                }
            }
        }

        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        public IActionResult Update(long id, PizzaFormModel data) 
        {
            if (!ModelState.IsValid)
            {
                using PizzaContext ctx = new PizzaContext();
                {
                    List<Category> categories = ctx.Categories.ToList();
                    List<Ingredient> ingredients = ctx.Ingredients.ToList();
                    List<SelectListItem> listIngredients = new List<SelectListItem>();

                    foreach (Ingredient ingredient in ingredients)
                    {
                        listIngredients.Add(new SelectListItem()
                        { Text = ingredient.Name, Value = ingredient.Id.ToString() });
                    }

                    data.Pizza = ctx.Pizzas.Where(p => p.Id == id).FirstOrDefault();
                    data.Categories = categories;
                    data.Ingredients = listIngredients;

                    return View("Update", data);
                }
            }

            using (PizzaContext ctx = new PizzaContext())
            {
                Pizza _pizza = ctx.Pizzas.Where(pizza => pizza.Id == id).Include(p => p.Ingredients).FirstOrDefault();

                if (_pizza == null)
                {
                    return NotFound();
                }
                _pizza.Name = data.Pizza.Name;
                _pizza.Description = data.Pizza.Description;
                _pizza.Price = data.Pizza.Price;
                _pizza.Img = data.Pizza.Img;
                _pizza.CategoryId = data.Pizza.CategoryId;

                _pizza.Ingredients.Clear();

                if (data.SelectedIngredients != null)
                {
                    foreach (string selectedIngredientId in data.SelectedIngredients)
                    {
                        int selectedIntIngredientId = int.Parse(selectedIngredientId);
                        Ingredient ingredient = ctx.Ingredients.Where(p => p.Id == selectedIntIngredientId).FirstOrDefault();
                        _pizza.Ingredients.Add(ingredient);
                    }
                }

                ctx.SaveChanges();
                return RedirectToAction("Index");
            }
        }

        [Authorize(Roles = "ADMIN")]
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