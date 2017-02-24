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
        private string _service;
        private DateTime _date;

        public Client(string Name, string Phone, string Service, DateTime Date, int Id = 0)
        {
            _id = Id;
            _name = Name;
            _phone = Phone;
            _service = Service;
            _date = Date;
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
        public string GetService()
        {
            return _service;
        }
        public void SetService(string newService)
        {
            _service = newService;
        }
        public DateTime GetDate()
        {
            return _date;
        }
        public void SetDate(DateTime newDate)
        {
            _date = newDate;
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
                string clientService = rdr.GetString(3);
                DateTime clientDate = rdr.GetDateTime(4);
                Client newClient = new Client(clientName, clientPhone, clientService, clientDate, clientId);
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
