using Microsoft.VisualStudio.TestTools.UnitTesting;
using Salon.Models;
using System.Collections.Generic;
using System;

namespace Salon.Tests
{
    [TestClass]
    public class CategoryTest : IDisposable
    {

        public void Dispose()
        {
            Category.ClearAll();
            Item.ClearAll();
        }
        public CategoryTest()
        {
            DBConfiguration.ConnectionString = @"server=localhost;user id=root;password=root;port=8889;database=michael_larragueta_test;";
        }
        [TestMethod]
        public void CategoryConstructor_CreatesInstanceOfCategory_Category()
        {
            Category newCategory = new Category("test category");
            Assert.AreEqual(typeof(Category), newCategory.GetType());
        }
        [TestMethod]
        public void GetName_ReturnsName_String()
        {
            //Arrange
            string name = "Test Category";
            Category newCategory = new Category(name);

            //Act
            string result = newCategory.GetName();

            //Assert
            Assert.AreEqual(name, result);
        }
        public void GetAll_ReturnsAllCategoryObjects_CategoryList()
        {
            //Arrange
            string name01 = "Work";
            string name02 = "School";
            Category newCategory1 = new Category(name01);
            newCategory1.Save();
            Category newCategory2 = new Category(name02);
            newCategory2.Save();
            List<Category> newList = new List<Category> { newCategory1, newCategory2 };

            //Act
            List<Category> result = Category.GetAll();

            //Assert
            CollectionAssert.AreEqual(newList, result);
        }
        [TestMethod]
        public void GetItems_ReturnsEmptyItemList_ItemList()
        {
            //Arrange
            string name = "Work";
            Category newCategory = new Category(name);
            List<Item> newList = new List<Item> { };

            //Act
            List<Item> result = newCategory.GetItems();

            //Assert
            CollectionAssert.AreEqual(newList, result);

        }
        [TestMethod]
        public void GetAll_CategoriesEmptyAtFirst_List()
        {
            //Arrange, Act
            int result = Category.GetAll().Count;

            //Assert
            Assert.AreEqual(0, result);
        }
        [TestMethod]
        public void Equals_ReturnsTrueIfNamesAreTheSame_Category()
        {
            //Arrange, Act
            Category firstCategory = new Category("Ben");
            Category secondCategory = new Category("Ben");

            //Assert
            Assert.AreEqual(firstCategory, secondCategory);
        }

        [TestMethod]
        public void Save_SavesCategoryToDatabase_CategoryList()
        {
            //Arrange
            Category testCategory = new Category("Ben");
            testCategory.Save();

            //Act
            List<Category> result = Category.GetAll();
            List<Category> testList = new List<Category> { testCategory };

            //Assert
            CollectionAssert.AreEqual(testList, result);
        }

        [TestMethod]
        public void Save_DatabaseAssignsIdToCategory_Id()
        {
            //Arrange
            Category testCategory = new Category("Ben");
            testCategory.Save();

            //Act
            Category savedCategory = Category.GetAll()[0];

            int result = savedCategory.GetId();
            int testId = testCategory.GetId();

            //Assert
            Assert.AreEqual(testId, result);
        }

        [TestMethod]
        public void Find_ReturnsCategoryInDatabase_Category()
        {
            //Arrange
            Category testCategory = new Category("Ben");
            testCategory.Save();

            //Act
            Category foundCategory = Category.Find(testCategory.GetId());

            //Assert
            Assert.AreEqual(testCategory, foundCategory);
        }


    }
}
