using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace CountIt.Domain.Common
{
    public class BaseEntity : AuditableModel
    {
        [XmlElement("Id")]
        public int Id { get; set; }

        public BaseEntity(int id)
        {
            Id = id;
        }
        public BaseEntity()
        {
        }
    }
}
