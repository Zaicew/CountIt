using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;

namespace CountIt
{
    class ItemService
    {
        public List<Item> Items { get; set; }

        public ItemService()
        {
            Items = new List<Item>();
        }

        public void AddNewItem(CategoryService categories)
        {
            if (categories.Categories.Count <= 0)
            {
                Console.Clear();
                Console.WriteLine("Category list is empty. First please add at least one category.");
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

                var choosenCategory = categories.GetCategory();
                Items.Add(new Item(name, kcal, fat, protein, carb, choosenCategory.IdOfCategory));
            }
        }

        public int SignProductToCategory(CategoryService categories)
        {
            var choosenItem = GetItem();
            var choosenCategory = categories.GetCategory();

            choosenItem.CategoryId = choosenCategory.IdOfCategory;

            return choosenItem.Id;
        }

        public int DeleteProduct(CategoryService categoryService)
        {
            var choosenItem = GetItem();
            Items.Remove(choosenItem);
            return choosenItem.Id;
        }

        public void ShowAllProducts()
        { 
            foreach (var item in Items)
            {
                Console.WriteLine($"{item.Id}. Name: {item.Name}, kcal: {item.Kcal}, fat: {item.Fat}, protein: {item.Protein}, carb: {item.Carb}, category ID: {item.CategoryId}.");
            }
        }
        public void ShowAllProductsFromChoosenCategory(CategoryService categories)
        {
            var choosenCategory = categories.GetCategory();
            foreach (var item in Items)
            {
                if (item.CategoryId == choosenCategory.IdOfCategory)
                {
                    Console.WriteLine($"Name: {item.Name}, kcal: {item.Kcal}, fat: {item.Fat}, protein: {item.Protein}, carb: {item.Carb}, category ID: {item.CategoryId}.");
                }
            }
        }

        internal void SignProductToCategoryMixed(CategoryService categoryService, ItemService itemService)
        {
            Random rnd = new Random();
            foreach (var item in itemService.Items)
            {
                item.CategoryId = categoryService.Categories[rnd.Next(1, 3)].IdOfCategory;
            }
        }

        internal void AddNewItemMixed(CategoryService categoryService)
        {
            string[] names = { "Milk", "Sausage", "Nutella", "Chips", "Orange Juice", "Orange", "Apple", "Strawberry", "Pumpkin", "Chicken Breast", "White bread", "Cookies", "Fish", "Garlic dip", "Yoghurt" };
            double[] kcals = { 10.1, 100, 190, 154.5, 390.4, 38.8, 4.9, 45, 10, 19 };

            Random rnd = new Random();
            for (int i = 0; i < 50; i++)
            {
                Items.Add(new Item(names[rnd.Next(1, 15)], kcals[rnd.Next(1, 10)], kcals[rnd.Next(1, 10)], kcals[rnd.Next(1, 10)], kcals[rnd.Next(1, 10)], 
                    categoryService.Categories[rnd.Next(1,categoryService.Categories.Count)].IdOfCategory));
            }
        }

        private bool ChceckItemListContainsCurrentItemFromId(int id)
        {
            foreach (var item in Items)
            {
                if (item.Id == id)
                    return true;
            }
            return false;
        }
        private bool ChceckCategoryListContainsCurrentCategoryFromId(int id, CategoryService categories)
        {
            foreach (var item in categories.Categories)
            {
                if (item.IdOfCategory == id)
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
            while (!int.TryParse(Console.ReadLine(), out choosenIdOfItem) || !Items.Contains(Items.FirstOrDefault(s => s.Id == choosenIdOfItem)));

            return Items.FirstOrDefault(s => s.Id == choosenIdOfItem);
        }
    }
}
