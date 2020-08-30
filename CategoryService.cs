using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CountIt
{
    class CategoryService
    {
        internal List<Category> Categories { get; set; }

        public CategoryService()
        {
            Categories = new List<Category>
            {
                new Category("unsignedCategory")
            };
        }


        public ConsoleKeyInfo CategoryServiceView(MenuActionService actionService)
        {
            var ManagementApplicationMenuView = actionService.GetMenuActrionsByMenuName("ManagementApplication");
            Console.WriteLine("What You want to do?");
            foreach (var menuAction in ManagementApplicationMenuView)
            {
                Console.WriteLine($"{menuAction.Id}. {menuAction.Name}");
            }
            var operation = Console.ReadKey();
            return operation;
        }

        internal ConsoleKeyInfo AddNewCategoryView(MenuActionService actionService)
        {
            throw new NotImplementedException();
        }

        public int AddNewCategory()
        {
            Console.Clear();
            Console.WriteLine("Please type name of category...");
            string nameOfCategory;
            nameOfCategory = Console.ReadLine();
            if (IsCategoryNameExist(nameOfCategory) == false)
            {
                Console.WriteLine($"Aplication actually contains that name of category! {nameOfCategory}");
                var existedCategory = Categories.FirstOrDefault(s => s.Name == nameOfCategory);
                return existedCategory.IdOfCategory;
            }
            else
            {
                var addedCategory = new Category(nameOfCategory);
                Categories.Add(addedCategory);
                return addedCategory.IdOfCategory;
            }
        }

        public void WievAllCategories()
        {
            Console.Clear();
            foreach(var item in Categories)
            {
                Console.WriteLine($"ID: {item.IdOfCategory}, Nazwa: {item.Name}, ID: {item.IdOfCategory}.");
            }
        }

        public bool IsCategoryNameExist(string input)
        {
            foreach(var item in Categories)
            {
                if (item.Name == input)
                    return false;
            }
            return true;
        }

        internal void AddNewCategoryMixed()
        {
            Categories.Add(new Category("Milky"));
            Categories.Add(new Category("Bread and buns"));
            Categories.Add(new Category("Meat"));
        }

        public int DeleteCategory(ItemService itemService)
        {
            Console.Clear();
            Category categoryToDelete = GetCategory();
            Categories.Remove(categoryToDelete);

            return categoryToDelete.IdOfCategory;

        }

        private bool ChceckCategoryListContainsCurrentCategoryFromId(int id)
        {
            foreach (var item in Categories)
            {
                if (item.IdOfCategory == id)
                    return true;
            }
            return false;
        }

        public Category GetCategory()
        {
            int choosenIdOfCategory;
            do
            {
                Console.WriteLine("Choose category from list...");
                WievAllCategories();
            }
            while (!int.TryParse(Console.ReadLine(), out choosenIdOfCategory) || !ChceckCategoryListContainsCurrentCategoryFromId(choosenIdOfCategory));

            return Categories.FirstOrDefault(p => p.IdOfCategory == choosenIdOfCategory);
        }
    }
}
