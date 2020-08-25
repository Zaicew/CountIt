using System;
using System.Collections.Generic;
using System.Text;

namespace CountIt
{
    class Category
    {
        public static int Id { get; set; }
        public string Name { get; set; }
        public int TypeId { get; set; }

        public List<Item> Items { get; set; }
        public Category()
        {
            this.TypeId = 0;
            this.Name = "unsignedCategory";
            this.Items = new List<Item>();
        }

        public Category(string name)
        {
            this.TypeId = Id++;
            this.Name = name;
            this.Items = new List<Item>();
        }
    }
}
