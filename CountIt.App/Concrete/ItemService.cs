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

namespace CountIt.App.Concrete
{
    public class ItemService : BaseService<Item>
    {

        public void SignDefaultCategoryForAllProductsFromDeletingOne(Category categoryToDelete)
        {
            foreach (var item in Items)
            {
                if (item.CategoryId == categoryToDelete.Id)
                    item.CategoryId = 0;
            }
        }
    }
}
