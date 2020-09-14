using System;
using System.Collections.Generic;
using System.Text;

namespace CountIt.Domain.Common
{
    public class BaseEntity : AuditableModel
    {
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
