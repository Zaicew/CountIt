using CountIt.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace CountIt.Domain.Entity
{
    public class Meal : BaseEntity
    {
        public static int id;
        public string NameOfMeal { get; set; }
        public double TotalKcal { get; set; }
        public double TotalFat { get; set; }
        public double TotalProtein { get; set; }
        public double TotalCarbs { get; set; }
        public double TotalGrams { get; set; }
        public List<ItemInMeal> ProductsInMeal { get; set; }
        public Meal()
        {
            this.Id = id++;
            this.TotalKcal = 0;
            this.TotalFat = 0;
            this.TotalProtein = 0;
            this.TotalCarbs = 0;
            this.NameOfMeal = "unsignedMealName";
            this.ProductsInMeal = new List<ItemInMeal>();
            this.TotalGrams = 0;
        }        
        public Meal(string name)
        {
            this.Id = id++;
            this.TotalKcal = 0;
            this.TotalFat = 0;
            this.TotalProtein = 0;
            this.TotalCarbs = 0;
            this.NameOfMeal = name;
            this.ProductsInMeal = new List<ItemInMeal>();
            this.TotalGrams = 0;
        }        
    }
}
