using CountIt.App.Common;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using CountIt.Domain.Entity;
using CountIt.App.Abstract;

namespace CountIt.App.Concrete
{
    public class ItemService : BaseService<Item>, IItemService<Item>
    {
        public ItemService()
        {

        }
        public ItemService(string path)
        {
            Items = ReadFromXml("items", path).ToList();
        }
        public void SignDefaultCategoryForAllProductsFromDeletingOne(Category categoryToDelete)
        {
            foreach (var item in Items)
            {
                if (item.CategoryId == categoryToDelete.Id)
                    item.CategoryId = 0;
            }
        }
        public int AddItemByNesseseryData(string name, double kcal, double fat, double protein, double carb, Category category)
        {
            var id = GetLastId() + 1;
            AddItem(new Item(id, name, kcal, fat, protein, carb, category.Id));
            return id;
        }
    }
}
