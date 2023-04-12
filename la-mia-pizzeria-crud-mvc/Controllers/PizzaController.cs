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
    }
}