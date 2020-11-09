using CountIt.App.Common;
using CountIt.App.Concrete;
using CountIt.Domain.Common;
using CountIt.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace CountIt.App.Abstract
{
    public interface IDayService<T> : IService<T> where T : BaseEntity
    {
        int RemoveProductFromMeal(ItemInMeal item, Meal meal, Day day);
        Day CreateNewDayByDateTime(DateTime datetime, IMealService<Meal> mealService);
        void RecalculateMacrosInMeal(Meal meal);
        void RecalculateMacrosInDay(Day day);
        int AddProductToMeal(ItemInMeal item, Meal meal, Day day);
        int HideMeal(Meal meal, Day day);
        Meal[] AddDomainMealsToDay(IMealService<Meal> mealService);
    }
}
