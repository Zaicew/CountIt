using System;
using System.Collections.Generic;
using System.Text;

namespace CountIt
{
    class MealInDay : Meal
    {
        private double weight;
        public MealInDay() : base()
        {
            this.weight = 0;
        }
        public MealInDay(double weight, string name) : base(name)
        {
            this.weight = weight;
        } 

    }
}
