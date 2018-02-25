using System.Collections.Generic;
using MySql.Data.MySqlClient;
using HairSalon;
using System;

namespace HairSalon.Models
{
    public class Stylist
    {
        private int _id;
        private string _name;

        public Stylist(string name, int Id = 0)
        {
            _id = Id;
            _name = name;
        }

        // Getters

        public int GetId()
        {
            return _id;
        }
        public string GetName()
        {
            return _name;
        }
        // Setters
        public void SetId(int setId)
        {
            _id = setId;
        }
        public void SetName(string setName)
        {
            _name = setName;
        }
        public void Save()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO stylist (stylistname) VALUES (@stylistname);";

            MySqlParameter name = new MySqlParameter();
            name.ParameterName = "@stylistname";
            name.Value = this._name;
            cmd.Parameters.Add(name);

            cmd.ExecuteNonQuery();
            _id = (int) cmd.LastInsertedId;
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public static List<Stylist> GetAll()
        {
            List<Stylist> allStylist = new List<Stylist> {};
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM stylist;";
            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            while(rdr.Read())
            {
              int stylistId = rdr.GetInt32(0);
              string stylistName = rdr.GetString(1);

              Stylist newStylist = new Stylist(stylistName, stylistId);
              allStylist.Add(newStylist);
            }
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return allStylist;
        }


        public static Stylist Find(int id)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM stylist WHERE id = (@searchId);";

            MySqlParameter searchId = new MySqlParameter();
            searchId.ParameterName = "@searchId";
            searchId.Value = id;
            cmd.Parameters.Add(searchId);

            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            int stylistId = 0;
            string stylistName = "";

            while(rdr.Read())
            {
              stylistId = rdr.GetInt32(0);
              stylistName = rdr.GetString(1);
            }

            Stylist newStylist = new Stylist(stylistName, stylistId);
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }

            return newStylist;
        }

        public void Delete()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"DELETE FROM stylist WHERE id = @searchId;";

            MySqlParameter searchId = new MySqlParameter();
            searchId.ParameterName = "@searchId";
            searchId.Value = _id;
            cmd.Parameters.Add(searchId);

            cmd.ExecuteNonQuery();
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public static void DeleteAll()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"DELETE FROM stylist;";
            cmd.ExecuteNonQuery();
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }
    }
}
