using System;
using System.Collections.Generic;
using System.Text;

namespace CountIt
{
    class Category
    {
        private static int id;
        public string Name { get; set; }
        public int IdOfCategory { get; set; }

        //public List<Item> Items { get; set; }
        public Category()
        {
            this.IdOfCategory = 0;
            this.Name = "unsignedCategory";
            //this.Items = new List<Item>();
        }

        public Category(string name)
        {
            this.IdOfCategory = id++;
            this.Name = name;
            //this.Items = new List<Item>();
        }
    }
}
