﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CountIt
{
    class ItemInMeal : Item
    {
        public double Weight { get; set; }
        public ItemInMeal() : base ()
        {
            this.Weight = 0;
        }

        public ItemInMeal(Item item, double weight) : base(item)
        {
            this.Weight = weight;
        }
    }
}
