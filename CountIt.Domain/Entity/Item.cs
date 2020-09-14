using CountIt.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace CountIt.Domain.Entity
{
    public class Item : BaseEntity
    {
        public string Name { get; set; }
        public double Kcal{ get; set; }
        public double Fat{ get; set; }
        public double Protein{ get; set; }
        public double Carb{ get; set; }
        public int CategoryId { get; set; }

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
