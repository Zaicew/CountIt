using System;
using System.Collections.Generic;
using System.Text;

namespace CountIt
{
    class Item
    {
        private static int id;
        public int Id { get; set; }
        public string Name { get; set; }
        public double Kcal{ get; set; }
        public double Fat{ get; set; }
        public double Protein{ get; set; }
        public double Carb{ get; set; }
        public int CategoryId { get; set; }

        public Item()
        {
            this.Id = id++;
            this.Name = "emptyName";
            this.Kcal = 0.0;
            this.Fat = 0.0;
            this.Protein = 0.0;
            this.Carb = 0.0;
        }

        
        public Item(string name, double kcal, double fat, double protein, double carb, int categoryId = null)
        {
            this.Id = id++;
            this.Name = name;
            this.Kcal = kcal;
            this.Fat = fat;
            this.Protein = protein;
            this.Carb = carb;
            this.CategoryId = categoryId;
        }

    }
}
