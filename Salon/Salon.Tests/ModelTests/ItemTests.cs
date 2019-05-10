
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
            DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=salon_test;";
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
      

    }
}
