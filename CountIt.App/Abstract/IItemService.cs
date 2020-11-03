using CountIt.Domain.Common;
using CountIt.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace CountIt.App.Abstract
{
    public interface IItemService<T> : IService<T> where T : BaseEntity
    {
        void SignDefaultCategoryForAllProductsFromDeletingOne(Category categoryToDelete);
        int AddItemByNesseseryData(string name, double kcal, double fat, double protein, double carb, Category category);
    }
}
