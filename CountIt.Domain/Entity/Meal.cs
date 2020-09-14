using CountIt.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace CountIt.Domain.Entity
{
    public class Meal : BaseEntity
    {
        public string NameOfMeal { get; set; }
        public double TotalKcal { get; set; }
        public double TotalFat { get; set; }
        public double TotalProtein { get; set; }
        public double TotalCarbs { get; set; }
        public double TotalGrams { get; set; }
        public List<ItemInMeal> ProductsInMeal { get; set; }
        public Meal()
        {
            Id = 0;
            TotalKcal = 0;
            TotalFat = 0;
            TotalProtein = 0;
            TotalCarbs = 0;
            NameOfMeal = "unsignedMealName";
            ProductsInMeal = new List<ItemInMeal>();
            TotalGrams = 0;
        }        
        public Meal(string name, int id)
           : base(id)
        {
            TotalKcal = 0;
            TotalFat = 0;
            TotalProtein = 0;
            TotalCarbs = 0;
            NameOfMeal = name;
            ProductsInMeal = new List<ItemInMeal>();
            TotalGrams = 0;
        }        
    }
}
