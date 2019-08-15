using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CRUDelicious.Models;
using Microsoft.EntityFrameworkCore;

namespace CRUDelicious.Controllers
{
    public class HomeController : Controller
    {
        private MyContext dbContext;

        public HomeController(MyContext context)
        {
            dbContext = context;
        }

        [HttpGet("")]     
        public IActionResult Index()
        {
            List<Dish> All_dishes = dbContext.Dishes.ToList();
            return View("Index", All_dishes);
        }

        
        [HttpGet("new")]
        public IActionResult New()
        {
            return View("New");
        }

        
        [HttpPost("create")]
        public IActionResult Create(Dish My_dish)
        {
            if (ModelState.IsValid) // If validations are okay
            { 
                System.Console.WriteLine("VALID *********");
                dbContext.Add(My_dish); // Add the dish to the DB
                dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            else    // If validations are not okay
            {
                System.Console.WriteLine("INVALID ********");
                return View("New");
            }

        }

        [HttpGet("{DishId}")]
        public IActionResult View(int DishId)
        {
            Dish One_Dish = dbContext.Dishes.FirstOrDefault(Dish => Dish.DishId == DishId);
            return View("View", One_Dish);
        }


        [HttpGet("edit/{DishId}")]
        public IActionResult Edit(int DishId)
        {
            Dish One_Dish = dbContext.Dishes.FirstOrDefault(Dish => Dish.DishId == DishId);
            System.Console.WriteLine("23333333333");
            return View("Edit", One_Dish);
        }

        [HttpPost("update")]
        public IActionResult Update(Dish dishFromEditHTML)
        {
            if (ModelState.IsValid) // If validations are okay
            { 
                System.Console.WriteLine("WORKING *********");
                Dish One_Dish = dbContext.Dishes.FirstOrDefault(dishFromDB => dishFromDB.DishId == dishFromEditHTML.DishId);
                // Then we may modify properties of this tracked model object
                One_Dish.Dish_Name = dishFromEditHTML.Dish_Name;
                One_Dish.Chef_Name = dishFromEditHTML.Chef_Name; 
                One_Dish.Calories = dishFromEditHTML.Calories;
                One_Dish.Tastiness = dishFromEditHTML.Tastiness;
                One_Dish.Description = dishFromEditHTML.Description;

                // Finally, .SaveChanges() will update the DB with these new values
                dbContext.SaveChanges();

                return Redirect($"{One_Dish.DishId}");
            }
            else    // If validations are not okay
            {
                System.Console.WriteLine("NOT WORKING ********");
                return View("Edit");
            }

        }
        [HttpGet("delete/{DishId}")]
        public IActionResult DeleteDish(int DishId)
        {
            // Like Update, we will need to query for a single user from our Context object
            Dish One_Dish = dbContext.Dishes.SingleOrDefault(dishFromDB => dishFromDB.DishId == DishId);
            
            // Then pass the object we queried for to .Remove() on Users
            dbContext.Dishes.Remove(One_Dish);
            
            // Finally, .SaveChanges() will remove the corresponding row representing this User from DB 
            dbContext.SaveChanges();
            // Other code
            return RedirectToAction("Index");
        }

    } 
}
