using CountIt.App.Abstract;
using CountIt.App.Concrete;
using CountIt.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace CountIt.App.Managers
{
    public class MealManager
    {
        //private readonly IService<Meal> _mealservice;
        //public MealManager(MealService mealService)
        //{
        //    _mealservice = mealService;
        //} stare -> można usunąć mealservice?
        public MealManager()
        {
        }

        public void ShowAllProductsFromMeal(Meal mealHolder)
        {
            foreach (var item in mealHolder.ProductsInMeal)
                Console.WriteLine($"ID: {item.Id}, name: {item.Name}");
        }
    }
}
