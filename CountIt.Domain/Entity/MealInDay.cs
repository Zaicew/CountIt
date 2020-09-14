using System;
using System.Collections.Generic;
using System.Text;

namespace CountIt.Domain.Entity
{
    public class MealInDay : Meal
    {
        public double Weight { get; set; }
        public MealInDay() : base()
        {
            Weight = 0;
        }
        public MealInDay(double weight, string name, int id) : base(name, id + 1)
        {
            Weight = weight;
        } 

    }
}
