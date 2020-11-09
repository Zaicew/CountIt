using CountIt.App.Abstract;
using CountIt.App.Common;
using CountIt.App.Managers;
using CountIt.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Xml.Schema;

namespace CountIt.App.Concrete
{
    public class DayService : BaseService<Day>, IDayService<Day>
    {
        public List<Meal> MealsListInDay { get; set; }
        public DayService() : base()
        {

        }
        public int RemoveProductFromMeal(ItemInMeal item, Meal meal, Day day)
        {
            meal.ProductsInMeal.Remove(item);
            RecalculateMacrosInMeal(meal);
            RecalculateMacrosInDay(day);
            return item.Id;
        }
        //public int DeleteMeal(Meal meal, Day day)
        //{
        //    day.mealList.Remove(meal);
        //    return meal.Id;
        //}
        public int AddProductToMeal(ItemInMeal item, Meal meal, Day day)
        {
            meal.ProductsInMeal.Add(item);
            RecalculateMacrosInMeal(meal);
            RecalculateMacrosInDay(day);
            return item.Id;
        }
        //public int AddNewMealinDay(string name, Day day)
        //{
        //    var id =  GetLastId() + 1;
        //    var mealHolder = new MealInDay(name, id);
        //    day.mealList.Add(mealHolder);
        //    return mealHolder.Id;
        //}
        public Day CreateNewDayByDateTime(DateTime dateTime, IMealService<Meal> mealService)
        {
            var id = GetLastId() + 1;
            var dayHolder = new Day(dateTime, id);
            dayHolder.mealList =  AddDomainMealsToDay(mealService);
            Items.Add(dayHolder);
            return dayHolder;
        }
        public void RecalculateMacrosInMeal(Meal meal)
        {
            meal.TotalCarbs = 0;
            meal.TotalFat = 0;
            meal.Weight = 0;
            meal.TotalKcal = 0;
            meal.TotalProtein = 0;
            foreach (var element in meal.ProductsInMeal)
            {
                meal.TotalCarbs += Math.Round(element.Carb * element.Weight / 100, 2);
                meal.TotalFat += Math.Round(element.Fat * element.Weight / 100, 2);
                meal.Weight += Math.Round(element.Weight, 2);
                meal.TotalKcal += Math.Round(element.Kcal * element.Weight / 100, 2);
                meal.TotalProtein += Math.Round(element.Protein * element.Weight / 100, 2);
            }
        }
        public void RecalculateMacrosInDay(Day day)
        {
            day.TotalCarbs = 0;
            day.TotalFat = 0;
            day.TotalKcal = 0;
            day.TotalProtein = 0;

            foreach(var item in day.mealList)
            {
                if (item.IsVisible)
                {
                    day.TotalCarbs += item.TotalCarbs;
                    day.TotalFat += item.TotalFat;
                    day.TotalKcal += item.TotalKcal;
                    day.TotalProtein += item.TotalProtein;
                    day.TotalWeightInGrams += item.Weight;
                }
            }
        }
        public Meal[] AddDomainMealsToDay(IMealService<Meal> mealService)
        {
            Meal[] mealList = new Meal[6];
            var meal = new Meal("Breakfast", mealService.GetLastId() + 1);
            mealService.Items.Add(meal);
            mealList[0] = meal;
            meal = new Meal("Second Breakfast", mealService.GetLastId() + 1);
            mealService.Items.Add(meal);
            mealList[1] = meal;
            meal = new Meal("Lunch", mealService.GetLastId() + 1);
            mealService.Items.Add(meal);
            mealList[2] = meal;
            meal = new Meal("Midday Meal", mealService.GetLastId() + 1);
            mealService.Items.Add(meal);
            mealList[3] = meal;
            meal = new Meal("Snack", mealService.GetLastId() + 1);
            mealService.Items.Add(meal);
            mealList[4] = meal;
            meal = new Meal("Dinner", mealService.GetLastId() + 1);
            mealService.Items.Add(meal);
            mealList[5] = meal;

            return mealList;
        }
        public int HideMeal(Meal meal, Day day)
        {
            if (meal.IsVisible == false)
            {
                Console.WriteLine("This meal is not visible!");
                return -1;
            }
            if (HowManyMealsAreVisibleInDay(day) < 2)
            {
                Console.WriteLine("We should to have at least one active meal in day!");
                return -2;
            }

            meal.IsVisible = false;
            RecalculateMacrosInMeal(meal);
            RecalculateMacrosInDay(day);
            return meal.Id;
        }
        public int HowManyMealsAreVisibleInDay(Day day)
        {
            int counter = 0;
            foreach (var item in day.mealList)
                if (item.IsVisible == true)
                    counter++;
            return counter;
        }
    }
}
