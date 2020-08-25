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


        //public ConsoleKeyInfo AddNewItemView(MenuActionService actionService)
        //{
        //    var ManagementApplicationMenuView = actionService.GetMenuActrionsByMenuName("ManagementApplication");
        //    Console.WriteLine("Type Your product...");
        //    foreach (var menuAction in ManagementApplicationMenuView)
        //    {
        //        Console.WriteLine($"{menuAction.Id}. {menuAction.Name}");
        //    }
        //    var operation = Console.ReadKey();
        //    return operation;
        //}

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
                //int operation;
                //do
                //{
                //    Console.Clear();
                //    Console.WriteLine("Choose category (type corrct) (press number on your keyboard)...");
                //    categories.WievAllCategories();
                //}
                //while (!int.TryParse(Console.ReadLine(), out operation) || operation > categories.Categories.Count);
                Items.Add(new Item(name, kcal, fat, protein, carb, categories.Categories[0]));
                //Items.Add(new Item(name, kcal, fat, protein, carb, categories.Categories[operation]));
                ShowAllProducts();
            }
        }

        public int SignProductToCategory(CategoryService categories)
        {
            int choosenIdOfItem;
            do
            {
                Console.Clear();
                Console.WriteLine("Type ID of product which You want to sign");
                ShowAllProducts();
            }
            while(!int.TryParse(Console.ReadLine(), out choosenIdOfItem) || !chceckItemListContainsCurrentItemFromId(choosenIdOfItem));
            var choosenItem = Items[choosenIdOfItem];            
            int choosenIdOfCategory;
            do
            {
                Console.Clear();
                Console.WriteLine($"Choosen product: {choosenItem.Name}, {choosenItem.IdType}");
                Console.WriteLine("Type category ID where you want to place choosen product...");
                categories.WievAllCategories();                
            }
            while(!int.TryParse(Console.ReadLine(), out choosenIdOfCategory) || !chceckCategoryListContainsCurrentCategoryFromId(choosenIdOfCategory, categories));
            //var choosenCategory = categories.Categories[choosenIdOfItem];
            Category choosenCategory = categories.Categories.FirstOrDefault(p => p.TypeId == choosenIdOfCategory);

            choosenItem.Category = choosenCategory;
            return choosenItem.IdType;

        }

        public int DeleteProduct(CategoryService categoryService)
        {
            int choosenProductIdToDelete;
            do
            {
                Console.Clear();
                Console.WriteLine("Please type ID of product You want to delete...");
                ShowAllProducts();
            }
            while (!int.TryParse(Console.ReadLine(), out choosenProductIdToDelete) || !chceckItemListContainsCurrentItemFromId(choosenProductIdToDelete));
            Item itemToDelete = Items[choosenProductIdToDelete];
            Items.Remove(itemToDelete);
            return itemToDelete.IdType;
        }

        public void ShowAllProducts()
        {
            int i = 0;
            foreach (var item in Items)
            {
                Console.WriteLine($"{i}. Name: {item.Name}, kcal: {item.Kcal}, fat: {item.Fat}, protein: {item.Protein}, carb: {item.Carb}, category: {item.Category.Name}.");
                i++; 
            }
        }
        public void ShowAllProductsFromChoosenCategory(CategoryService categories)
        {
            int choosenIdOfCategory;
            do
            {
                Console.WriteLine("Type category ID...");
                categories.WievAllCategories();
            }
            while (!int.TryParse(Console.ReadLine(), out choosenIdOfCategory) || !chceckCategoryListContainsCurrentCategoryFromId(choosenIdOfCategory, categories));
            int i = 0;
            string choosenCategoryName = categories.Categories[choosenIdOfCategory].Name;
            foreach (var item in Items)
            {
                if (item.Category.Name == choosenCategoryName)
                {
                    Console.WriteLine($"{i}. Name: {item.Name}, kcal: {item.Kcal}, fat: {item.Fat}, protein: {item.Protein}, carb: {item.Carb}, category: {item.Category.Name}.");
                    i++;
                }
            }
        }

        internal void SignProductToCategoryMixed(CategoryService categoryService, ItemService itemService)
        {
            Random rnd = new Random();
            foreach (var item in itemService.Items)
            {
                item.Category = categoryService.Categories[rnd.Next(1, 3)];
            }
        }

        internal void AddNewItemMixed(CategoryService categoryService)
        {
            string[] names = { "Milk", "Sausage", "Nutella", "Chips", "Orange Juice", "Orange", "Apple", "Strawberry", "Pumpkin", "Chicken Breast", "White bread", "Cookies", "Fish", "Garlic dip", "Yoghurt" };
            double[] kcals = { 10.1, 100, 190, 154.5, 390.4, 38.8, 4.9, 45, 10, 19 };

            Random rnd = new Random();
            for (int i = 0; i < 50; i++)
            {
                Items.Add(new Item(names[rnd.Next(1, 15)], kcals[rnd.Next(1, 10)], kcals[rnd.Next(1, 10)], kcals[rnd.Next(1, 10)], kcals[rnd.Next(1, 10)]));
            }
        }

        private bool chceckItemListContainsCurrentItemFromId(int id)
        {
            foreach (var item in Items)
            {
                if (item.IdType == id)
                    return true;
            }
            return false;
        }
        private bool chceckCategoryListContainsCurrentCategoryFromId(int id, CategoryService categories)
        {
            foreach (var item in categories.Categories)
            {
                if (item.TypeId == id)
                    return true;
            }
            return false;
        }
    }
}
