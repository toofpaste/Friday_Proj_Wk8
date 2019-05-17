using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace Salon.Models
{
    public class Item
    {
        private string _description;
        private DateTime _shiftDate;
        private int _id;
        private int _categoryId;
        private DateTime _endShift;
        private string _cutInfo;
        private string _imgUrl;
        private int _price;

        public Item(string description, DateTime shiftDate, int categoryId, DateTime endShift, string cutInfo, string imgUrl, int price, int id = 0)
        {
            _description = description;
            _shiftDate = shiftDate;
            _endShift = endShift;
            _cutInfo = cutInfo;
            _id = id;
            _price = price;
            _imgUrl = imgUrl;
            _categoryId = categoryId;
        }
        public DateTime GetDate()
        {
            return _shiftDate;
        }
        public int GetPrice()
        {
            return _price;
        }
        public string GetUrl()
        {
            return _imgUrl;
        }
        public DateTime GetEnd()
        {
            return _endShift;
        }
        public string GetInfo()
        {
            return _cutInfo;
        }
        public int GetCategoryId()
        {
            return _categoryId;
        }

        public string GetDescription()
        {
            return _description;
        }
        public void SetshiftDate(DateTime shiftDate)
        {
            _shiftDate = shiftDate;
        }

        public void SetDescription(string newDescription)
        {
            _description = newDescription;
        }

        public int GetId()
        {
            return _id;
        }

        public static List<Item> GetAll()
        {
            List<Item> allItems = new List<Item> { };
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM `items` ORDER BY `shiftDate`  ASC;";
            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
            while (rdr.Read())
            {
                int itemId = rdr.GetInt32(0);
                string itemDescription = rdr.GetString(1);
                DateTime shiftDate = rdr.GetDateTime(2);
                int itemCategoryId = rdr.GetInt32(3);
                DateTime endShift = rdr.GetDateTime(4);
                string cutInfo = rdr.GetString(5);
                string imgUrl = rdr.GetString(6);
                int price = rdr.GetInt32(7);
                Item newItem = new Item(itemDescription, shiftDate, itemCategoryId, endShift, cutInfo, imgUrl, price, itemId);
                allItems.Add(newItem);
            }
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return allItems;
        }

        public static void ClearAll()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"DELETE FROM `items`;";
            cmd.ExecuteNonQuery();
            conn.Close();
            if (conn != null)
            {
            conn.Dispose();
      }
        }

        public static Item Find(int id)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM `items` WHERE id = @thisId;";
            MySqlParameter thisId = new MySqlParameter();
            thisId.ParameterName = "@thisId";
            thisId.Value = id;
            cmd.Parameters.Add(thisId);
            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            int itemId = 0;
            string itemDescription = "";
            DateTime shiftDate = new DateTime(2011, 6, 10);
            int catId = 0;
            DateTime endShiftA = new DateTime(2011, 6, 10);
            string cutInfoA = "";
            string imgUrl = "";
            int price = 0;
            while(rdr.Read())
               {
                itemId = rdr.GetInt32(0);
                itemDescription = rdr.GetString(1);
                shiftDate = rdr.GetDateTime(2);
                catId = rdr.GetInt32(3);
                endShiftA = rdr.GetDateTime(4);
                cutInfoA = rdr.GetString(5);
                imgUrl = rdr.GetString(6);
                price = rdr.GetInt32(7);
                }
            Item foundItem = new Item(itemDescription, shiftDate, catId, endShiftA, cutInfoA,imgUrl, price, itemId);
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return foundItem;
        }

        public override bool Equals(System.Object otherItem)
        {
            if (!(otherItem is Item))
            {
                return false;
            }
            else
            {
                Item newItem = (Item)otherItem;
                bool idEquality = (this.GetId() == newItem.GetId());
                bool descriptionEquality = (this.GetDescription() == newItem.GetDescription());
                bool shiftDateEquality = (this.GetDate() == newItem.GetDate());
                bool categoryEquality = this.GetCategoryId() == newItem.GetCategoryId();
                return (idEquality && descriptionEquality && categoryEquality);
            }
        }


        public void Edit(string newDescription, DateTime newShiftDate, DateTime newEndShift, string newCutInfo, string newImgUrl, int newPrice)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            var cmd2 = conn.CreateCommand() as MySqlCommand;
            var cmd3 = conn.CreateCommand() as MySqlCommand;
            var cmd4 = conn.CreateCommand() as MySqlCommand;
            var cmd5 = conn.CreateCommand() as MySqlCommand;
            var cmd6 = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"UPDATE items SET description = @newDescription WHERE id = @searchId;";
            cmd2.CommandText = @"UPDATE items SET shiftDate = @newShiftDate WHERE id = @searchId;";
            cmd3.CommandText = @"UPDATE items SET endShift = @newEndShift WHERE id = @searchId;";
            cmd4.CommandText = @"UPDATE items SET cutInfo = @newCutInfo WHERE id = @searchId;";
            cmd5.CommandText = @"UPDATE items SET imgUrl = @newImgUrl WHERE id = @searchId;";
            cmd6.CommandText = @"UPDATE items SET price = @newPrice WHERE id = @searchId;";
            MySqlParameter searchId = new MySqlParameter();
            searchId.ParameterName = "@searchId";
            searchId.Value = _id;
            cmd.Parameters.Add(searchId);
            MySqlParameter description = new MySqlParameter();
            description.ParameterName = "@newDescription";
            description.Value = newDescription;

            _shiftDate = newShiftDate;
            cmd2.Parameters.Add(searchId);
            MySqlParameter shiftDate = new MySqlParameter();
            shiftDate.ParameterName = "@newShiftDate";
            shiftDate.Value = this._shiftDate;
            cmd2.Parameters.Add(shiftDate);
            cmd.Parameters.Add(description);

            _endShift = newEndShift;
            cmd3.Parameters.Add(searchId);
            MySqlParameter endShift = new MySqlParameter();
            endShift.ParameterName = "@newEndShift";
            endShift.Value = this._endShift;
            cmd3.Parameters.Add(endShift);


            _cutInfo = newCutInfo;
            cmd4.Parameters.Add(searchId);
            MySqlParameter cutInfo = new MySqlParameter();
           cutInfo.ParameterName = "@newCutInfo";
            cutInfo.Value = this._cutInfo;
            cmd4.Parameters.Add(cutInfo);

            _imgUrl = newImgUrl;
            cmd5.Parameters.Add(searchId);
            MySqlParameter imgUrl = new MySqlParameter();
            imgUrl.ParameterName = "@newImgUrl";
            imgUrl.Value = this._imgUrl;
            cmd5.Parameters.Add(imgUrl);

            _price = newPrice;
            cmd6.Parameters.Add(searchId);
            MySqlParameter price = new MySqlParameter();
            price.ParameterName = "@newPrice";
            price.Value = this._price;
            cmd6.Parameters.Add(price);

            cmd.ExecuteNonQuery();
            cmd2.ExecuteNonQuery();
            cmd3.ExecuteNonQuery();
            cmd4.ExecuteNonQuery();
            cmd5.ExecuteNonQuery();
            cmd6.ExecuteNonQuery();
            _description = newDescription;
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public void Save()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO items (description, shiftDate, category_id, endShift, cutInfo, imgUrl, price) VALUES (@ItemDescription,  @ItemShiftDate, @CategoryId, @ItemEndShift, @ItemCutInfo, @ItemImgUrl, @ItemPrice);";
            MySqlParameter description = new MySqlParameter();
            description.ParameterName = "@ItemDescription";
            description.Value = this._description;
            MySqlParameter shiftDate = new MySqlParameter();
            MySqlParameter category_id = new MySqlParameter();
            MySqlParameter endShift = new MySqlParameter();
            MySqlParameter cutInfo = new MySqlParameter();
            MySqlParameter imgUrl = new MySqlParameter();
            MySqlParameter price = new MySqlParameter();
            category_id.ParameterName = "@CategoryId";
            shiftDate.ParameterName = "@ItemShiftDate";
            endShift.ParameterName = "@ItemEndShift";
            cutInfo.ParameterName = "@ItemCutInfo";
            imgUrl.ParameterName = "@ItemImgUrl";
            price.ParameterName = "@ItemPrice";
            category_id.Value = this._categoryId;
            shiftDate.Value = this._shiftDate;
            endShift.Value = this._endShift;
            cutInfo.Value = this._cutInfo;
            imgUrl.Value = this._imgUrl;
            price.Value = this._price;
            cmd.Parameters.Add(category_id);
            cmd.Parameters.Add(description);
            cmd.Parameters.Add(shiftDate);
            cmd.Parameters.Add(endShift);
            cmd.Parameters.Add(cutInfo);
            cmd.Parameters.Add(imgUrl);
            cmd.Parameters.Add(price);
            cmd.ExecuteNonQuery();
            _id = (int)cmd.LastInsertedId;
            _categoryId = (int)cmd.LastInsertedId;
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

         public void Delete()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"DELETE FROM items WHERE id = @item_id; DELETE FROM categories_items WHERE item_id = @item_id;";
            MySqlParameter itemId = new MySqlParameter();
            itemId.ParameterName = "@item_id";
            itemId.Value = this.GetId();
            cmd.Parameters.Add(itemId);
            cmd.ExecuteNonQuery();
            if (conn != null)
            {
              conn.Close();
            }
        }

          public List<Category> GetCategories()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT categories.* FROM items
                JOIN categories_items ON (items.id = categories_items.item_id)
                JOIN categories ON (categories_items.category_id = categories.id)
                WHERE items.id = @item_id;";
            MySqlParameter itemId = new MySqlParameter();
            itemId.ParameterName = "@item_id";
            itemId.Value = _id;
            cmd.Parameters.Add(itemId);
            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            List<Category> categories = new List<Category> {};
            while(rdr.Read())
            {
                int catId = rdr.GetInt32(0);
                string catName = rdr.GetString(1);
                string cutInfo = rdr.GetString(2);
                Category foundCat = new Category(catName, cutInfo, catId);
                categories.Add(foundCat);
            }
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return categories;
        }
          public void AddCategory(Category newCategory)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO categories_items (category_id, item_id) VALUES (@category_id, @item_id);";
            MySqlParameter category_id = new MySqlParameter();
            category_id.ParameterName = "@category_id";
            category_id.Value = newCategory.GetId();
            cmd.Parameters.Add(category_id);
            MySqlParameter item_id = new MySqlParameter();
            item_id.ParameterName = "@item_id";
            item_id.Value = _id;
            cmd.Parameters.Add(item_id);
            cmd.ExecuteNonQuery();
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }



    }
}