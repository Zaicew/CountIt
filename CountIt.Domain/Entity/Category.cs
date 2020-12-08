using CountIt.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace CountIt.Domain.Entity
{
    public class Category : BaseEntity
    {
        [XmlElement("Name")]
        public string Name { get; set; }

        public Category()
        {
            //this.Id = 0;
            //this.Name = "unsignedCategory";
        }

        public Category(string name, int id)
            : base(id)
        {
            this.Name = name;
        }
    }
}
