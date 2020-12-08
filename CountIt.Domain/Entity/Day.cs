using CountIt.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace CountIt.Domain.Entity
{
    public class Day : BaseEntity
    {
        [XmlElement("DateTime")]
        public DateTime DateTime { get; set; }
        //public List<Meal> MealList { get; set; }
        [XmlElement("mealList")]
        public Meal[] mealList = new Meal[5];
        [XmlElement("TotalKcal")]
        public double TotalKcal { get; set; }
        [XmlElement("TotalFat")]
        public double TotalFat { get; set; }
        [XmlElement("TotalProtein")]
        public double TotalProtein { get; set; }
        [XmlElement("TotalCarbs")]
        public double TotalCarbs { get; set; }
        [XmlElement("TotalWeightInGrams")]
        public double TotalWeightInGrams { get; set; }


        public Day()
            : base()
        {
            //Id = 0;
            //DateTime = DateTime.Now;
            //mealList = new Meal[6];
            //TotalCarbs = 0;
            //TotalFat = 0;
            //TotalKcal = 0;
            //TotalProtein = 0;
            //TotalWeightInGrams = 0;
        }
        public Day(DateTime datetime, int id)
            : base(id)
        {
            DateTime = datetime;
            mealList = new Meal[6];
            TotalCarbs = 0;
            TotalFat = 0;
            TotalKcal = 0;
            TotalProtein = 0;
            TotalWeightInGrams = 0;
        }
    }
}
