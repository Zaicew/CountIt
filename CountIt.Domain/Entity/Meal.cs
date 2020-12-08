using CountIt.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace CountIt.Domain.Entity
{
    public class Meal : BaseEntity
    {
        [XmlElement("NameOfMeal")]
        public string NameOfMeal { get; set; }
        [XmlElement("TotalKcal")]
        public double TotalKcal { get; set; }
        [XmlElement("TotalFat")]
        public double TotalFat { get; set; }
        [XmlElement("TotalProtein")]
        public double TotalProtein { get; set; }
        [XmlElement("TotalCarbs")]
        public double TotalCarbs { get; set; }
        [XmlElement("Weight")]
        public double Weight { get; set; }
        [XmlElement("IsVisible")]
        public bool IsVisible { get; set; }
        [XmlElement("ProductsInMeal")]
        public List<ItemInMeal> ProductsInMeal { get; set; }
        public Meal()
        {
            //Id = 0;
            //TotalKcal = 0;
            //TotalFat = 0;
            //TotalProtein = 0;
            //TotalCarbs = 0;
            //NameOfMeal = "unsignedMealName";
            //ProductsInMeal = new List<ItemInMeal>();
            //Weight = 0;
            //IsVisible = true;
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
            Weight = 0;
            IsVisible = true;
        }        
    }
}
