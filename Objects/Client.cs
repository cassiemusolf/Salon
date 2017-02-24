using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace SalonApp
{
    public class Client
    {
        private int _id;
        private string _name;
        private string _phone;

        public Client(string Name, string Phone, int Id = 0)
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

        public override bool Equals(System.Object otherClient)
        {
            if(!(otherClient is Client))
            {
                return false;
            }
            else
            {
                Client newClient = (Client) otherClient;
                bool idEquality = (this.GetId() == newClient.GetId());
                bool nameEquality = (this.GetName() == newClient.GetName());
                bool phoneEquality = (this.GetPhone() == newClient.GetPhone());
                return (idEquality && nameEquality && phoneEquality);
            }
        }

        public static List<Client> GetAll()
        {
            List<Client> allClients = new List<Client>{};

            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM clients;", conn);
            SqlDataReader rdr = cmd.ExecuteReader();

            while(rdr.Read())
            {
                int clientId = rdr.GetInt32(0);
                string clientName = rdr.GetString(1);
                string clientPhone = rdr.GetString(2);

                Client newClient = new Client(clientName, clientPhone, clientId);
                allClients.Add(newClient);
            }

            if (rdr != null)
            {
                rdr.Close();
            }
            if (conn != null)
            {
                conn.Close();
            }

            return allClients;
        }

        public void Save()
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("INSERT INTO clients (name, phone) OUTPUT INSERTED.id VALUES (@ClientName, @ClientPhone);", conn);

            SqlParameter nameParameter = new SqlParameter();
            nameParameter.ParameterName = "@ClientName";
            nameParameter.Value = this.GetName();
            cmd.Parameters.Add(nameParameter);

            SqlParameter phoneParameter = new SqlParameter();
            phoneParameter.ParameterName = "@ClientPhone";
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

        public static void DeleteAll()
        {
            SqlConnection conn = DB.Connection();
            conn.Open();
            SqlCommand cmd = new SqlCommand("DELETE FROM clients;", conn);
            cmd.ExecuteNonQuery();
            conn.Close();
        }
    }
}
