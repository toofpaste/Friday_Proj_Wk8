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

        public Item(string description, DateTime shiftDate, int categoryId, DateTime endShift, string cutInfo, string imgUrl, int id = 0)
        {
            _description = description;
            _shiftDate = shiftDate;
            _endShift = endShift;
            _cutInfo = cutInfo;
            _id = id;
            -imgUrl = imgUrl;
            _categoryId = categoryId;
        }
        public DateTime GetDate()
        {
            return _shiftDate;
        }
        public string GetUrl()
        {
            reutnr _imgUrl;
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
                Item newItem = new Item(itemDescription, shiftDate, itemCategoryId, endShift, cutInfo, itemId);
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
            rdr.Read();
            int itemId = rdr.GetInt32(0);
            string itemDescription = rdr.GetString(1);
            DateTime shiftDate = rdr.GetDateTime(2);
            int catId = rdr.GetInt32(3);
            DateTime endShiftA = rdr.GetDateTime(4);
            string cutInfoA = rdr.GetString(5);
            Item foundItem = new Item(itemDescription, shiftDate, catId, endShiftA, cutInfoA, itemId);
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


        public void Edit(string newDescription, DateTime newShiftDate, DateTime newEndShift, string newCutInfo)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            var cmd2 = conn.CreateCommand() as MySqlCommand;
            var cmd3 = conn.CreateCommand() as MySqlCommand;
            var cmd4 = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"UPDATE items SET description = @newDescription WHERE id = @searchId;";
            cmd2.CommandText = @"UPDATE items SET shiftDate = @newShiftDate WHERE id = @searchId;";
            cmd3.CommandText = @"UPDATE items SET endShift = @newEndShift WHERE id = @searchId;";
            cmd4.CommandText = @"UPDATE items SET cutInfo = @newCutInfo WHERE id = @searchId;";
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

            cmd.ExecuteNonQuery();
            cmd2.ExecuteNonQuery();
            cmd3.ExecuteNonQuery();
            cmd4.ExecuteNonQuery();
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
            cmd.CommandText = @"INSERT INTO items (description, shiftDate, category_id, endShift, cutInfo) VALUES (@ItemDescription,  @ItemShiftDate, @CategoryId, @ItemEndShift, @ItemCutInfo);";
            MySqlParameter description = new MySqlParameter();
            description.ParameterName = "@ItemDescription";
            description.Value = this._description;
            MySqlParameter shiftDate = new MySqlParameter();
            MySqlParameter category_id = new MySqlParameter();
            MySqlParameter endShift = new MySqlParameter();
            MySqlParameter cutInfo = new MySqlParameter();
            category_id.ParameterName = "@CategoryId";
            shiftDate.ParameterName = "@ItemShiftDate";
            endShift.ParameterName = "@ItemEndShift";
            cutInfo.ParameterName = "@ItemCutInfo";
            category_id.Value = this._categoryId;
            shiftDate.Value = this._shiftDate;
            endShift.Value = this._endShift;
            cutInfo.Value = this._cutInfo;
            cmd.Parameters.Add(category_id);
            cmd.Parameters.Add(description);
            cmd.Parameters.Add(shiftDate);
            cmd.Parameters.Add(endShift);
            cmd.Parameters.Add(cutInfo);
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