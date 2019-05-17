using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Mvc;
using Salon.Models;

namespace Salon.Controllers
{
    public class CategoriesController : Controller
    {

        [HttpGet("/categories")]
        public ActionResult Index()
        {
            List<Category> allCategories = Category.GetAll();
            return View(allCategories);
        }

        [HttpGet("/categories/new")]
        public ActionResult New()
        {
            return View();
        }

        [HttpPost("/categories")]
        public ActionResult Create(string categoryName, string cutInfo)
        {
            Category newCategory = new Category(categoryName, cutInfo);
            newCategory.Save();
            List<Category> allCategories = Category.GetAll();
            return View("Index", allCategories);
        }

        [HttpPost("/category/{categoryId}")]
        public ActionResult Delete(int categoryId)
        {
            Category item = Category.Find(categoryId);
            item.Delete();
            Category newCategory = new Category("dummy", "dummy");
            List<Category> allCategories = Category.GetAll();
            return View("Index", allCategories);
        }
        [HttpGet("/categories/{categoryId}/delete")]
        public ActionResult DeleteOne(int categoryId)
        {
            Dictionary<string, object> model = new Dictionary<string, object>();
            Category item = Category.Find(categoryId);
            model.Add("category", item);
            return View(model);
        }

         [HttpPost("/categories/deleteAll")]
        public ActionResult DeleteA(int itemId)
        { 
            Category.ClearAll();
            Category newCategory = new Category("dummy", "dummy");
            List<Category> allCategories = Category.GetAll();
            return View("Index", allCategories);
        }
        [HttpGet("/categories/deleteAll/delete")]
        public ActionResult DeleteAll(int itemId)
        {
            Dictionary<string, object> model = new Dictionary<string, object>();
            Item item = Item.Find(itemId);
            model.Add("item", item);
            return View(model);
        }


        [HttpGet("/categories/{id}")]
        public ActionResult Show(int id)
        {
            Dictionary<string, object> model = new Dictionary<string, object>();
            Category selectedCategory = Category.Find(id);
            List<Item> categoryItems = selectedCategory.GetItems();
            List<Item> allItems = Item.GetAll();
            model.Add("allItems", allItems);
            model.Add("category", selectedCategory);
            model.Add("items", categoryItems);
            return View(model);
        }

        [HttpPost("/categories/{categoryId}/items")]
        public ActionResult Create(int categoryId, string itemDescription, DateTime shiftDate, DateTime endShift, string cutInfo, string imgUrl, int price)
        {
            Dictionary<string, object> model = new Dictionary<string, object>();
            Category foundCategory = Category.Find(categoryId);
            Item newItem = new Item(itemDescription, shiftDate, categoryId, endShift, cutInfo, imgUrl, price);
            newItem.Save();
            foundCategory.AddItem(newItem);
            List<Item> categoryItems = foundCategory.GetItems();
            model.Add("items", categoryItems);
            model.Add("category", foundCategory);
            return View("Show", model);
        }
        [HttpGet("/categories/{categoryId}/edit")]
        public ActionResult Edit(int categoryId)
        {
            Dictionary<string, object> model = new Dictionary<string, object>();
            Category category = Category.Find(categoryId);
            model.Add("category", category);
            return View(model);
        }
         [HttpPost("/categories/{categoryId}")]
        public ActionResult Update(int categoryId, string newName)
        {
            Category category = Category.Find(categoryId);
            category.Edit(newName);
            Dictionary<string, object> model = new Dictionary<string, object>();
            List<Item> categoryItems = category.GetItems();
            List<Item> allItems = Item.GetAll();
            model.Add("allItems", allItems);
            model.Add("items", categoryItems);
            model.Add("category", category);
            return View("Show", model);
        }

    }
}


