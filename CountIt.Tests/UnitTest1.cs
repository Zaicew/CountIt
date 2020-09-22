using CountIt.App.Abstract;
using CountIt.App.Concrete;
using CountIt.App.Managers;
using CountIt.Domain.Entity;
using FluentAssertions;
using Moq;
using System;
using System.ComponentModel;
using Xunit;

namespace CountIt.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            //Arrange
            Item item = new Item(150, "Cos", 20, 20, 20, 20, 2);
            var mock = new Mock<IService<Item>>();
            mock.Setup(s => s.GetItemById(150)).Returns(item);

            var manager = new ItemManager(new CategoryService(), mock.Object);
            //Act

            var returnedItem = manager.GetItemById(item.Id);
            //Assert

            Assert.Equal(item, returnedItem);
        }

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

        //    var mock = new Mock<IService<Day>>();
        //    mock.Object.Items.Add(dayTrue);
        //    var day = mock.Object.AddItem(dayTrue);
        //    //mock.Object.AddItem(new Day(new DateTime(2020, 09, 09), 3));
        //    //mock.Object.AddItem(new Day(new DateTime(2020, 09, 08), 4));
        //    //mock.Object.AddItem(new Day(new DateTime(2020, 09, 07), 3));
        //    //mock.Setup(s => s.AddItem(dayTrue)).Returns(1)
        //    DayManager dayManager = new DayManager(mock.Object);

        //    //Act
        //    var trueAnswer = dayManager.IsDayExistinginDatabase(dayTrue.DateTime);
        //    var falseAnswer = dayManager.IsDayExistinginDatabase(dayTrue.DateTime);

        //    //Assert
        //    trueAnswer.Should().BeTrue();
        //    falseAnswer.Should().BeFalse();
        //}

        [Fact]
        public void IsDayExistingInDatabaseTest_Senaszel()
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
            //var categoryMock = new Mock<CategoryService>();
            var categoryMock = new CategoryService();
            var itemMock = new ItemService();

            //categoryMock.Setup(s => s.IsCategoryNameExist("as")).Returns(false);

            var categoryManager = new CategoryManager(categoryMock, itemMock);
            //Act
            var output = categoryManager.TestingMethod();

            //Assert
            output.Should().BeTrue();
        }        
        [Fact]
        public void GetCategoryByName_Test()
        {
            //Arrange
            //var categoryMock = new Mock<CategoryService>();
            //var categoryHolder = new Category("Milky", 2);
            var categoryService = new CategoryService();
            var categoryHolder = new Category("Milky", 2);

            //categoryMock.Object.Items.Add(categoryHolder);
            categoryService.Items.Add(categoryHolder);
                      
            //categoryMock.Setup(s => s.GetCategoryByName("Milky")).Returns(categoryHolder);

            //Act
            var output = categoryService.GetCategoryByName("Milky");
            //Assert
            output.Should().NotBeNull();
            output.Should().BeOfType<Category>();
            output.Should().BeSameAs(categoryHolder);
        }
    }
}
