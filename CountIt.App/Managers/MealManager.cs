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
        private IService<Meal> _mealservice;
        public MealManager(MealService mealService)
        {
            _mealservice = mealService;
        }
        public bool IsNameMealExist(string nameOfMeal)
        {
            foreach (var item in _mealservice.Items)
            {
                if (item.NameOfMeal == nameOfMeal)
                    return false;
            }
            return true;
        }

        public void ShowAllProductsFromMeal(Meal mealHolder)
        {
            foreach (var item in mealHolder.ProductsInMeal)
                Console.WriteLine($"ID: {item.Id}, name: {item.Name}");
        }
    }
}
