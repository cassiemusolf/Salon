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

        public void Save()
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("INSERT INTO stylists (name, phone) OUTPUT INSERTED.id VALUES (@StylistName, @StylistPhone);", conn);

            SqlParameter nameParameter = new SqlParameter();
            nameParameter.ParameterName = "@StylistName";
            nameParameter.Value = this.GetName();
            cmd.Parameters.Add(nameParameter);

            SqlParameter phoneParameter = new SqlParameter();
            phoneParameter.ParameterName = "@StylistPhone";
            phoneParameter.Value = this.GetPhone();
            cmd.Parameters.Add(phoneParameter);

            SqlDataReader rdr = cmd.ExecuteReader();

            while(rdr.Read())
            {
                this._id = rdr.GetInt32(0);
            }
            if (rdr != null)
            {
                rdr.Close();
            }
            if (conn != null)
            {
                conn.Close();
            }
        }

        public static Stylist Find(int id)
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM stylists WHERE id = @StylistId;", conn);

            SqlParameter stylistIdParameter = new SqlParameter();
            stylistIdParameter.ParameterName = "@StylistId";
            stylistIdParameter.Value = id.ToString();
            cmd.Parameters.Add(stylistIdParameter);
            SqlDataReader rdr = cmd.ExecuteReader();

            int foundStylistId = 0;
            string foundStylistName = null;
            string foundStylistPhone = null;
            while(rdr.Read())
            {
                foundStylistId = rdr.GetInt32(0);
                foundStylistName = rdr.GetString(1);
                foundStylistPhone = rdr.GetString(2);
            }
            Stylist foundStylist = new Stylist(foundStylistName, foundStylistPhone, foundStylistId);

            if (rdr != null)
            {
                rdr.Close();
            }
            if (conn != null)
            {
                conn.Close();
            }
            return foundStylist;
        }

        public List<Client> GetClients()
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM clients WHERE stylists_id = @StylistId;", conn);
            SqlParameter stylistIdParameter = new SqlParameter();
            stylistIdParameter.ParameterName = "@StylistId";
            stylistIdParameter.Value = this.GetId();
            cmd.Parameters.Add(stylistIdParameter);
            SqlDataReader rdr = cmd.ExecuteReader();

            List<Client> clients = new List<Client> {};
            while(rdr.Read())
            {
                int clientId = rdr.GetInt32(0);
                string clientName = rdr.GetString(1);
                string clientPhone = rdr.GetString(2);
                int clientStylistId = rdr.GetInt32(3);
                Client newClient = new Client(clientName, clientPhone, clientStylistId, clientId);
                clients.Add(newClient);
            }
            if (rdr != null)
            {
                rdr.Close();
            }
            if (conn != null)
            {
                conn.Close();
            }
            return clients;
        }

        public void Update(string newName)
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("UPDATE stylists SET name = @NewName OUTPUT INSERTED.name WHERE id = @StylistId;", conn);

            SqlParameter newNameParameter = new SqlParameter();
            newNameParameter.ParameterName = "@NewName";
            newNameParameter.Value = newName;
            cmd.Parameters.Add(newNameParameter);

            SqlParameter stylistIdParameter = new SqlParameter();
            stylistIdParameter.ParameterName = "@StylistId";
            stylistIdParameter.Value = this.GetId();
            cmd.Parameters.Add(stylistIdParameter);
            SqlDataReader rdr = cmd.ExecuteReader();

            while(rdr.Read())
            {
                this._name = rdr.GetString(0);
            }

            if (rdr != null)
            {
                rdr.Close();
            }

            if (conn != null)
            {
                conn.Close();
            }
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
