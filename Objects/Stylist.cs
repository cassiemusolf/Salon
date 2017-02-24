using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace SalonApp
{
    public class Stylist
    {
        private int _id;
        private string _name;
        private string _phone;

        public Stylist(string Name, string Phone, int Id = 0)
        {
            _id = Id;
            _name = Name;
            _phone = Phone;
        }

        public int GetId()
        {
            return _id;
        }
        public string GetName()
        {
            return _name;
        }
        public void SetName(string newName)
        {
            _name = newName;
        }
        public string GetPhone()
        {
            return _phone;
        }
        public void SetPhone(string newPhone)
        {
            _phone = newPhone;
        }

        public override bool Equals(System.Object otherStylist)
        {
            if (!(otherStylist is Stylist))
            {
              return false;
            }
            else
            {
              Stylist newStylist = (Stylist) otherStylist;
              bool idEquality = this.GetId() == newStylist.GetId();
              bool nameEquality = this.GetName() == newStylist.GetName();
              bool phoneEquality = (this.GetPhone() == newStylist.GetPhone());
              return (idEquality && nameEquality && phoneEquality);
            }
        }

        public static List<Stylist> GetAll()
        {
            List<Stylist> allStylists = new List<Stylist>{};

            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM stylists;", conn);
            SqlDataReader rdr = cmd.ExecuteReader();

            while(rdr.Read())
            {
                int stylistId = rdr.GetInt32(0);
                string stylistName = rdr.GetString(1);
                string stylistPhone = rdr.GetString(2);

                Stylist newStylist = new Stylist(stylistName, stylistPhone, stylistId);
                allStylists.Add(newStylist);
            }

            if (rdr != null)
            {
                rdr.Close();
            }
            if (conn != null)
            {
                conn.Close();
            }

            return allStylists;
        }

        public static void DeleteAll()
        {
            SqlConnection conn = DB.Connection();
            conn.Open();
            SqlCommand cmd = new SqlCommand("DELETE FROM stylists;", conn);
            cmd.ExecuteNonQuery();
            conn.Close();
        }
    }
}
