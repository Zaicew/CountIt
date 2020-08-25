using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CountIt
{
    class CategoryService
    {
        public List<Category> Categories { get; set; }

        public CategoryService()
        {
            Categories = new List<Category>();
            Categories.Add(new Category("unsignedCategory"));
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
            if (isCategoryNameExist(nameOfCategory) == false)
            {
                Console.WriteLine($"Aplication actually contains that name of category! {nameOfCategory}");
                var existedCategory = Categories.FirstOrDefault(s => s.Name == nameOfCategory);
                return existedCategory.TypeId;
            }
            else
            {
                var addedCategory = new Category(nameOfCategory);
                Categories.Add(addedCategory);
                return addedCategory.TypeId;
            }
        }

        public void WievAllCategories()
        {
            Console.Clear();
            int i = 0;
            foreach(var item in Categories)
            {
                Console.WriteLine($"{i}. Nazwa: {item.Name}, ID: {item.TypeId}.");
                i++;
            }
        }

        public bool isCategoryNameExist(string input)
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

        public void DeleteCategory(ItemService itemService)
        {
            Console.Clear();
            int choosenIdOfCategory;
            do
            {
                Console.WriteLine($"Remmember, we cannot to delete {Categories[0].Name}");
                Console.WriteLine("Type ID of category, which You want to delete...");
                WievAllCategories();
            }
            while (!int.TryParse(Console.ReadLine(), out choosenIdOfCategory) || !ChceckCategoryListContainsCurrentCategoryFromId(choosenIdOfCategory) || choosenIdOfCategory == 0);

            string nameCategoryToDelete = Categories[choosenIdOfCategory].Name;
            Category categoryToDelete = Categories[choosenIdOfCategory];
            foreach (var element in itemService.Items)
            {
                if (element.Category.Name == nameCategoryToDelete)
                    element.Category = Categories[0];
            }
            itemService.ShowAllProducts();

            Categories.Remove(categoryToDelete);

        }

        private bool ChceckCategoryListContainsCurrentCategoryFromId(int id)
        {
            foreach (var item in Categories)
            {
                if (item.TypeId == id)
                    return true;
            }
            return false;
        }
    }
}
