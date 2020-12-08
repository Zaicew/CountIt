using CountIt.App.Abstract;
using CountIt.App.Concrete;
using CountIt.App.Managers;
using CountIt.Domain.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Xml.Serialization;
using static System.Net.Mime.MediaTypeNames;

namespace CountIt
{
    class Program
    {
        static void Main(string[] args)
        {
            string filePath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            filePath += "\\xmlSettings";
            Directory.CreateDirectory(filePath);
            Console.WriteLine(filePath);

            //Assembly asm = Assembly.GetEntryAssembly();
            //string appDir = Path.GetDirectoryName(asm.Location);
            //string filePath = Path.Combine(appDir, "settings.xml");
            //Console.WriteLine("{0}", filePath + "jestem przed");
            //Console.WriteLine(appDir);
            //Console.ReadLine();
            //Directory.CreateDirectory(filePath + "xdddd");
            //Console.WriteLine("jestem po!");
            //Console.ReadLine();

            MenuActionService actionService = new MenuActionService();

            //IItemService<Item> itemService = new ItemService(Path.Combine(filePath, "items.xml"));
            //ICategoryService<Category> categoryService = new CategoryService(Path.Combine(filePath, "categories.xml"));
            //IDayService<Day> dayService = new DayService(Path.Combine(filePath, "days.xml"));
            //IMealService<Meal> mealService = new MealService(Path.Combine(filePath, "meals.xml"));
            //Console.WriteLine(Path.Combine(filePath, "items.xml"));
            ItemService itemService = new ItemService(Path.Combine(filePath, "Items.xml"));
            CategoryService categoryService = new CategoryService(Path.Combine(filePath, "Categories.xml"));
            DayService dayService = new DayService(Path.Combine(filePath, "Days.xml"));
            MealService mealService = new MealService(Path.Combine(filePath, "Meals.xml"));

            ItemManager itemManager = new ItemManager(categoryService, itemService);
            CategoryManager categoryManager = new CategoryManager(categoryService, itemService);
            MealManager mealManager = new MealManager();
            DayManager dayManager = new DayManager(actionService, dayService, mealService);

            Console.WriteLine("Welcome to CountItApp!");

            bool exitApp;
            do
            {
                Console.WriteLine("What You want to do:");
                var mainMenu = actionService.GetMenuActrionsByMenuName("MainMenu");
                foreach (var item in mainMenu)
                {
                    Console.WriteLine($"{item.Id}. {item.Name}");
                }
                exitApp = false;
                var operation = Console.ReadKey();
                bool isBackButtonPressed;
                switch (operation.KeyChar)
                {
                    case '1':
                        do
                        {                            
                            var keyInfo = categoryManager.CategoryServiceView(actionService);
                            isBackButtonPressed = false;
                            switch (keyInfo.KeyChar)
                            {
                                case '1':
                                    categoryManager.AddNewCategory();
                                    break;
                                case '2':
                                    itemManager.AddNewItem(categoryManager);
                                    break;
                                case '3':
                                    itemManager.SignProductToCategory(categoryManager);
                                    break;
                                case '4':
                                    categoryManager.DeleteCategory();
                                    break;
                                case '5':
                                    itemManager.DeleteProduct();
                                    break;
                                case '6':
                                    categoryManager.WievAllCategories();
                                    break;
                                case '7':
                                    itemManager.ShowAllProducts();
                                    break;
                                case '8':
                                    Console.Clear();
                                    itemManager.ShowAllProductsFromChoosenCategory(categoryManager);
                                    break;
                                case '9':
                                    isBackButtonPressed = true;
                                    break;
                                default:
                                    Console.WriteLine("Please choose right operation!");
                                    break;
                            }
                        }
                        while (!isBackButtonPressed);
                        break;

                    case '2':
                        do
                        {
                            var keyInfo = dayManager.AddNewDayView();
                            isBackButtonPressed = false;
                            switch (keyInfo.KeyChar)
                            {
                                case '1':
                                    dayManager.AddNewDay();
                                    break;
                                case '2':
                                    dayManager.ChangeMealName();
                                    break;
                                case '3':
                                    dayManager.AddProductToMeal(itemManager);
                                    break;
                                case '4':
                                    dayManager.RemoveProductFromMeal(mealManager);
                                    break;
                                case '5':
                                    dayManager.DeleteDay();
                                    break;
                                case '6':
                                    dayManager.DeleteMeal();
                                    break;
                                case '7':
                                    dayManager.ShowDayMacro();
                                    break;
                                case '8':
                                    dayManager.ShowMealMacro();
                                    break;
                                case '9':
                                    dayManager.ShowAllDays();
                                    break;
                                case '0':
                                    dayManager.ShowAllDays();
                                    isBackButtonPressed = true;
                                    break;
                                default:
                                    Console.WriteLine("Please choose right operation!");
                                    break;
                            }
                        }
                        while (!isBackButtonPressed);
                        break;
                    case '3':
                        //ItemService itemService = new ItemService(Path.Combine(filePath, "items.xml"));
                        //ICategoryService<Category> categoryService = new CategoryService(Path.Combine(filePath, "categories.xml"));
                        //IDayService<Day> dayService = new DayService(Path.Combine(filePath, "days.xml"));
                        //IMealService<Meal> mealService = new MealService(Path.Combine(filePath, "meals.xml"));
                        itemService.SaveListToXml("items", Path.Combine(filePath, "Items.xml"));
                        categoryService.SaveListToXml("categories", Path.Combine(filePath, "Categories.xml"));
                        dayService.SaveListToXml("days", Path.Combine(filePath, "Days.xml"));
                        mealService.SaveListToXml("meals", Path.Combine(filePath, "Meals.xml"));
                        exitApp = true;
                        break;
                    case '4':
                        //categoryManager.AddNewCategoryMixed();
                        //itemManager.AddNewItemMixed(categoryService, itemService);
                        //itemManager.SignProductToCategoryMixed(categoryService, itemService);
                        //dayManager.AddNewDayMixed();
                        ////dayManager.AddNewMealMixed(mealManager, itemManager, dayService);
                        //dayManager.AddProductsToMealsMixed(mealManager, itemManager, itemService);
                        break;
                    default:
                        Console.WriteLine("Please choose right operation!");
                        break;
                }
            }
            while (!exitApp);


        }
    }
}
