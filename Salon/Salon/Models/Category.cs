using System.Collections.Generic;
using System;
using MySql.Data.MySqlClient;

namespace Salon.Models
{
    public class Category
    {
        private static List<Category> _instances = new List<Category> { };
        private string _name;
        private int _id;
        private List<Item> _items;

        public Category(string categoryName, int id = 0)
        {
            _name = categoryName;
            _id = id;
            _items = new List<Item> { };
        }

        public override int GetHashCode()
        {
            return this.GetId().GetHashCode();
        }

        public string GetName()
        {
            return _name;
        }

        public int GetId()
        {
            return _id;
        }

        public static void ClearAll()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"DELETE FROM categories;";
            cmd.ExecuteNonQuery();
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public static List<Category> GetAll()
        {
            List<Category> allCategories = new List<Category> { };
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM categories;";
            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            while (rdr.Read())
            {
                int CategoryId = rdr.GetInt32(0);
                string CategoryName = rdr.GetString(1);
                Category newCategory = new Category(CategoryName, CategoryId);
                allCategories.Add(newCategory);
            }
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return allCategories;
        }

        public static Category Find(int id)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM categories WHERE id = (@searchId);";
            MySqlParameter searchId = new MySqlParameter();
            searchId.ParameterName = "@searchId";
            searchId.Value = id;
            cmd.Parameters.Add(searchId);
            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            int CategoryId = 0;
            string CategoryName = "";
            while (rdr.Read())
            {
                CategoryId = rdr.GetInt32(0);
                CategoryName = rdr.GetString(1);
            }
            Category newCategory = new Category(CategoryName, CategoryId);
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return newCategory;
        }
        public List<Item> GetItems()
        {
            List<Item> allCategoryItems = new List<Item> { };
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM items WHERE category_id = @category_id;";
            MySqlParameter categoryId = new MySqlParameter();
            categoryId.ParameterName = "@category_id";
            categoryId.Value = this._id;
            cmd.Parameters.Add(categoryId);
            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            while (rdr.Read())
            {
                int itemId = rdr.GetInt32(0);
                string itemDescription = rdr.GetString(1);
                DateTime dd = rdr.GetDateTime(2);
                int itemCategoryId = rdr.GetInt32(3);
                DateTime endShift = rdr.GetDateTime(4);
                string cutInfo = rdr.GetString(5);
                string imgUrl = rdr.GetString(6);
                int price = rdr.GetInt32(7);
                Item newItem = new Item(itemDescription, dd, itemCategoryId, endShift, cutInfo, imgUrl, price,  itemId);
                allCategoryItems.Add(newItem);
            }
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return allCategoryItems;
        }



        public void AddItem(Item item)
        {
            _items.Add(item);
        }


        public override bool Equals(System.Object otherCategory)
        {
            if (!(otherCategory is Category))
            {
                return false;
            }
            else
            {
                Category newCategory = (Category)otherCategory;
                bool idEquality = this.GetId().Equals(newCategory.GetId());
                bool nameEquality = this.GetName().Equals(newCategory.GetName());
                return (idEquality && nameEquality);
            }
        }

        public void Save()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO categories (name) VALUES (@name);";
            MySqlParameter name = new MySqlParameter();
            name.ParameterName = "@name";
            name.Value = this._name;
            cmd.Parameters.Add(name);
            cmd.ExecuteNonQuery();
            _id = (int)cmd.LastInsertedId;
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

    }
}
