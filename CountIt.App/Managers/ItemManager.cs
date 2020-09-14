using CountIt.App.Abstract;
using CountIt.App.Concrete;
using CountIt.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CountIt.App.Managers
{
    public class ItemManager
    {
        //private ItemService _itemService;
        //private CategoryService _categoryService; 
        private IService<Item> _itemService;
        private IService<Category> _categoryService;
        public ItemManager(CategoryService categoryService, ItemService itemService)
        {
            _categoryService = categoryService;
            _itemService = itemService;
        }

        public int AddNewItem(CategoryManager _categoryManager)
        {
            if (_categoryService.Items.Count <= 0)
            {
                Console.Clear();
                Console.WriteLine("Category list is empty. First please add at least one category.");
                return 0;
            }
            else
            {
                double kcal, fat, protein, carb;
                Console.WriteLine("Please type name of product...");
                string name = Console.ReadLine();
                do
                {
                    Console.WriteLine("Please type product kcal/100g...");
                }
                while (double.TryParse(Console.ReadLine(), out kcal) != true);
                do
                {
                    Console.WriteLine("Please type product fat/100g...");
                }
                while (double.TryParse(Console.ReadLine(), out fat) != true);
                do
                {
                    Console.WriteLine("Please type product protein/100g...");
                }
                while (double.TryParse(Console.ReadLine(), out protein) != true);
                do
                {
                    Console.WriteLine("Please type product carb/100g...");
                }
                while (double.TryParse(Console.ReadLine(), out carb) != true);

                var choosenCategory = _categoryManager.GetCategory();
                int id = _itemService.GetLastId();
                Item itemHolder = new Item(id + 1, name, kcal, fat, protein, carb, choosenCategory.Id);
                _itemService.AddItem(itemHolder);

                return itemHolder.Id;
            }
        }

        public int SignProductToCategory(CategoryManager _categoryManager)
        {
            var choosenItem = GetItem();
            var choosenCategory = _categoryManager.GetCategory();

            choosenItem.CategoryId = choosenCategory.Id;

            return choosenItem.Id;
        }

        public int DeleteProduct()
        {
            var choosenItem = GetItem();
            _itemService.Items.Remove(choosenItem);
            return choosenItem.Id;
        }

        public void ShowAllProducts()
        {
            foreach (var item in _itemService.Items)
            {
                Console.WriteLine($"{item.Id}. Name: {item.Name}, kcal: {item.Kcal}, fat: {item.Fat}, protein: {item.Protein}, carb: {item.Carb}, category ID: {item.CategoryId}.");
            }
        }
        public void ShowAllProductsFromChoosenCategory(CategoryManager _categoryManager)
        {
            var choosenCategory = _categoryManager.GetCategory();
            foreach (var item in _itemService.Items)
            {
                if (item.CategoryId == choosenCategory.Id)
                {
                    Console.WriteLine($"Name: {item.Name}, kcal: {item.Kcal}, fat: {item.Fat}, protein: {item.Protein}, carb: {item.Carb}, category ID: {item.CategoryId}.");
                }
            }
        }

        public void SignDefaultCategoryForAllProductsFromDeletingOne(Category categoryToDelete)
        {
            foreach (var item in _itemService.Items)
            {
                if (item.CategoryId == categoryToDelete.Id)
                    item.CategoryId = 0;
            }
        }

        public void SignProductToCategoryMixed(CategoryService categoryService, ItemService itemService)
        {
            Random rnd = new Random();
            foreach (var item in itemService.Items)
            {
                item.CategoryId = categoryService.Items[rnd.Next(1, 3)].Id;
            }
        }

        public void AddNewItemMixed(CategoryService categoryService, ItemService itemService)
        {
            string[] names = { "Milk", "Sausage", "Nutella", "Chips", "Orange Juice", "Orange", "Apple", "Strawberry", "Pumpkin", "Chicken Breast", "White bread", "Cookies", "Fish", "Garlic dip", "Yoghurt" };
            double[] kcals = { 10.1, 100, 190, 154.5, 390.4, 38.8, 4.9, 45, 10, 19 };

            Random rnd = new Random();
            for (int i = 0; i < 50; i++)
            {
                int itemId = _itemService.GetLastId();
                _itemService.Items.Add(new Item(itemId + 1, names[rnd.Next(1, 15)], kcals[rnd.Next(1, 10)], kcals[rnd.Next(1, 10)], kcals[rnd.Next(1, 10)], kcals[rnd.Next(1, 10)],
                    categoryService.Items[rnd.Next(1, categoryService.Items.Count)].Id));
            }
        }

        private bool ChceckItemListContainsCurrentItemFromId(int id)
        {
            foreach (var item in _itemService.Items)
            {
                if (item.Id == id)
                    return true;
            }
            return false;
        }
        private bool ChceckCategoryListContainsCurrentCategoryFromId(int id, CategoryService categories)
        {
            foreach (var item in categories.Items)
            {
                if (item.Id == id)
                    return true;
            }
            return false;
        }

        public Item GetItem()
        {
            int choosenIdOfItem;
            do
            {
                Console.Clear();
                Console.WriteLine("Type ID of product which You want to choose");
                ShowAllProducts();
            }
            while (!int.TryParse(Console.ReadLine(), out choosenIdOfItem) || !_itemService.Items.Contains(_itemService.Items.FirstOrDefault(s => s.Id == choosenIdOfItem)));

            return _itemService.Items.FirstOrDefault(s => s.Id == choosenIdOfItem);
        }
    }
}