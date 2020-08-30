using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Xml.Schema;

namespace CountIt
{
    class DayService
    {
        public List<Meal> MealsListInDay { get; set; }
        public List<Day> Days { get; set; }

        public DayService()
        {
            //MealsListInDay = new List<Meal>();
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

        public int AddNewMeal(MealService mealService)
        {
            var choosenDay = GetDay();

            Console.WriteLine("Please type name of meal...");
            string nameOfMeal = Console.ReadLine();
            if (choosenDay.mealList.Contains(choosenDay.mealList.FirstOrDefault(s => s.nameOfMeal == nameOfMeal)))
            {
                Console.WriteLine($"Current day contain that name of meal! {nameOfMeal}");
                return MealsListInDay.FirstOrDefault(s => s.nameOfMeal == nameOfMeal).Id;
            }
            else
            {
                var addedMeal = new Meal(nameOfMeal);
                choosenDay.mealList.Add(addedMeal);
                return choosenDay.mealList.FirstOrDefault(s => s.nameOfMeal == nameOfMeal).Id;
            }
        }
        public int AddNewDay()
        {
            
            int day, month, year;
            Console.Clear();
            //do
            //{
            Console.WriteLine("Type day of month... (example: '18')");
                int.TryParse(Console.ReadLine(), out day);
            Console.WriteLine("Type number of month... (example: '3')");
                int.TryParse(Console.ReadLine(), out month);
            Console.WriteLine("Type year... (example: '1998')");
                int.TryParse(Console.ReadLine(), out year);
            //}
            //while (int.TryParse(Console.ReadLine(), out day) || int.TryParse(Console.ReadLine(), out month) || int.TryParse(Console.ReadLine(), out year));
            DateTime dateTime = new DateTime(year, month, day);
            DateTime.TryParse($"{day}/{month}/{year}", out dateTime);
            if (!IsDayExistinginDatabase(dateTime))
            {
                Console.WriteLine($"Your data actually cotains typed day! {dateTime}");
                var existedDay = Days.FirstOrDefault(s => s.dateTime == dateTime);
                Console.WriteLine("existed day: " + existedDay.dateTime);
                return existedDay.IdOfDay;
            }
            else
            {
                var addedDay = new Day(dateTime);
                Days.Add(addedDay);
                Console.WriteLine("added day: " + addedDay.dateTime);
                return addedDay.IdOfDay;
            } 
        }

        public void AddProductToMeal(ItemService itemService)
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

            Console.WriteLine("how many grams: " + howManyGrmasOfProduct);

            mealHolder.ProductsInMeal.Add(itemHolder);
            mealHolder.HowManyGramsOfProduct.Add(howManyGrmasOfProduct);

            RecalculateMacrosInMeal(mealHolder, itemHolder, howManyGrmasOfProduct, '+');
            RecalculateMacrosInDay(dayHolder, mealHolder, '+');

            Console.WriteLine("Added grams of product: " + mealHolder.HowManyGramsOfProduct[0]);
            Console.WriteLine("Total Carbs: " + mealHolder.TotalCarbs);
            Console.WriteLine("Total fat: " + mealHolder.TotalFat);
            Console.WriteLine("Total kcal: " + mealHolder.TotalKcal);
            Console.WriteLine("Total protein: " + mealHolder.TotalProtein);
        }

        internal void ShowMealMacro()
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

            Console.WriteLine($"Id: {dayHolder.IdOfDay}, date: {dayHolder.dateTime}, total carb: {dayHolder.TotalCarbs}" +
                $", total fat: {dayHolder.TotalFat}, total protein: {dayHolder.TotalProtein}, total kcal: {dayHolder.TotalKcal} ");
        }

        private void ShowAllMealsFromDay(Day dayHolder)
        {
            foreach (var item in dayHolder.mealList)
            {
                Console.WriteLine($"ID: {item.Id}, name: {item.nameOfMeal}");
            }
        }

        public int DeleteDay()
        {
            var dayHolder = GetDay();
            Days.Remove(dayHolder);
            return dayHolder.IdOfDay;
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
            var itemHolder = mealHolder.ProductsInMeal.FirstOrDefault(s => s.Id == choosenIdOfItem);

            RecalculateMacrosInMeal(mealHolder, itemHolder, mealHolder.HowManyGramsOfProduct[choosenIdOfItem], '-');
            RecalculateMacrosInDay(dayHolder, mealHolder, '-');

            Console.WriteLine("Total Carbs: " + mealHolder.TotalCarbs);
            Console.WriteLine("Total fat: " + mealHolder.TotalFat);
            Console.WriteLine("Total kcal: " + mealHolder.TotalKcal);
            Console.WriteLine("Total protein: " + mealHolder.TotalProtein);

            mealHolder.HowManyGramsOfProduct.Remove(mealHolder.HowManyGramsOfProduct[choosenIdOfItem]);
            mealHolder.ProductsInMeal.Remove(itemHolder);

        }

        public void ShowAllDays()
        {
            foreach (var item in Days)
            {
                Console.WriteLine($"Id: {item.IdOfDay}," +
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
                    return false;
            }
            return true;
        }

        public void ShowAllMealsFromDay(int choosenIdOfDay)
        {
            var currentDay = Days.FirstOrDefault(s => s.IdOfDay == choosenIdOfDay);
            foreach (var item in currentDay.mealList)
            {
                Console.WriteLine($"Id: {item.Id}, name of meal: {item.nameOfMeal}");
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
                    var itemHolder = itemService.Items[rnd.Next(1, itemService.Items.Count)];

                    mealHolder.ProductsInMeal.Add(itemHolder);
                    howManyGramsOfProduct = grams[rnd.Next(1, 7)];

                    mealHolder.HowManyGramsOfProduct.Add(howManyGramsOfProduct);

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
            while (!int.TryParse(Console.ReadLine(), out choosenIdOfDay) || !Days.Contains(Days.FirstOrDefault(s => s.IdOfDay == choosenIdOfDay)));

            return Days.FirstOrDefault(s => s.IdOfDay == choosenIdOfDay);
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
                    Console.WriteLine($"ID: {item.Id}, name: {item.nameOfMeal}.");
                }
            }
            while (!int.TryParse(Console.ReadLine(), out choosenIdOfMeal) || !day.mealList.Contains(day.mealList.FirstOrDefault(s => s.Id == day.IdOfDay)));
            return day.mealList.FirstOrDefault(s => s.Id == day.IdOfDay);
        }
        private void RecalculateMacrosInMeal(Meal meal, Item item, double grams, char sign)
        {
            if (sign == '+')
            {
                meal.TotalCarbs += Math.Round(item.Carb * grams / 100, 2);
                meal.TotalFat += Math.Round(item.Fat * grams / 100, 2);
                meal.TotalKcal += Math.Round(item.Kcal * grams / 100, 2);
                meal.TotalProtein += Math.Round(item.Protein * grams / 100, 2);
            }
            else
            {
                meal.TotalCarbs -= Math.Round(item.Carb * grams / 100, 2);
                meal.TotalFat -= Math.Round(item.Fat * grams / 100, 2);
                meal.TotalKcal -= Math.Round(item.Kcal * grams / 100, 2);
                meal.TotalProtein -= Math.Round(item.Protein * grams / 100, 2);
            }

        } 
        private void RecalculateMacrosInDay(Day day, Meal meal, char sign)
        {
            if (sign == '+')
            {
                day.TotalCarbs += Math.Round(meal.TotalCarbs, 2);
                day.TotalFat += Math.Round(meal.TotalFat, 2);
                day.TotalKcal += Math.Round(meal.TotalKcal, 2);
                day.TotalProtein += Math.Round(meal.TotalProtein, 2);
            }
            else
            {
                day.TotalCarbs -= Math.Round(meal.TotalCarbs, 2);
                day.TotalFat -= Math.Round(meal.TotalFat, 2);
                day.TotalKcal -= Math.Round(meal.TotalKcal, 2);
                day.TotalProtein -= Math.Round(meal.TotalProtein, 2);
            }

        }
    }
}
