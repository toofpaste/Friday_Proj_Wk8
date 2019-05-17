
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Salon.Models;
using System.Collections.Generic;
using System;
using MySql.Data.MySqlClient;

namespace Salon.Tests
{
    [TestClass]
    public class ItemTest : IDisposable
    {

        public void Dispose()
        {
            Item.ClearAll();
        }

        public ItemTest()
        {
            DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=michael_larragueta_test;";
        }


        [TestMethod]
        public void ItemConstructor_CreatesInstanceOfItem_Item()
        {

            DateTime dd = new DateTime(2011, 6, 10);
            int price = 50;
            string testUrl = "https://i.imgur.com/BBcy6Wc.jpg";
            int catId = 0;
            Item newItem = new Item("Bob", dd, catId, dd, "buzz", testUrl, price);
            Assert.AreEqual(typeof(Item), newItem.GetType());
        }

        [TestMethod]
        public void GetDescription_ReturnsDescription_String()
        {
            int catId = 0;
            int price = 50;
            string testUrl = "https://i.imgur.com/BBcy6Wc.jpg";
            DateTime dd = new DateTime(2011, 6, 10);
            string description = "John";
            Item newItem = new Item(description, dd, catId, dd, "buzz", testUrl, price);
            string result = newItem.GetDescription();
            Assert.AreEqual(description, result);
        }

        [TestMethod]
        public void SetDescription_SetDescription_String()
        {
            int catId = 0;
            int price = 50;
             string testUrl = "https://i.imgur.com/BBcy6Wc.jpg";
            DateTime dd = new DateTime(2011, 6, 10);
            string description = "bob";
            Item newItem = new Item(description, dd, catId, dd, "buzz", testUrl, price);

            string updatedDescription = "john";
            newItem.SetDescription(updatedDescription);
            string result = newItem.GetDescription();

            Assert.AreEqual(updatedDescription, result);
        }

        [TestMethod]
        public void GetAll_ReturnsEmptyListFromDatabase_ItemList()
        {
            List<Item> newList = new List<Item> { };
            List<Item> result = Item.GetAll();
            CollectionAssert.AreEqual(newList, result);
        }


        [TestMethod]
        public void Equals_ReturnsTrueIfDescriptionsAreTheSame_Item()
        {
            int catId = 0;
            int price = 50;
            DateTime dd = new DateTime(2011, 6, 10);
             string testUrl = "https://i.imgur.com/BBcy6Wc.jpg";
            Item firstItem = new Item("Todd", dd, catId, dd, "buzz", testUrl, price);
            Item secondItem = new Item("Todd", dd, catId, dd, "buzz", testUrl, price);
            Assert.AreEqual(firstItem, secondItem);
        }

        [TestMethod]
        public void Delete_DeletesItemAssociationsFromDatabase_ItemList()
        {
          //Arrange
          
            int catId = 0;
            int price = 50;
            DateTime dd = new DateTime(2011, 6, 10);
             string testUrl = "https://i.imgur.com/BBcy6Wc.jpg";
          Category testCategory = new Category("Home stuff", "test");
          testCategory.Save();
          Item testItem = new Item("Todd", dd, catId, dd, "buzz", testUrl, price);
          testItem.Save();

          //Act
          testItem.AddCategory(testCategory);
          testItem.Delete();
          List<Item> resultCategoryItems = testCategory.GetItems();
          List<Item> testCategoryItems = new List<Item> {};

          //Assert
          CollectionAssert.AreEqual(testCategoryItems, resultCategoryItems);
        }
            [TestMethod]
        public void AddCategory_AddsCategoryToItem_CategoryList()
        {
            //Arrange
            
            int catId = 0;
            int price = 50;
            DateTime dd = new DateTime(2011, 6, 10);
             string testUrl = "https://i.imgur.com/BBcy6Wc.jpg";
            Item testItem = new Item("Todd", dd, catId, dd, "buzz", testUrl, price);
            testItem.Save();
            Category testCategory = new Category("Home stuff", "test");
            testCategory.Save();

            //Act
            testItem.AddCategory(testCategory);

            List<Category> result = testItem.GetCategories();
            List<Category> testList = new List<Category>{testCategory};

            //Assert
            CollectionAssert.AreEqual(testList, result);
        }
        [TestMethod]
    public void GetCategories_ReturnsAllItemCategories_CategoryList()
    {
      //Arrange
      
            int catId = 0;
            int price = 50;
            DateTime dd = new DateTime(2011, 6, 10);
             string testUrl = "https://i.imgur.com/BBcy6Wc.jpg";
            Item testItem = new Item("Todd", dd, catId, dd, "buzz", testUrl, price);
      testItem.Save();
      Category testCategory1 = new Category("Home stuff", "test");
      testCategory1.Save();
      Category testCategory2 = new Category("Work stuff", "test");
      testCategory2.Save();

      //Act
      testItem.AddCategory(testCategory1);
      List<Category> result = testItem.GetCategories();
      List<Category> testList = new List<Category> {testCategory1};

      //Assert
      CollectionAssert.AreEqual(testList, result);
    }
          [TestMethod]
    public void Edit_UpdatesItemInDatabase_String()
    {
      //Arrange
      int catId = 0;
            int price = 50;
            DateTime dd = new DateTime(2011, 6, 10);
             string testUrl = "https://i.imgur.com/BBcy6Wc.jpg";
            Item testItem = new Item("Todd", dd, catId, dd, "buzz", testUrl, price);
      testItem.Save();
      string secondDescription = "Mow the lawn";

      //Act
      testItem.Edit("Mow the lawn", dd, dd, "buzz", testUrl, price);
      string result = Item.Find(testItem.GetId()).GetDescription();

      //Assert
      Assert.AreEqual(secondDescription, result);
    }
        [TestMethod]
    public void Save_AssignsIdToObject_Id()
    {
      //Arrange
      int catId = 0;
        int price = 50;
        DateTime dd = new DateTime(2011, 6, 10);
            string testUrl = "https://i.imgur.com/BBcy6Wc.jpg";
        Item testItem = new Item("Todd", dd, catId, dd, "buzz", testUrl, price);

      //Act
      testItem.Save();
      Item savedItem = Item.GetAll()[0];

      int result = savedItem.GetId();
      int testId = testItem.GetId();

      //Assert
      Assert.AreEqual(testId, result);
    }
}
  


    
}
