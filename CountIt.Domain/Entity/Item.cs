using CountIt.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace CountIt.Domain.Entity
{
    public class Item : BaseEntity
    {
        [XmlElement("Name")]
        public string Name { get; set; }
        [XmlElement("Kcal")]
        public double Kcal{ get; set; }
        [XmlElement("Fat")]
        public double Fat{ get; set; }
        [XmlElement("Protein")]
        public double Protein{ get; set; }
        [XmlElement("Carb")]
        public double Carb{ get; set; }
        [XmlElement("CategoryId")]
        public int CategoryId { get; set; }

        public Item()
        {

        }
        public Item(int id)
            :base(id)
        {
            this.Name = "emptyName";
            this.Kcal = 0.0;
            this.Fat = 0.0;
            this.Protein = 0.0;
            this.Carb = 0.0;
        }

        
        public Item(int id, string name, double kcal, double fat, double protein, double carb, int categoryId)
            :base(id)
        {
            this.Name = name;
            this.Kcal = kcal;
            this.Fat = fat;
            this.Protein = protein;
            this.Carb = carb;
            this.CategoryId = categoryId;
        }
        public Item(Item item)
            :base(item.Id)
        {
            this.Name = item.Name;
            this.Kcal = item.Kcal;
            this.Fat = item.Fat;
            this.Protein = item.Protein;
            this.Carb = item.Carb;
        }
    }
}
