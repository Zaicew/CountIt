using System;
using System.Collections.Generic;
using System.Text;

namespace CountIt.Domain.Entity
{
    public class ItemInMeal : Item
    {
        public double Weight { get; set; }
        public ItemInMeal(Item item, double weight) : base(item)
        {
            this.Weight = weight;
        }
    }
}
