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

        public Item(string description, DateTime shiftDate, int categoryId, int id = 0)
        {
            _description = description;
            _shiftDate = shiftDate;
            _id = id;
            _categoryId = categoryId;
        }
        public DateTime GetDate()
        {
            return _shiftDate;
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
                Item newItem = new Item(itemDescription, shiftDate, itemCategoryId, itemId);
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
            cmd.CommandText = @"DELETE FROM items;";
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
            // while (rdr.Read())
            // {
            rdr.Read();
            itemId = rdr.GetInt32(0);
            itemDescription = rdr.GetString(1);
            DateTime shiftDate = rdr.GetDateTime(2);
            int catId = rdr.GetInt32(3);
            Item foundItem = new Item(itemDescription, shiftDate, catId, itemId);
            // }
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


        public void Edit(string newDescription, DateTime newShiftDate)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            var cmd2 = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"UPDATE items SET (description, shiftDate) = (@newDescription, @newShiftDate) WHERE id = @searchId;";
            MySqlParameter searchId = new MySqlParameter();
            searchId.ParameterName = "@searchId";
            searchId.Value = _id;
            cmd.Parameters.Add(searchId);
            MySqlParameter description = new MySqlParameter();
            description.ParameterName = "@newDescription";
            description.Value = newDescription;

            MySqlParameter shiftDate = new MySqlParameter();
            shiftDate.ParameterName = "@newShiftDate";
            shiftDate.Value = this._shiftDate;
            cmd.Parameters.Add(shiftDate);
            cmd.Parameters.Add(description);

            cmd.ExecuteNonQuery();
            _description = newDescription; // <--- This line is new!
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
            cmd.CommandText = @"INSERT INTO items (description, shiftDate, category_id) VALUES (@ItemDescription,  @ItemShiftDate, @CategoryId);";
            MySqlParameter description = new MySqlParameter();
            description.ParameterName = "@ItemDescription";
            description.Value = this._description;
            MySqlParameter shiftDate = new MySqlParameter();
            MySqlParameter category_id = new MySqlParameter();
            category_id.ParameterName = "@CategoryId";
            shiftDate.ParameterName = "@ItemShiftDate";
            category_id.Value = this._categoryId;
            shiftDate.Value = this._shiftDate;
            cmd.Parameters.Add(category_id);
            cmd.Parameters.Add(description);
            cmd.Parameters.Add(shiftDate);
            cmd.ExecuteNonQuery();
            _id = (int)cmd.LastInsertedId;
            _categoryId = (int)cmd.LastInsertedId;
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }


    }
}