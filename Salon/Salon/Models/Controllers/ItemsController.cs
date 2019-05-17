using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Mvc;
using Salon.Models;

namespace Salon.Controllers
{
    public class ItemsController : Controller
    {

        [HttpGet("/categories/{categoryId}/items/new")]
        public ActionResult New(int categoryId)
        {
            Category category = Category.Find(categoryId);
            return View(category);
        }

        [HttpGet("/categories/{categoryId}/items/{itemId}")]
        public ActionResult Show(int itemId)
        {
            Item item = Item.Find(itemId);
            Dictionary<string, object> model = new Dictionary<string, object>();
            List<Category> ItemCat = item.GetCategories();
            List<Category> allCat = Category.GetAll();
            model.Add("item", item);
            model.Add("ItemCat", ItemCat);
            model.Add("allCat", allCat);
           // Category category = Category.Find(categoryId);
            //model.Add("item", item);
            //model.Add("category", category);
            return View(model);
        }

        [HttpPost("/items/delete")]
        public ActionResult DeleteAll()
        {
            Item.ClearAll();
            return View();
        }

        [HttpGet("/categories/{categoryId}/items/{itemId}/edit")]
        public ActionResult Edit(int categoryId, int itemId)
        {
            Dictionary<string, object> model = new Dictionary<string, object>();
            Category category = Category.Find(categoryId);
            model.Add("category", category);
            Item item = Item.Find(itemId);
            model.Add("item", item);
            return View(model);
        }

        [HttpPost("/categories/{categoryId}/items/{itemId}")]
        public ActionResult Update(int categoryId, int itemId, string newDescription, DateTime newShiftDate, DateTime newEndShift, string newCutInfo, string newImgUrl, int newPrice)
        {
            Item item = Item.Find(itemId);
            item.Edit(newDescription, newShiftDate, newEndShift, newCutInfo, newImgUrl, newPrice);
            Dictionary<string, object> model = new Dictionary<string, object>();
            Category category = Category.Find(categoryId);
            model.Add("category", category);
            model.Add("item", item);
            return View("Show", model);
        }

    }
}