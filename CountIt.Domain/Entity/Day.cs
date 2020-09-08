using CountIt.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace CountIt.Domain.Entity
{
    public class Day : BaseEntity
    {
        private static int id;
        public DateTime dateTime { get; set; }
        public List<Meal> mealList { get; set; }
        public double TotalKcal { get; set; }
        public double TotalFat { get; set; }
        public double TotalProtein { get; set; }
        public double TotalCarbs { get; set; }
        public double TotalWeightInGrams { get; set; }

        public Day()
        {
            this.Id = id++;
            this.dateTime = DateTime.Now;
            this.TotalCarbs = 0;
            this.TotalFat = 0;
            this.TotalKcal = 0;
            this.TotalProtein = 0;
            this.mealList = new List<Meal>();
            this.TotalWeightInGrams = 0;
        }
        public Day(DateTime datetime)
        {
            this.Id = id++;
            this.dateTime = datetime;
            this.TotalCarbs = 0;
            this.TotalFat = 0;
            this.TotalKcal = 0;
            this.TotalProtein = 0;
            this.mealList = new List<Meal>();
            this.TotalWeightInGrams = 0;
        }
    }
}
