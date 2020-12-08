using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace CountIt.Domain.Common
{
    public class AuditableModel
    {
        [XmlIgnore]
        public int CreateById { get; set; }
        [XmlIgnore]
        public DateTime CreatedDateTime { get; set; }
        [XmlIgnore]
        public int? ModifiedById { get; set; }
        [XmlIgnore]
        public DateTime? ModifiedDateeTime { get; set; }
    }
}
