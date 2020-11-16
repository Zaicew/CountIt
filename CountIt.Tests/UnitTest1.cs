using CountIt.App.Abstract;
using CountIt.App.Concrete;
using CountIt.App.Managers;
using CountIt.Domain.Entity;
using FluentAssertions;
using FluentAssertions.Common;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.DataCollection;
using Moq;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using Xunit;

namespace CountIt.Tests
{
    public class UnitTest1
    {
        //testing methods from ICategoryService

        [Fact]
        public void IsCategoryNameExist_Test()
        {
            //Arrange
            var categoryService = new CategoryService();
            var trueCategory = new Category("true", 0);
            var fakeCategory = new Category("false", 1);

            categoryService.AddItem(trueCategory);

            //Act
            var trueOutput = categoryService.IsCategoryNameExist(trueCategory.Name);
            var falseOutput = categoryService.IsCategoryNameExist(fakeCategory.Name);

            //Assert
            trueOutput.Should().BeTrue();
            falseOutput.Should().BeFalse();
        }
        [Fact]
        public void GetCategoryByName_Test()
        {
            //Arrange
            var categoryService = new CategoryService();
            var categoryHolder = new Category("Milky", 2);

            categoryService.Items.Add(categoryHolder);

            //Act
            var output = categoryService.GetCategoryByName("Milky");
            //Assert
            output.Should().NotBeNull();
            output.Should().BeOfType<Category>();
            output.Should().BeSameAs(categoryHolder);
        }
        [Fact]
        public void CreateCategory_Test()
        {
            //Arrange
            var categoryService = new CategoryService();
            categoryService.Items.Add(new Category("", 100));

            //Act
            var output = categoryService.CreateCategory("");

            //Assert
            output.Should().NotBeNull();
            output.Should().BeOfType<Category>();
            output.Id.Should().IsSameOrEqualTo(101);
            categoryService.Items.Should().Contain(output);            
        }
        [Fact]
        public void GetCategoryById_Test()
        {
            //Arrange
            var categoryService = new CategoryService();
            var trueCategory = new Category("trueName", 10);
            var falseCategory = new Category("falseName", 20);

            categoryService.AddItem(trueCategory);
            //Act
            var trueOutput = categoryService.GetCategoryById(10);
            var falseOutput = categoryService.GetCategoryById(20);

            //Assert
            trueOutput.Should().NotBeNull();
            trueOutput.Should().BeOfType<Category>();
            trueOutput.Id.Should().IsSameOrEqualTo(10);
            trueOutput.Name.Should().BeSameAs("trueName");

            falseOutput.Should().BeNull();
            categoryService.Items.Should().NotContain(falseCategory);
        }

        //testing methods from IDayService

        [Fact]
        public void CreateNewDayByDateTime_Test()
        {
            //Arrange
            var dayService = new DayService();
            var mealService = new MealService();
            dayService.Items.Add(new Day(new DateTime(2020, 10, 10), 100));
            var dateTime = new DateTime(2020, 08, 18);

            //Act
            var output = dayService.CreateNewDayByDateTime(dateTime, mealService);

            //Assert
            output.Should().NotBeNull();
            output.Should().BeOfType<Day>();
            output.Id.Should().IsSameOrEqualTo(101);
            dayService.Items.Should().Contain(output);
        }
        [Fact]
        public void RecalculateMacrosInDay_Test()
        {
            //Arrange
            var dayService = new DayService();
            var mealService = new MealService();
            var day = new Day(new DateTime(2020, 10, 15), 1); 
            dayService.Items.Add(day);
            day.mealList = dayService.AddDomainMealsToDay(mealService);
            day.mealList[0].ProductsInMeal.Add(new ItemInMeal(new Item(1, "", 100, 100, 100, 100, 1), 9000));
            dayService.RecalculateMacrosInMeal(day.mealList[0]);
            //Act
            dayService.RecalculateMacrosInDay(day);
            //Assert
            day.TotalKcal.IsSameOrEqualTo(900000);
        }
        [Fact]
        public void RecalculateMacrosInMeal_Test()
        {
            //Arrange
            var dayService = new DayService();
            var meal = new Meal("", 1);
            meal.ProductsInMeal.Add(new ItemInMeal(new Item(1, "", 50, 50, 50, 50, 1), 100));
            //Act
            dayService.RecalculateMacrosInMeal(meal);
            //Assert
            meal.Weight.IsSameOrEqualTo(100);
            meal.TotalKcal.IsSameOrEqualTo(50);
        }
        [Fact]
        public void AddProductToMeal()
        {
            //Arrange
            var itemInMeal = new ItemInMeal(new Item(10, "name", 100, 100, 100, 100, 0), 50);
            var day = new Day(new DateTime(2020, 12, 12), 10);
            var dayService = new DayService();
            var mealService = new MealService();
            day.mealList = dayService.AddDomainMealsToDay(mealService);
            //Act
            var output = dayService.AddProductToMeal(itemInMeal, day.mealList[0], day);
            //Assert
            output.Should().NotBe(null);
            day.mealList[0].Should().NotBe(null);
            day.mealList[0].NameOfMeal.Should().BeSameAs("Breakfast");
            day.mealList[0].ProductsInMeal.Should().NotBeNullOrEmpty();
            day.mealList[0].ProductsInMeal.Should().Contain(itemInMeal);
        }
        [Fact]
        public void RemoveProductFromMeal()
        {
            //Arrange
            ItemInMeal item = new ItemInMeal(new Item(10, "", 50, 50, 50, 50, 0), 500);
            MealService mealService = new MealService();
            DayService dayService = new DayService();
            Meal meal = new Meal("Name", 10);
            meal.ProductsInMeal.Add(item);
            Day day = new Day(new DateTime(2020, 10, 10), 10);
            day.mealList = dayService.AddDomainMealsToDay(mealService);
            day.mealList[0] = meal;
            dayService.RecalculateMacrosInMeal(meal);
            Debug.WriteLine("***************************************************************************");

            Debug.WriteLine($"MEAL Macros: {meal.TotalCarbs}, {meal.TotalFat}, {meal.TotalKcal}, {meal.TotalProtein}.");
            dayService.RecalculateMacrosInDay(day);
            Debug.WriteLine($"DAY Macros: {day.TotalCarbs}, {day.TotalFat}, {day.TotalKcal}, {day.TotalProtein}.");


            //Act
            var output = dayService.RemoveProductFromMeal(item, meal, day);
            Debug.WriteLine("***************************************************************************");

            Debug.WriteLine($"MEAL Macros: {meal.TotalCarbs}, {meal.TotalFat}, {meal.TotalKcal}, {meal.TotalProtein}.");
            Debug.WriteLine($"DAY Macros: {day.TotalCarbs}, {day.TotalFat}, {day.TotalKcal}, {day.TotalProtein}.");
            //Assert
            output.IsSameOrEqualTo(10);
            day.mealList[0].ProductsInMeal.Should().NotContain(item);
        }
        [Fact]
        public void HideMeal_Test()
        {            
            //Arrange
            var itemInMeal = new ItemInMeal(new Item(10, "name", 100, 100, 100, 100, 0), 50);
            var day = new Day(new DateTime(2020, 12, 12), 10);
            var dayService = new DayService();
            var mealService = new MealService();
            day.mealList = dayService.AddDomainMealsToDay(mealService);
            //Act
            var outputTrue = dayService.HideMeal(day.mealList[0], day);
            var outputFalse = dayService.HideMeal(day.mealList[0], day);
            dayService.HideMeal(day.mealList[1], day);
            dayService.HideMeal(day.mealList[2], day);
            dayService.HideMeal(day.mealList[3], day);
            dayService.HideMeal(day.mealList[4], day);
            var outputVisibleDaysAreLessThanTwo = dayService.HideMeal(day.mealList[5], day);
            //Assert
            outputTrue.Should().Be(day.mealList[0].Id);
            outputFalse.Should().Be(-1);
            outputVisibleDaysAreLessThanTwo.Should().Be(-2);

        }
        [Fact]
        public void AddDomainMealsInDay()
        {
            //Arrange
            var dayService = new DayService();
            var mealService = new MealService();
            //Act
            var output = dayService.AddDomainMealsToDay(mealService);
            //Assert
            output.Should().NotBeNullOrEmpty();
            output.Length.Should().Be(6);
            output[0].NameOfMeal.Should().Be("Breakfast");
            output[1].NameOfMeal.Should().Be("Second Breakfast");
            output[2].NameOfMeal.Should().Be("Lunch");
            output[3].NameOfMeal.Should().Be("Midday Meal");
            output[4].NameOfMeal.Should().Be("Snack");
            output[5].NameOfMeal.Should().Be("Dinner");
        }
        [Fact]
        public void HowManyMealsAreVisibleInDay()
        {
            //Arrange
            var day = new Day(new DateTime(2020, 12, 12), 10);
            var dayService = new DayService();
            var mealService = new MealService();
            day.mealList = dayService.AddDomainMealsToDay(mealService);
            //Act
            var outputFull = dayService.HowManyMealsAreVisibleInDay(day);
            day.mealList[0].IsVisible = false;
            day.mealList[1].IsVisible = false;
            day.mealList[2].IsVisible = false;
            day.mealList[3].IsVisible = false;
            day.mealList[4].IsVisible = false;
            day.mealList[5].IsVisible = false;
            var outputNull = dayService.HowManyMealsAreVisibleInDay(day);
            //Assert
            outputFull.Should().Be(6);
            outputNull.Should().Be(0);
        }


        //testing methods from IItemService

        [Fact]
        public void SignDefaultCategoryForAllProductsFromDeletingOne_Test()
        {            
            //Arrange
            var categoryToDelete = new Category("Delete", 1);
            var item1 = new Item(1, "Name", 10, 10, 10, 10, 1);
            var item2 = new Item(2, "Name", 10, 10, 10, 10, 2);
            var itemService = new ItemService();
            itemService.Items.Add(item1);
            itemService.Items.Add(item2);
            //Act
            itemService.SignDefaultCategoryForAllProductsFromDeletingOne(categoryToDelete);
            //Assert
            itemService.Items.FirstOrDefault(s => s.Id == 1).CategoryId.Should().Be(0);
            itemService.Items.FirstOrDefault(s => s.Id == 2).CategoryId.Should().Be(2);
        }
        [Fact]
        public void AddItemByNesseseryData_Test()
        {
            //Arrange
            var itemService = new ItemService();
            itemService.AddItem(new Item(100));
            //Act
            var output = itemService.AddItemByNesseseryData("", 1, 1, 1, 1, new Category("name", 1));
            //Assert
            output.Should().NotBe(null);
            output.Should().Be(101);
        }

        //first tests

        //[Fact]
        //public void Test1()
        //{
        //    //Arrange
        //    Item item = new Item(150, "Cos", 20, 20, 20, 20, 2);
        //    var mock = new Mock<IService<Item>>();
        //    mock.Setup(s => s.GetItemById(150)).Returns(item);

        //    var manager = new ItemManager(new CategoryService(), mock.Object);
        //    //Act

        //    var returnedItem = manager.GetItemById(item.Id);
        //    //Assert

        //    Assert.Equal(item, returnedItem);
        //}
        [Fact]
        public void Mojpierwszytest()
        {
            //Arrange
            Item item = new Item(150, "Cos", 20, 20, 20, 20, 2);
            var mock = new Mock<IService<Item>>();
            mock.Setup(s => s.AddItem(item)).Returns(item.Id);

            var manager = new ItemService();
            //Act

            int returnedItem = manager.AddItem(item);
            //Assert

            returnedItem.Should().Be(item.Id);
            returnedItem.Should().NotBe(null);
        }
        //[Fact]
        //public void IsDayExistingInDatabaseTest()
        //{
        //    //Arrange
        //    var dayTrue = new Day(new DateTime(2020, 09, 10), 1);
        //    var dayFalse = new Day(new DateTime(2020, 09, 11), 2);

        //    var mock = new Mock<IDayService<Day>>();
        //    //mock.Object.Items.Add(dayTrue);
        //    //var day = mock.Object.AddItem(dayTrue);
        //    //mock.Object.AddItem(new Day(new DateTime(2020, 09, 09), 3));
        //    //mock.Object.AddItem(new Day(new DateTime(2020, 09, 08), 4));
        //    //mock.Object.AddItem(new Day(new DateTime(2020, 09, 07), 3));
        //    mock.Setup(s => s.AddItem(dayTrue)).Returns(1);
        //    DayManager dayManager = new DayManager(mock.Object);

        //    //Act
        //    var trueAnswer = dayManager.IsDayExistinginDatabase(dayTrue.DateTime);
        //    var falseAnswer = dayManager.IsDayExistinginDatabase(dayTrue.DateTime);

        //    //Assert
        //    trueAnswer.Should().BeFalse();
        //    //falseAnswer.Should().BeFalse();
        //}
        [Fact]
        public void IsDayExistingInDatabaseTest()
        {
            //Arrange

            var categoryTrue = new Category("Mleko", 1);
            var categoryService = new CategoryService();
            categoryService.Items.Add(categoryTrue);

            //Act
            var trueAnswer = categoryService.IsCategoryNameExist("Mleko");
            var falseAnswer = categoryService.IsCategoryNameExist(It.IsAny<string>());

            //Assert
            trueAnswer.Should().BeTrue();
            falseAnswer.Should().BeFalse();
        }
        [Fact]
        public void TestingMetod_Test()
        {
            //Arrange
            var categoryMock = new CategoryService();
            var itemMock = new ItemService();

            var categoryManager = new CategoryManager(categoryMock, itemMock);
            //Act
            var output = categoryManager.TestingMethod();

            //Assert
            output.Should().BeTrue();
        }               
    }
}
