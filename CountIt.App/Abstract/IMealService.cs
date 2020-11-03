using CountIt.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace CountIt.App.Abstract
{
    public interface IMealService<T> : IService<T> where T : BaseEntity
    {
    }
}
