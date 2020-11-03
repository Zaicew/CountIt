using CountIt.App.Common;
using CountIt.Domain.Common;
using CountIt.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace CountIt.App.Abstract
{
    public interface ICategoryService<T> : IService<T> where T : BaseEntity
    {
        bool IsCategoryNameExist(string input);
        Category GetCategoryByName(string name);
        Category CreateCategory(string name);
        Category GetCategoryById(int id);
    }
}
