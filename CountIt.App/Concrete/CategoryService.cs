using CountIt.App.Common;
using CountIt.App.Managers;
using CountIt.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CountIt.App.Concrete
{
    public class CategoryService : BaseService<Category>
    {
        public bool IsCategoryNameExist(string input)
        {
            foreach (var item in Items)
            {
                if (item.Name == input)
                    return false;
            }
            return true;
        }
    }
}
