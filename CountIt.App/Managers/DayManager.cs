using CountIt.App.Abstract;
using CountIt.App.Concrete;
using CountIt.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CountIt.App.Managers
{
    public class DayManager
    {
        //private MenuActionService _menuActionService;
        //private DayService _dayService;
        //private MealService _mealService;
        private readonly MenuActionService _menuActionService;
        private readonly IDayService<Day> _dayService;
        private readonly IMealService<Meal> _mealService;


        //public DayManager(MenuActionService menuActionService, IDayService<Day> dayService, IMealService<Meal> mealService)
        //{
        //    _menuActionService = menuActionService;
        //    _dayService = dayService;
        //    //_mealService = mealService;
        //}        stary konstruktor z mealservice - nie potrzebny jest?
        public DayManager(MenuActionService menuActionService, IDayService<Day> dayService, IMealService<Meal> mealService)
        {
            _menuActionService = menuActionService;
            _dayService = dayService;
            _mealService = mealService;
        }
        public DayManager(IDayService<Day> dayService)
        {
            _dayService = dayService;
        }
        public ConsoleKeyInfo AddNewDayView()
        {
            var ManagementApplicationMenuView = _menuActionService.GetMenuActrionsByMenuName("UserCalculator");
            Console.WriteLine("Type Your product...");
            foreach (var menuAction in ManagementApplicationMenuView)
            {
                Console.WriteLine($"{menuAction.Id}. {menuAction.Name}");
            }
            var operation = Console.ReadKey();
            return operation;
        }

        //public int AddNewMeal()
        //{
        //    string nameOfMeal;
        //    var choosenDay = GetDay();
        //    do
        //    {
        //        Console.WriteLine("Please type name of meal...");
        //        nameOfMeal = Console.ReadLine();
        //    }
        //    while (choosenDay.mealList.Contains(choosenDay.mealList.FirstOrDefault(s => s.NameOfMeal == nameOfMeal)));
        //    var addedMealId = _dayService.AddNewMealinDay(nameOfMeal, choosenDay);
        //    return addedMealId;
        //}
        public int AddNewDay()
        {

            int day, month, year;
            DateTime dateTime;
            Console.Clear();
            do
            {
                Console.WriteLine("Type day of month... (example: '18')");
                int.TryParse(Console.ReadLine(), out day);
                Console.WriteLine("Type number of month... (example: '3')");
                int.TryParse(Console.ReadLine(), out month);
                Console.WriteLine("Type year... (example: '1998')");
                int.TryParse(Console.ReadLine(), out year);
            }
            while (!DateTime.TryParse($"{day}/{month}/{year}", out dateTime) || IsDayExistinginDatabase(dateTime));
            var holder = _dayService.CreateNewDayByDateTime(dateTime, _mealService);
            return holder.Id;
        }

        public int AddProductToMeal(ItemManager _itemManager)
        {
            double howManyGrmasOfProduct;
            var dayHolder = GetDay();
            var mealHolder = GetMealFromDay(dayHolder);
            //var itemHolder = _itemManager.GetItem();
            do
            {
                Console.Clear();
                Console.WriteLine("Type how many grams of choosen product You want to add...");
            }
            while (!double.TryParse(Console.ReadLine(), out howManyGrmasOfProduct));

            var itemInMealHolder = new ItemInMeal(_itemManager.GetItem(), howManyGrmasOfProduct);
            _dayService.AddProductToMeal(itemInMealHolder, mealHolder, dayHolder);

            Console.WriteLine("Added grams of product: " + itemInMealHolder.Weight);
            Console.WriteLine("Total Carbs: " + mealHolder.TotalCarbs);
            Console.WriteLine("Total fat: " + mealHolder.TotalFat);
            Console.WriteLine("Total kcal: " + mealHolder.TotalKcal);
            Console.WriteLine("Total protein: " + mealHolder.TotalProtein);

            return itemInMealHolder.Id;
        }

        public int ChangeMealName()
        {
            var dayHolder = GetDay();
            var mealHolder = GetMealFromDay(dayHolder);
            Console.WriteLine("Type name of meal...");
            var name = Console.ReadLine();
            mealHolder.NameOfMeal = name;
            return mealHolder.Id;
        }

        public void ShowMealMacro()
        {
            var dayHolder = GetDay();
            var mealHolder = GetMealFromDay(dayHolder);

            Console.WriteLine($"(Total's) Carb: {mealHolder.TotalCarbs}, fat: {mealHolder.TotalFat}, protein: {mealHolder.TotalProtein}, kcal: {mealHolder.TotalKcal}. ");
        }

        public int DeleteMeal()
        {
            var dayHolder = GetDay();
            var mealHodler = GetMealFromDay(dayHolder);
            var id = _dayService.HideMeal(mealHodler, dayHolder);
            //dayHolder.MealList.Remove(mealToDelete);
            return id;
        }

        public void ShowDayMacro()
        {
            var dayHolder = GetDay();

            Console.WriteLine($"Id: {dayHolder.Id}, date: {dayHolder.DateTime}, total carb: {dayHolder.TotalCarbs}" +
                $", total fat: {dayHolder.TotalFat}, total protein: {dayHolder.TotalProtein}, total kcal: {dayHolder.TotalKcal} ");
        }

        public int DeleteDay()
        {
            var dayHolder = GetDay();
            _dayService.Items.Remove(dayHolder);
            return dayHolder.Id;
        }

        public int RemoveProductFromMeal(MealManager _mealManager)
        {
            var dayHolder = GetDay();
            var mealHolder = GetMealFromDay(dayHolder);
            var itemInMealHolder = GetItemFromMeal(mealHolder, _mealManager);
            return _dayService.RemoveProductFromMeal(itemInMealHolder, mealHolder, dayHolder);

            //mealHolder.ProductsInMeal.Remove(itemInMealHolder);
            //_dayService.RecalculateMacrosInMeal(mealHolder);
            //_dayService.RecalculateMacrosInDay(dayHolder);

            //Console.WriteLine("Total Carbs: " + mealHolder.TotalCarbs);
            //Console.WriteLine("Total fat: " + mealHolder.TotalFat);
            //Console.WriteLine("Total kcal: " + mealHolder.TotalKcal);
            //Console.WriteLine("Total protein: " + mealHolder.TotalProtein);
        }

        public void ShowAllDays()
        {
            foreach (var item in _dayService.Items)
            {
                Console.WriteLine($"Id: {item.Id}," +
                    $" date: {item.DateTime}," +
                    $" total carb: {Math.Round(item.TotalCarbs, 2)}," +
                    $" total fat: {Math.Round(item.TotalFat, 2)}," +
                    $" total protein: {Math.Round(item.TotalProtein, 2)}," +
                    $" total kcal: {Math.Round(item.TotalKcal, 2)} ");
            }
        }

        public bool IsDayExistinginDatabase(DateTime dateTime)
        {
            if (_dayService.Items.Count < 1)
                return false;
            else
            {
                foreach (var item in _dayService.Items)
                {
                    if (item.DateTime == dateTime)
                        Console.WriteLine($"This day existing in Your app: {dateTime.Date.ToShortDateString()}");
                    return false;
                }
            }
            return true;
        }

        public void ShowAllMealsFromDay(int choosenIdOfDay)
        {
            var currentDay = _dayService.Items.FirstOrDefault(s => s.Id == choosenIdOfDay);
            foreach (var item in currentDay.mealList)
            {
                Console.WriteLine($"Id: {item.Id}, name of meal: {item.NameOfMeal}");
            }
        }
        //public void AddNewDayMixed()
        //{
        //    int[] days = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30 };
        //    int[] months = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
        //    int[] years = { 2018, 2019, 2020, 2021, 2022, 2023 };

        //    Random rnd = new Random();

        //    for (int i = 0; i < 10; i++)
        //    {
        //        DateTime dateTime = new DateTime(days[rnd.Next(1, 30)], months[rnd.Next(1, 12)], days[rnd.Next(1, 6)]);
        //        _dayService.Items.Add(new Day(dateTime, rnd.Next(1,10)));
        //    }
        //}

        //public void AddNewMealMixed(MealManager _mealManager, ItemManager _itemManager, DayService _dayService)
        //{
        //    Random rnd = new Random();
        //    string[] names = { "Breakfast", "Lunch", "Dinner", "Evening food :)" };
        //    foreach (var item in _dayService.Items)
        //    {
        //        for (int i = 0; i < names.Length; i++)
        //        {
        //            item.MealList.Add(new Meal(names[i], rnd.Next(1,10)));
        //        }
        //    }
        //}

        //public void AddProductsToMealsMixed(MealManager mealManager, ItemManager itemManager, ItemService itemService)
        //{
        //    Random rnd = new Random();
        //    double howManyGramsOfProduct = 0;
        //    double[] grams = { 20.3, 30.9, 40.3, 50.1, 60.8, 70.9, 80.4, 90.8 };
        //    foreach (var item in _dayService.Items)
        //    {
        //        for (int i = 0; i < item.MealList.Count; i++)
        //        {
        //            var mealHolder = item.MealList[i];
        //            var itemHolder = new ItemInMeal(itemService.Items[rnd.Next(1, itemService.Items.Count)], grams[rnd.Next(1, 8)]);

        //            mealHolder.ProductsInMeal.Add(itemHolder);
        //            howManyGramsOfProduct = grams[rnd.Next(1, 7)];


        //            mealHolder.TotalCarbs += Math.Round(itemHolder.Carb * howManyGramsOfProduct / 100, 2);
        //            mealHolder.TotalFat += Math.Round(itemHolder.Fat * howManyGramsOfProduct / 100, 2);
        //            mealHolder.TotalKcal += Math.Round(itemHolder.Kcal * howManyGramsOfProduct / 100, 2);
        //            mealHolder.TotalProtein += Math.Round(itemHolder.Protein * howManyGramsOfProduct / 100, 2);

        //            item.TotalCarbs += Math.Round(mealHolder.TotalCarbs, 2);
        //            item.TotalFat += Math.Round(mealHolder.TotalFat, 2);
        //            item.TotalKcal += Math.Round(mealHolder.TotalKcal, 2);
        //            item.TotalProtein += Math.Round(mealHolder.TotalProtein, 2);

        //        }
        //    }
        //}

        public Day GetDay()
        {
            int choosenIdOfDay;
            do
            {
                Console.WriteLine("Type ID day You want to choose...");
                ShowAllDays();
            }
            while (!int.TryParse(Console.ReadLine(), out choosenIdOfDay) || !_dayService.Items.Contains(_dayService.Items.FirstOrDefault(s => s.Id == choosenIdOfDay)));

            return _dayService.Items.FirstOrDefault(s => s.Id == choosenIdOfDay);
        }
        public Meal GetMealFromDay(Day day)
        {
            int choosenIdOfMeal;
            do
            {
                Console.Clear();
                Console.WriteLine("Please type meal ID from day, where You want to add product...");
                foreach (var item in day.mealList)
                {
                    Console.WriteLine($"ID: {item.Id}, name: {item.NameOfMeal}.");
                }
            }
            while (!int.TryParse(Console.ReadLine(), out choosenIdOfMeal) || !day.mealList.Contains(day.mealList.FirstOrDefault(s => s.Id == choosenIdOfMeal)));
            return day.mealList.FirstOrDefault(s => s.Id == choosenIdOfMeal);
        }

        public ItemInMeal GetItemFromMeal(Meal meal, MealManager mealManager)
        {
            int choosenIdOfItem;
            do
            {
                Console.Clear();
                Console.WriteLine("Please type product ID, which you want to delete from meal...");
                mealManager.ShowAllProductsFromMeal(meal);
            }
            while (!int.TryParse(Console.ReadLine(), out choosenIdOfItem) || !meal.ProductsInMeal.Contains(meal.ProductsInMeal.FirstOrDefault(s => s.Id == choosenIdOfItem)));
            Console.WriteLine("item id to delete meal : " + choosenIdOfItem);
            return meal.ProductsInMeal.FirstOrDefault(s => s.Id == choosenIdOfItem);
        }
    }
}
