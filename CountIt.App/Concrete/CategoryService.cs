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
                    return true;
            }
            return false;
        }

        public Category GetCategoryByName(string name)
        {
            return Items.FirstOrDefault(s => s.Name == name);
        }

        public Category CreateCategory(string name)
        {
            int lastId = GetLastId();
            var categoryHolder = new Category(name, lastId + 1);
            return categoryHolder;
        }
    }
}
