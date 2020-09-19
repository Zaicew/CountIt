using CountIt.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace CountIt.Domain.Entity
{
    public class Day : BaseEntity
    {
        public DateTime DateTime { get; set; }
        public List<Meal> MealList { get; set; }
        public double TotalKcal { get; set; }
        public double TotalFat { get; set; }
        public double TotalProtein { get; set; }
        public double TotalCarbs { get; set; }
        public double TotalWeightInGrams { get; set; }


        public Day()
            : base()
        {
            Id = 0;
            DateTime = DateTime.Now;
            TotalCarbs = 0;
            TotalFat = 0;
            TotalKcal = 0;
            TotalProtein = 0;
            MealList = new List<Meal>();
            TotalWeightInGrams = 0;
        }
        public Day(DateTime datetime, int id)
            : base(id)
        {
            DateTime = datetime;
            TotalCarbs = 0;
            TotalFat = 0;
            TotalKcal = 0;
            TotalProtein = 0;
            MealList = new List<Meal>();
            TotalWeightInGrams = 0;
        }
    }
}
