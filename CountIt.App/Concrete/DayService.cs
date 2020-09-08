using CountIt.App.Common;
using CountIt.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Xml.Schema;

namespace CountIt.App.Concrete
{
    public class DayService : BaseService<Day>
    {
        public List<MealInDay> MealsListInDay { get; set; }
        public List<Day> Days { get; set; }

        public DayService()
        {
            Days = new List<Day>();
        }
        public ConsoleKeyInfo AddNewDayView(MenuActionService actionService)
        {
            var ManagementApplicationMenuView = actionService.GetMenuActrionsByMenuName("UserCalculator");
            Console.WriteLine("Type Your product...");
            foreach (var menuAction in ManagementApplicationMenuView)
            {
                Console.WriteLine($"{menuAction.Id}. {menuAction.Name}");
            }
            var operation = Console.ReadKey();
            return operation;
        }

        public int AddNewMeal()
        {
            string nameOfMeal;
            var choosenDay = GetDay();
            do
            {
                Console.WriteLine("Please type name of meal...");
                nameOfMeal = Console.ReadLine();
            }
            while (!choosenDay.mealList.Contains(choosenDay.mealList.FirstOrDefault(s => s.NameOfMeal == nameOfMeal)));
            
            var addedMeal = new Meal(nameOfMeal);
            choosenDay.mealList.Add(addedMeal);
            return choosenDay.mealList.FirstOrDefault(s => s.NameOfMeal == nameOfMeal).Id;
        }
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
            while (!DateTime.TryParse($"{day}/{month}/{year}", out dateTime) || !IsDayExistinginDatabase(dateTime));

            var addedDay = new Day(dateTime);
            Days.Add(addedDay);
            Console.WriteLine("added day: " + addedDay.dateTime.ToShortDateString());
            return addedDay.Id;

            //DateTime.TryParse($"{day}/{month}/{year}", out dateTime);
            //if (!IsDayExistinginDatabase(dateTime))
            //{
            //    Console.WriteLine($"Your data actually cotains typed day! {dateTime}");
            //    var existedDay = Days.FirstOrDefault(s => s.dateTime == dateTime);
            //    Console.WriteLine("existed day: " + existedDay.dateTime);
            //    return existedDay.IdOfDay;
            //}
            //else
            //{
            //    var addedDay = new Day(dateTime);
            //    Days.Add(addedDay);
            //    Console.WriteLine("added day: " + addedDay.dateTime);
            //    return addedDay.IdOfDay;
            //} 
        }

        public int AddProductToMeal(ItemService itemService)
        {
            double howManyGrmasOfProduct;
            var dayHolder = GetDay();
            var mealHolder = GetMealFromDay(dayHolder);
            var itemHolder = itemService.GetItem();
            
            do
            {
                Console.Clear();
                Console.WriteLine("Type how many grams of choosen product You want to add...");
            }
            while (!double.TryParse(Console.ReadLine(), out howManyGrmasOfProduct));

            var itemInMealHolder = new ItemInMeal(itemHolder, howManyGrmasOfProduct);

            mealHolder.ProductsInMeal.Add(itemInMealHolder);
            RecalculateMacrosInMeal(mealHolder);
            RecalculateMacrosInDay(dayHolder);

            Console.WriteLine("Added grams of product: " + itemInMealHolder.Weight);
            Console.WriteLine("Total Carbs: " + mealHolder.TotalCarbs);
            Console.WriteLine("Total fat: " + mealHolder.TotalFat);
            Console.WriteLine("Total kcal: " + mealHolder.TotalKcal);
            Console.WriteLine("Total protein: " + mealHolder.TotalProtein);

            return itemInMealHolder.Id;
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
            var mealToDelete = GetMealFromDay(dayHolder);

            dayHolder.mealList.Remove(mealToDelete);

            return mealToDelete.Id;
        }

        public void ShowDayMacro()
        {
            var dayHolder = GetDay();

            Console.WriteLine($"Id: {dayHolder.Id}, date: {dayHolder.dateTime}, total carb: {dayHolder.TotalCarbs}" +
                $", total fat: {dayHolder.TotalFat}, total protein: {dayHolder.TotalProtein}, total kcal: {dayHolder.TotalKcal} ");
        }

        public int DeleteDay()
        {
            var dayHolder = GetDay();
            Days.Remove(dayHolder);
            return dayHolder.Id;
        }

        public void RemoveProductFromMeal(MealService mealService)
        {
            var dayHolder = GetDay();
            var mealHolder = GetMealFromDay(dayHolder);
            int choosenIdOfItem;
            do
            {
                Console.Clear();
                Console.WriteLine("Please type product ID, which you want to delete from meal...");
                mealService.ShowAllProductsFromMeal(mealHolder);
            }
            while (!int.TryParse(Console.ReadLine(), out choosenIdOfItem) || !mealHolder.ProductsInMeal.Contains(mealHolder.ProductsInMeal.FirstOrDefault(s => s.Id == choosenIdOfItem)));
            Console.WriteLine("item id to delete meal : " + choosenIdOfItem);
            var itemInMealHolder = mealHolder.ProductsInMeal.FirstOrDefault(s => s.Id == choosenIdOfItem);

            mealHolder.ProductsInMeal.Remove(itemInMealHolder);
            RecalculateMacrosInMeal(mealHolder);
            RecalculateMacrosInDay(dayHolder);

            Console.WriteLine("Total Carbs: " + mealHolder.TotalCarbs);
            Console.WriteLine("Total fat: " + mealHolder.TotalFat);
            Console.WriteLine("Total kcal: " + mealHolder.TotalKcal);
            Console.WriteLine("Total protein: " + mealHolder.TotalProtein);
        }

        public void ShowAllDays()
        {
            foreach (var item in Days)
            {
                Console.WriteLine($"Id: {item.Id}," +
                    $" date: {item.dateTime}," +
                    $" total carb: {Math.Round(item.TotalCarbs,2)}," +
                    $" total fat: {Math.Round(item.TotalFat,2)}," +
                    $" total protein: {Math.Round(item.TotalProtein,2)}," +
                    $" total kcal: {Math.Round(item.TotalKcal,2)} ");
            }
        }

        private bool IsDayExistinginDatabase(DateTime dateTime)
        {
            foreach(var item in Days)
            {
                if (item.dateTime == dateTime)
                    Console.WriteLine($"This day existing in Your app: {dateTime.Date.ToShortDateString()}");
                    return false;
            }
            return true;
        }

        public void ShowAllMealsFromDay(int choosenIdOfDay)
        {
            var currentDay = Days.FirstOrDefault(s => s.Id == choosenIdOfDay);
            foreach (var item in currentDay.mealList)
            {
                Console.WriteLine($"Id: {item.Id}, name of meal: {item.NameOfMeal}");
            }
        }
        public void AddNewDayMixed()
        {
            int[] days = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30 };
            int[] months = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
            int[] years = { 2018, 2019, 2020, 2021, 2022, 2023 };

            Random rnd = new Random();

            for (int i = 0; i < 10; i++)
            {
                DateTime dateTime = new DateTime(days[rnd.Next(1, 30)], months[rnd.Next(1, 12)], days[rnd.Next(1, 6)]);
                Days.Add(new Day(dateTime));
            }
        }

        public void AddNewMealMixed(MealService mealService, ItemService itemService)        
        {
            Random rnd = new Random();
            string[] names = { "Breakfast", "Lunch", "Dinner", "Evening food :)" };
            foreach(var item in Days)
            {
                for (int i = 0; i < names.Length; i++)
                {
                    item.mealList.Add(new Meal(names[i]));
                }
            }
        }

        public void AddProductsToMealsMixed(MealService mealService, ItemService itemService)
        {
            Random rnd = new Random();
            double howManyGramsOfProduct = 0;
            double[] grams = { 20.3, 30.9, 40.3, 50.1, 60.8, 70.9, 80.4, 90.8 };
            foreach (var item in Days)
            {
                for (int i = 0; i < item.mealList.Count; i++)
                {
                    var mealHolder = item.mealList[i];
                    var itemHolder = new ItemInMeal(itemService.Items[rnd.Next(1, itemService.Items.Count)], grams[rnd.Next(1,8)]);

                    mealHolder.ProductsInMeal.Add(itemHolder);
                    howManyGramsOfProduct = grams[rnd.Next(1, 7)];


                    mealHolder.TotalCarbs += Math.Round(itemHolder.Carb * howManyGramsOfProduct / 100, 2);
                    mealHolder.TotalFat += Math.Round(itemHolder.Fat * howManyGramsOfProduct / 100, 2);
                    mealHolder.TotalKcal += Math.Round(itemHolder.Kcal * howManyGramsOfProduct / 100, 2);
                    mealHolder.TotalProtein += Math.Round(itemHolder.Protein * howManyGramsOfProduct / 100, 2);

                    item.TotalCarbs += Math.Round(mealHolder.TotalCarbs, 2);
                    item.TotalFat += Math.Round(mealHolder.TotalFat, 2);
                    item.TotalKcal += Math.Round(mealHolder.TotalKcal, 2);
                    item.TotalProtein += Math.Round(mealHolder.TotalProtein, 2);

                }
            }
        }

        public Day GetDay()
        {
            int choosenIdOfDay;
            do
            {
                Console.WriteLine("Type ID day You want to choose...");
                ShowAllDays();
            }
            while (!int.TryParse(Console.ReadLine(), out choosenIdOfDay) || !Days.Contains(Days.FirstOrDefault(s => s.Id == choosenIdOfDay)));

            return Days.FirstOrDefault(s => s.Id == choosenIdOfDay);
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
        private void RecalculateMacrosInMeal(Meal meal)
        {
            meal.TotalCarbs = 0;
            meal.TotalFat = 0;
            meal.TotalGrams = 0;
            meal.TotalKcal = 0;
            meal.TotalProtein = 0;
            foreach (var element in meal.ProductsInMeal)
            {
                meal.TotalCarbs += Math.Round(element.Carb * element.Weight / 100, 2);
                meal.TotalFat += Math.Round(element.Fat * element.Weight / 100, 2);
                meal.TotalGrams += Math.Round(element.Weight, 2);
                meal.TotalKcal += Math.Round(element.Kcal * element.Weight / 100, 2);
                meal.TotalProtein += Math.Round(element.Protein * element.Weight / 100, 2);
            }
        } 
        private void RecalculateMacrosInDay(Day day)
        {
            day.TotalCarbs = 0;
            day.TotalFat = 0;
            day.TotalKcal = 0;
            day.TotalProtein = 0;

            foreach (var item in day.mealList)
            {
                day.TotalCarbs += item.TotalCarbs;
                day.TotalFat += item.TotalFat;
                day.TotalKcal += item.TotalKcal;
                day.TotalProtein += item.TotalProtein;
                day.TotalWeightInGrams += item.TotalGrams;
            }
        }
    }
}
