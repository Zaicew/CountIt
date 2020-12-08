using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace CountIt.Domain.Entity
{
    public class ItemInMeal : Item
    {
        [XmlElement("Weight")]
        public double Weight { get; set; }
        public ItemInMeal() : base()
        {
            //this.Weight = 0;
        }
        public ItemInMeal(Item item, double weight) : base(item)
        {
            this.Weight = weight;
        }
    }
}
