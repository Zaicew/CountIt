using CountIt.App.Abstract;
using CountIt.App.Concrete;
using CountIt.App.Managers;
using CountIt.Domain.Entity;
using System;
using System.ComponentModel.Design;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace CountIt
{
    class Program
    {
        static void Main(string[] args)
        {
            MenuActionService actionService = new MenuActionService();
            IItemService<Item> itemService = new ItemService();
            ICategoryService<Category> categoryService = new CategoryService();
            IDayService<Day> dayService = new DayService();
            IMealService<Meal> mealService = new MealService();
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
