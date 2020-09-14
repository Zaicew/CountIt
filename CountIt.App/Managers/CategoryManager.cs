using CountIt.App.Concrete;
using CountIt.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CountIt.App.Managers
{
    public class CategoryManager
    {
        private ItemService _itemService;
        private CategoryService _categoryService;

        public CategoryManager(CategoryService categoryService, ItemService itemService)
        {
            _categoryService = categoryService;
            _categoryService.Items.Add(new Category("unsignedCategory", 0));
            _itemService = itemService;
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
        public int AddNewCategory()
        {
            Console.Clear();
            Console.WriteLine("Please type name of category...");
            string nameOfCategory;
            nameOfCategory = Console.ReadLine();
            if (IsCategoryNameExist(nameOfCategory) == false)
            {
                Console.WriteLine($"Aplication actually contains that name of category! {nameOfCategory}");
                var existedCategory = _categoryService.Items.FirstOrDefault(s => s.Name == nameOfCategory);
                return existedCategory.Id;
            }
            else
            {
                var id = _categoryService.GetLastId();
                var addedCategory = new Category(nameOfCategory, id + 1);
                _categoryService.Items.Add(addedCategory);
                return addedCategory.Id;
            }
        }

        public void WievAllCategories()
        {
            Console.Clear();
            foreach (var item in _categoryService.Items)
            {
                Console.WriteLine($"ID: {item.Id}, Nazwa: {item.Name}, ID: {item.Id}.");
            }
        }

        public bool IsCategoryNameExist(string input)
        {
            foreach (var item in _categoryService.Items)
            {
                if (item.Name == input)
                    return false;
            }
            return true;
        }

        public void AddNewCategoryMixed()
        {
            _categoryService.Items.Add(new Category("Milky",1));
            _categoryService.Items.Add(new Category("Bread and buns",2));
            _categoryService.Items.Add(new Category("Meat",3));
        }

        public int DeleteCategory(ItemManager _itemManager)
        {
            Console.Clear();
            Category categoryToDelete = GetCategory();
            if (categoryToDelete.Id == _categoryService.Items.FirstOrDefault(s => s.Name == "unsignedCategory").Id)
            {
                Console.WriteLine("You cannot to delete default category!");
                return 0;
            }
            else
            {
                _itemManager.SignDefaultCategoryForAllProductsFromDeletingOne(categoryToDelete);
                _categoryService.Items.Remove(categoryToDelete);
                return categoryToDelete.Id;
            }
        }
        public Category GetCategory()
        {
            int choosenIdOfCategory;
            do
            {
                Console.WriteLine("Choose category from list...");
                WievAllCategories();
            }
            while (!int.TryParse(Console.ReadLine(), out choosenIdOfCategory) || !_categoryService.Items.Contains(_categoryService.Items.FirstOrDefault(s => s.Id == choosenIdOfCategory)));

            return _categoryService.Items.FirstOrDefault(p => p.Id == choosenIdOfCategory);
        }
    }
}
