using System;
using System.Collections.Generic;
using System.Text;

namespace CountIt
{
    class Meal
    {
        public static int idStatic;
        public int Id { get; set; }
        public string nameOfMeal { get; set; }
        public double TotalKcal { get; set; }
        public double TotalFat { get; set; }
        public double TotalProtein { get; set; }
        public double TotalCarbs { get; set; }
        public List<double> HowManyGramsOfProduct { get; set; }
        public List<Item> ProductsInMeal { get; set; }
        public Meal()
        {
            this.Id = idStatic++;
            this.TotalKcal = 0;
            this.TotalFat = 0;
            this.TotalProtein = 0;
            this.TotalCarbs = 0;
            this.nameOfMeal = "unsignedMealName";
            this.ProductsInMeal = new List<Item>();
            this.HowManyGramsOfProduct = new List<double>();
        }        
        public Meal(string name)
        {
            this.Id = idStatic++;
            this.TotalKcal = 0;
            this.TotalFat = 0;
            this.TotalProtein = 0;
            this.TotalCarbs = 0;
            this.nameOfMeal = name;
            this.ProductsInMeal = new List<Item>();
            this.HowManyGramsOfProduct = new List<double>();
        }
    }
}
