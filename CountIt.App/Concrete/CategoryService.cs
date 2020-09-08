using CountIt.App.Common;
using CountIt.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CountIt.App.Concrete
{
    public class CategoryService : BaseService<Day>
    {
        public List<Category> Categories { get; set; }

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

        public void AddNewCategoryMixed()
        {
            Categories.Add(new Category("Milky"));
            Categories.Add(new Category("Bread and buns"));
            Categories.Add(new Category("Meat"));
        }

        public int DeleteCategory(ItemService itemService)
        {
            Console.Clear();
            Category categoryToDelete = GetCategory();
            if (categoryToDelete.IdOfCategory == Categories.FirstOrDefault(s => s.Name == "unsignedCategory").IdOfCategory)
            {
                Console.WriteLine("You cannot to delete default category!");
                return 0;
            }
            else
            {
                itemService.SignDefaultCategoryForAllProductsFromDeletingOne(categoryToDelete);
                Categories.Remove(categoryToDelete);
                return categoryToDelete.IdOfCategory;
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
            while (!int.TryParse(Console.ReadLine(), out choosenIdOfCategory) || !Categories.Contains(Categories.FirstOrDefault(s => s.IdOfCategory == choosenIdOfCategory)));

            return Categories.FirstOrDefault(p => p.IdOfCategory == choosenIdOfCategory);
        }
    }
}
