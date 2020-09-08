using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CountIt
{
    class MealService
    {
        private List<Meal> Meals { get; set; } 
        public MealService()
        {
            Meals = new List<Meal>();
        }
        public bool isNameMealExist(string nameOfMeal)
        {
            foreach (var item in Meals)
            {
                if (item.nameOfMeal == nameOfMeal)
                    return false;
            }
            return true;
        }

        internal void ShowAllProductsFromMeal(Meal mealHolder)
        {
            foreach (var item in mealHolder.ProductsInMeal)
                Console.WriteLine($"ID: {item.Id}, name: {item.Name}");
        }
    }
}
