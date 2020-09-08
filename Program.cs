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
            actionService = Initialize(actionService);

            ItemService itemService = new ItemService();
            CategoryService categoryService = new CategoryService();
            MealService mealService = new MealService();
            DayService dayService = new DayService();

            Console.WriteLine("Welcome to CountItApp!");

            bool exitApp = false;
            bool isBackButtonPressed = false;
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
                switch (operation.KeyChar)
                {
                    case '1':
                        do
                        {
                            var keyInfo = categoryService.CategoryServiceView(actionService);
                            isBackButtonPressed = false;
                            switch (keyInfo.KeyChar)
                            {
                                case '1':
                                categoryService.AddNewCategory();
                                    break;
                                case '2':
                                itemService.AddNewItem(categoryService);
                                    break;
                                case '3':
                                itemService.SignProductToCategory(categoryService);
                                    break;
                                case '4':
                                categoryService.DeleteCategory(itemService);
                                    break;
                                case '5':
                                itemService.DeleteProduct(categoryService);
                                    break;
                                case '6':
                                categoryService.WievAllCategories();
                                break;
                                case '7':
                                itemService.ShowAllProducts();
                                    break;     
                                case '8':
                                Console.Clear();
                                itemService.ShowAllProductsFromChoosenCategory(categoryService);
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
                            var keyInfo = dayService.AddNewDayView(actionService);
                            isBackButtonPressed = false;
                            switch (keyInfo.KeyChar)
                            {
                                case '1':
                                    dayService.AddNewDay();
                                    break;
                                case '2':
                                    dayService.AddNewMeal(mealService);
                                    break;
                                case '3':
                                    dayService.AddProductToMeal(itemService);
                                    break;
                                case '4':
                                    dayService.RemoveProductFromMeal(mealService);
                                    break;
                                case '5':
                                    dayService.DeleteDay();
                                    break;
                                case '6':
                                    dayService.DeleteMeal();
                                    break;
                                case '7':
                                    dayService.ShowDayMacro();
                                    break;
                                case '8':
                                    dayService.ShowMealMacro();
                                    break;
                                case '9':
                                    dayService.ShowAllDays();
                                    //isBackButtonPressed = true;
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
                        categoryService.AddNewCategoryMixed();
                        itemService.AddNewItemMixed(categoryService);
                        itemService.SignProductToCategoryMixed(categoryService, itemService);
                        dayService.AddNewDayMixed();
                        dayService.AddNewMealMixed(mealService, itemService);
                        dayService.AddProductsToMealsMixed(mealService, itemService);
                        break;
                    default:
                        Console.WriteLine("Please choose right operation!");
                        break;
                }
            }
            while (!exitApp);
            

        }

        private static MenuActionService Initialize(MenuActionService actionService)
        {
            actionService.AddNewAction(1, "Management of products", "MainMenu");
            actionService.AddNewAction(2, "Your calculator", "MainMenu");
            actionService.AddNewAction(3, "Close app", "MainMenu");
            actionService.AddNewAction(4, "Set some data", "MainMenu");

            actionService.AddNewAction(1, "Add category", "ManagementApplication");
            actionService.AddNewAction(2, "Add product", "ManagementApplication");
            actionService.AddNewAction(3, "Sign product to category", "ManagementApplication");
            actionService.AddNewAction(4, "Delete category", "ManagementApplication");
            actionService.AddNewAction(5, "Delete product", "ManagementApplication");
            actionService.AddNewAction(6, "Show all categories", "ManagementApplication");
            actionService.AddNewAction(7, "Show all products from all categories", "ManagementApplication");
            actionService.AddNewAction(8, "Show all products from choosen category", "ManagementApplication");
            actionService.AddNewAction(9, "Back", "ManagementApplication");

            actionService.AddNewAction(1, "Add meal day", "UserCalculator");
            actionService.AddNewAction(2, "Add meal", "UserCalculator");
            actionService.AddNewAction(3, "Add product to meal", "UserCalculator");
            actionService.AddNewAction(4, "Delete product from meal", "UserCalculator");
            actionService.AddNewAction(5, "Delete day", "UserCalculator");
            actionService.AddNewAction(6, "Delete meal", "UserCalculator");
            actionService.AddNewAction(7, "Show day calories", "UserCalculator");
            actionService.AddNewAction(8, "Show meal calories", "UserCalculator");
            actionService.AddNewAction(9, "Show all day macros", "UserCalculator");
            actionService.AddNewAction(0, "Back", "UserCalculator");

            return actionService;
        }
    }
}
