using CountIt.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace CountIt.Domain.Entity
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public Category()
        {
            this.Id = 0;
            this.Name = "unsignedCategory";
        }

        public Category(string name, int id)
            : base(id)
        {
            this.Name = name;
        }
    }
}
