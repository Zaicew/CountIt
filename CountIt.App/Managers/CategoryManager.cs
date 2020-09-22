using CountIt.App.Abstract;
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
        //private ItemService _itemService;
        private CategoryService _categoryService;
        private IService<Item> _itemService;
        //private IService<Category> _categoryService; >> zapytac o to kajetana!
        //private CategoryService categoryService;

        public CategoryManager(CategoryService categoryService, IService<Item> itemService)
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
            if (_categoryService.IsCategoryNameExist(nameOfCategory) == true)
            {
                Console.WriteLine($"Aplication actually contains that name of category! {nameOfCategory}");
                return _categoryService.GetCategoryByName(nameOfCategory).Id;
                //var existedCategory = _categoryService.Items.FirstOrDefault(s => s.Name == nameOfCategory);
                //return existedCategory.Id;
            }
            else
            {
                var categoryHolder = _categoryService.CreateCategory(nameOfCategory);
                //var id = _categoryService.GetLastId();
                //var addedCategory = new Category(nameOfCategory, id + 1);
                _categoryService.Items.Add(categoryHolder);
                return categoryHolder.Id;
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

        public void AddNewCategoryMixed()
        {
            _categoryService.Items.Add(new Category("Milky",1));
            _categoryService.Items.Add(new Category("Bread and buns",2));
            _categoryService.Items.Add(new Category("Meat",3));
        }

        public int DeleteCategory(ItemService _itemService)
        {
            Console.Clear();
            Category categoryToDelete = GetCategory();
            if (categoryToDelete.Id == _categoryService.Items.FirstOrDefault(s => s.Name == "unsignedCategory").Id)
            {
                Console.WriteLine("You cannot to delete default category!");
                return -1;
            }
            else
            {
                _itemService.SignDefaultCategoryForAllProductsFromDeletingOne(categoryToDelete);
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


        public bool TestingMethod()
        {
            var output = _categoryService.IsCategoryNameExist("as");

            if (output == false)
                return true;
            else             
            return false;

        }
    }
}
