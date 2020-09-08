using CountIt.App.Common;
using CountIt.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CountIt.App.Concrete
{
    public class MealService : BaseService<Meal>
    {
        public List<Meal> Meals { get; set; } 
        public MealService()
        {
            Meals = new List<Meal>();
        }
        public bool IsNameMealExist(string nameOfMeal)
        {
            foreach (var item in Meals)
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
