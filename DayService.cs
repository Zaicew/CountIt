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
            int choosenIdOfDay;
            do
            {
                Console.WriteLine("Choose ID of day You want to add meal...");
                ShowAllDays();
            }
            while (!int.TryParse(Console.ReadLine(), out choosenIdOfDay) || !Days.Contains(Days.FirstOrDefault(s => s.IdOfDay == choosenIdOfDay)));
            var choosenDay = Days.FirstOrDefault(s => s.IdOfDay == choosenIdOfDay);
            Console.WriteLine("Please type name of meal...");
            string nameOfMeal = Console.ReadLine();
            if (choosenDay.mealList.Contains(choosenDay.mealList.FirstOrDefault(s => s.nameOfMeal == nameOfMeal)))
            {
                Console.WriteLine($"Current day contain that name of meal! {nameOfMeal}");
                var existingNameOfMeal = MealsListInDay.FirstOrDefault(s => s.nameOfMeal == nameOfMeal);
                return existingNameOfMeal.Id;
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
                int.TryParse(Console.ReadLine(), out day);
                int.TryParse(Console.ReadLine(), out month);
                int.TryParse(Console.ReadLine(), out year);
            //}
            //while (int.TryParse(Console.ReadLine(), out day) || int.TryParse(Console.ReadLine(), out month) || int.TryParse(Console.ReadLine(), out year));
            DateTime dateTime = new DateTime(year, month, day);
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
            int choosenIdOfItemToAddToMeal;
            int choosenIdOfDay;
            int choosenIdOfMeal;
            double howManyGrmasOfProduct;
            //itemService.ShowAllProducts();

            do
            {
                Console.Clear();
                Console.WriteLine("Please type day ID, where You want to add product...");
                ShowAllDays();
            }
            while (!int.TryParse(Console.ReadLine(), out choosenIdOfDay) || !Days.Contains(Days.FirstOrDefault(s => s.IdOfDay == choosenIdOfDay)));
            var dayHolder = Days.FirstOrDefault(s => s.IdOfDay == choosenIdOfDay);
            Console.WriteLine("id of day: " + choosenIdOfDay);
            do
            {
                Console.Clear();
                Console.WriteLine("Please type meal ID from day, where You want to add product...");
                foreach (var item in dayHolder.mealList)
                {
                    Console.WriteLine($"ID: {item.Id}, name: {item.nameOfMeal}.");
                }
            }
            while (!int.TryParse(Console.ReadLine(), out choosenIdOfMeal) || !dayHolder.mealList.Contains(dayHolder.mealList.FirstOrDefault(s => s.Id == choosenIdOfDay)));
            var mealHolder = dayHolder.mealList.FirstOrDefault(s => s.Id == choosenIdOfDay);
            Console.WriteLine("id of meal: " + choosenIdOfMeal);
            do
            {
                Console.Clear();
                Console.WriteLine("Please type product ID, which you want to add to meal...");
                itemService.ShowAllProducts();
            }
            while (!int.TryParse(Console.ReadLine(), out choosenIdOfItemToAddToMeal) || !itemService.Items.Contains(itemService.Items.FirstOrDefault(s => s.IdType == choosenIdOfItemToAddToMeal)));
            Console.WriteLine("id item to add meal : " + choosenIdOfItemToAddToMeal);
            var itemHolder = itemService.Items.FirstOrDefault(s => s.IdType == choosenIdOfItemToAddToMeal);
            do
            {
                Console.Clear();
                Console.WriteLine("Type how many grams of choosen product You want to add...");
            }
            while (!double.TryParse(Console.ReadLine(), out howManyGrmasOfProduct));
            Console.WriteLine("how many grams: " + howManyGrmasOfProduct);
            mealHolder.ProductsInMeal.Add(itemService.Items.FirstOrDefault(s => s.IdType == choosenIdOfItemToAddToMeal));

            mealHolder.HowManyGramsOfProduct.Add(howManyGrmasOfProduct);
            mealHolder.TotalCarbs += Math.Round(itemHolder.Carb * howManyGrmasOfProduct / 100, 2);
            mealHolder.TotalFat += Math.Round(itemHolder.Fat * howManyGrmasOfProduct / 100, 2);
            mealHolder.TotalKcal += Math.Round(itemHolder.Kcal * howManyGrmasOfProduct / 100, 2);
            mealHolder.TotalProtein += Math.Round(itemHolder.Protein * howManyGrmasOfProduct / 100, 2);

            dayHolder.TotalCarbs += Math.Round(mealHolder.TotalCarbs, 2);
            dayHolder.TotalFat += Math.Round(mealHolder.TotalFat, 2);
            dayHolder.TotalKcal += Math.Round(mealHolder.TotalKcal, 2);
            dayHolder.TotalProtein += Math.Round(mealHolder.TotalProtein, 2);

            Console.WriteLine("Added day: " + mealHolder.ProductsInMeal.FirstOrDefault(s => s.IdType == choosenIdOfItemToAddToMeal));
            Console.WriteLine("Added grams of product: " + mealHolder.HowManyGramsOfProduct[0]);
            Console.WriteLine("Total Carbs: " + mealHolder.TotalCarbs);
            Console.WriteLine("Total fat: " + mealHolder.TotalFat);
            Console.WriteLine("Total kcal: " + mealHolder.TotalKcal);
            Console.WriteLine("Total protein: " + mealHolder.TotalProtein);
        }

        internal void ShowMealMacro()
        {
            int dayId, mealId;
            do
            {
                Console.WriteLine("Type ID of day from which you want to delete meal...");
                ShowAllDays();
            }
            while (!int.TryParse(Console.ReadLine(), out dayId) || !Days.Contains(Days.FirstOrDefault(s => s.IdOfDay == dayId)));
            var dayHolder = Days.FirstOrDefault(s => s.IdOfDay == dayId);
            do
            {
                Console.WriteLine("Type ID of meal from choosen day you want to delete...");
                ShowAllMealsFromDay(dayHolder);
            }
            while (!int.TryParse(Console.ReadLine(), out mealId) || !dayHolder.mealList.Contains(dayHolder.mealList.FirstOrDefault(s => s.Id == mealId)));
            var mealHolder = dayHolder.mealList.FirstOrDefault(s => s.Id == mealId);

            Console.WriteLine($"(Total's) Carb: {mealHolder.TotalCarbs}, fat: {mealHolder.TotalFat}, protein: {mealHolder.TotalProtein}, kcal: {mealHolder.TotalKcal}. ");
        }

        public int DeleteMeal()
        {
            int dayId, mealIdToDelete;
            do
            {
                Console.WriteLine("Type ID of day from which you want to delete meal...");
                ShowAllDays();
            }
            while (!int.TryParse(Console.ReadLine(), out dayId) || !Days.Contains(Days.FirstOrDefault(s => s.IdOfDay == dayId)));
            var dayHolder = Days.FirstOrDefault(s => s.IdOfDay == dayId);
            do
            {
                Console.WriteLine("Type ID of meal from choosen day you want to delete...");
                ShowAllMealsFromDay(dayHolder);
            }
            while (!int.TryParse(Console.ReadLine(), out mealIdToDelete) || !dayHolder.mealList.Contains(dayHolder.mealList.FirstOrDefault(s => s.Id == mealIdToDelete)));
            var mealToDelete = dayHolder.mealList.FirstOrDefault(s => s.Id == mealIdToDelete);

            dayHolder.mealList.Remove(mealToDelete);

            return dayHolder.IdOfDay;
        }

        public void ShowDayMacro()
        {
            int dayId;
            do
            {
                Console.WriteLine("Type ID of day from which you want to see macros...");
                ShowAllDays();
            }
            while (!int.TryParse(Console.ReadLine(), out dayId) || !Days.Contains(Days.FirstOrDefault(s => s.IdOfDay == dayId)));
            var dayHolder = Days.FirstOrDefault(s => s.IdOfDay == dayId);

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
            int dayIdToDelete;
            do
            {
                Console.WriteLine("Type ID of day you want to delete: ");
                ShowAllDays();
            }
            while (!int.TryParse(Console.ReadLine(), out dayIdToDelete) || !Days.Contains(Days.FirstOrDefault(s => s.IdOfDay == dayIdToDelete)));
            var dayHolder = Days.FirstOrDefault(s => s.IdOfDay == dayIdToDelete);
            Days.Remove(dayHolder);
            return dayHolder.IdOfDay;
        }

        public void RemoveProductFromMeal(MealService mealService)
        {
            int choosenIdOfItemToAddToMeal;
            int choosenIdOfDay;
            int choosenIdOfMeal;
            do
            {
                Console.Clear();
                Console.WriteLine("Please type day ID, where You want to delete product...");
                ShowAllDays();
            }
            while (!int.TryParse(Console.ReadLine(), out choosenIdOfDay) || !Days.Contains(Days.FirstOrDefault(s => s.IdOfDay == choosenIdOfDay)));
            var dayHolder = Days.FirstOrDefault(s => s.IdOfDay == choosenIdOfDay);
            Console.WriteLine("id of day: " + choosenIdOfDay);
            do
            {
                Console.Clear();
                Console.WriteLine("Please type meal ID from day, where You want to delete product...");
                foreach (var item in dayHolder.mealList)
                {
                    Console.WriteLine($"ID: {item.Id}, name: {item.nameOfMeal}.");
                }
            }
            while (!int.TryParse(Console.ReadLine(), out choosenIdOfMeal) || !dayHolder.mealList.Contains(dayHolder.mealList.FirstOrDefault(s => s.Id == choosenIdOfDay)));
            var mealHolder = dayHolder.mealList.FirstOrDefault(s => s.Id == choosenIdOfDay);
            Console.WriteLine("id of meal: " + choosenIdOfMeal);
            do
            {
                Console.Clear();
                Console.WriteLine("Please type product ID, which you want to delete from meal...");
                mealService.ShowAllProductsFromMeal(mealHolder);
            }
            while (!int.TryParse(Console.ReadLine(), out choosenIdOfItemToAddToMeal) || !mealHolder.ProductsInMeal.Contains(mealHolder.ProductsInMeal.FirstOrDefault(s => s.IdType == choosenIdOfItemToAddToMeal)));
            Console.WriteLine("item id to delete meal : " + choosenIdOfItemToAddToMeal);
            var itemHolder = mealHolder.ProductsInMeal.FirstOrDefault(s => s.IdType == choosenIdOfItemToAddToMeal);


            mealHolder.TotalCarbs -= Math.Round(itemHolder.Carb * mealHolder.HowManyGramsOfProduct[choosenIdOfItemToAddToMeal] / 100, 2);
            mealHolder.TotalFat -= Math.Round(itemHolder.Fat * mealHolder.HowManyGramsOfProduct[choosenIdOfItemToAddToMeal] / 100, 2);
            mealHolder.TotalKcal -= Math.Round(itemHolder.Kcal * mealHolder.HowManyGramsOfProduct[choosenIdOfItemToAddToMeal] / 100, 2);
            mealHolder.TotalProtein -= Math.Round(itemHolder.Protein * mealHolder.HowManyGramsOfProduct[choosenIdOfItemToAddToMeal] / 100, 2);

            dayHolder.TotalCarbs -= Math.Round(mealHolder.TotalCarbs, 2);
            dayHolder.TotalFat -= Math.Round(mealHolder.TotalFat, 2);
            dayHolder.TotalKcal -= Math.Round(mealHolder.TotalKcal, 2);
            dayHolder.TotalProtein -= Math.Round(mealHolder.TotalProtein, 2);

            Console.WriteLine("Meal result: " + mealHolder.ProductsInMeal.FirstOrDefault(s => s.IdType == choosenIdOfItemToAddToMeal));
            Console.WriteLine("Deleted grams of product: " + mealHolder.HowManyGramsOfProduct[0]);
            Console.WriteLine("Total Carbs: " + mealHolder.TotalCarbs);
            Console.WriteLine("Total fat: " + mealHolder.TotalFat);
            Console.WriteLine("Total kcal: " + mealHolder.TotalKcal);
            Console.WriteLine("Total protein: " + mealHolder.TotalProtein);

            mealHolder.HowManyGramsOfProduct.Remove(mealHolder.HowManyGramsOfProduct[choosenIdOfItemToAddToMeal]);
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
    }
}
//mealHolder.HowManyGramsOfProduct.Remove(mealHolder.HowManyGramsOfProduct[choosenIdOfItemToAddToMeal]);
//mealHolder.ProductsInMeal.Remove(itemHolder);