using CountIt.App.Abstract;
using CountIt.App.Common;
using CountIt.App.Concrete;
using CountIt.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;

namespace CountIt.App.Managers
{
    public class CategoryManager
    {
        //private ItemService _itemService;
        //private CategoryService _categoryService;
        //private IService<Item> _itemService; // >> zapytac o to kajetana!
        //private BaseService<Category> _categoryService;
        private readonly ICategoryService<Category> _categoryService;
        private readonly IItemService<Item> _itemService;
        //private CategoryService _categoryService;
        //private ItemService _itemService;

        public CategoryManager(ICategoryService<Category> categoryService, IItemService<Item> itemService)
        {
            _categoryService = categoryService;
            _itemService = itemService;

            if(!_categoryService.Items.Contains(_categoryService.Items.FirstOrDefault(s => s.Name == "unsignedCategory")))
            _categoryService.Items.Add(new Category("unsignedCategory", 0));
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
            }
            else
            {
                var categoryHolder = _categoryService.CreateCategory(nameOfCategory);
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
        public int DeleteCategory()
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

            return _categoryService.GetCategoryById(choosenIdOfCategory);
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
