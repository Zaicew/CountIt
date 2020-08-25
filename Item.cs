using System;
using System.Collections.Generic;
using System.Text;

namespace CountIt
{
    class Item
    {
        public static int Id { get; set; }
        public int IdType { get; set; }
        public string Name { get; set; }
        public double Kcal{ get; set; }
        public double Fat{ get; set; }
        public double Protein{ get; set; }
        public double Carb{ get; set; }
        public Category Category { get; set; }

        public Item()
        {
            this.IdType = Id++;
            this.Name = "emptyName";
            this.Kcal = 0.0;
            this.Fat = 0.0;
            this.Protein = 0.0;
            this.Carb = 0.0;
        }

        
        public Item(string name, double kcal, double fat, double protein, double carb, Category category = null)
        {
            this.IdType = Id++;
            this.Name = name;
            this.Kcal = kcal;
            this.Fat = fat;
            this.Protein = protein;
            this.Carb = carb;
            this.Category = category;
        }

    }
}
