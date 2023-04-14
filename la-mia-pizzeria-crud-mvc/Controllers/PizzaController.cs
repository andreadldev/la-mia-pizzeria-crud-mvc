using la_mia_pizzeria_crud_mvc.Models;
using Microsoft.AspNetCore.Mvc;
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
            var menuItem = ctx.Pizzas.Include(p => p.Category).First(p => p.Id == id);

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

                PizzaFormModel model = new PizzaFormModel();
                model.Pizza = new Pizza();
                model.Categories = categories;

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