using la_mia_pizzeria_crud_mvc.Models;
using Microsoft.AspNetCore.Mvc;
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
            var menuItem = ctx.Pizzas.Find(id);

            return View(menuItem);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Pizza data) 
        {
            if (!ModelState.IsValid)
            {
                return View("Create", data);
            }

            using (PizzaContext ctx = new PizzaContext())
            {
                Pizza newPizza = new Pizza();
                newPizza.Img = data.Img;
                newPizza.Name = data.Name;
                newPizza.Description = data.Description;
                newPizza.Price = data.Price;

                ctx.Pizzas.Add(newPizza);
                ctx.SaveChanges();

                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
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
    }
}