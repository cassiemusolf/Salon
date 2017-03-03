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
        private int _stylistId;

        public Client(string Name, string Phone, int StylistId, int Id = 0)
        {
            _id = Id;
            _name = Name;
            _phone = Phone;
            _stylistId = StylistId;
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
                bool stylistEquality = (this.GetStylistId() == newClient.GetStylistId());
                return (idEquality && nameEquality && phoneEquality && stylistEquality);
            }
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
        public int GetStylistId()
        {
            return _stylistId;
        }
        public void SetStylistId(int newStylistId)
        {
            _stylistId = newStylistId;
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
                int clientStylistId = rdr.GetInt32(3);

                Client newClient = new Client(clientName, clientPhone, clientStylistId, clientId);
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

            SqlCommand cmd = new SqlCommand("INSERT INTO clients (name, phone, stylists_id) OUTPUT INSERTED.id VALUES (@ClientName, @ClientPhone, @ClientStylistId);", conn);

            SqlParameter nameParameter = new SqlParameter();
            nameParameter.ParameterName = "@ClientName";
            nameParameter.Value = this.GetName();
            cmd.Parameters.Add(nameParameter);

            SqlParameter phoneParameter = new SqlParameter();
            phoneParameter.ParameterName = "@ClientPhone";
            phoneParameter.Value = this.GetPhone();
            cmd.Parameters.Add(phoneParameter);

            SqlParameter stylistIdParameter = new SqlParameter();
            stylistIdParameter.ParameterName = "@ClientStylistId";
            stylistIdParameter.Value = this.GetStylistId();
            cmd.Parameters.Add(stylistIdParameter);

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

        public static Client Find(int id)
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM clients WHERE id = @ClientId;", conn);

            SqlParameter clientIdParameter = new SqlParameter();
            clientIdParameter.ParameterName = "@ClientId";
            clientIdParameter.Value = id.ToString();
            cmd.Parameters.Add(clientIdParameter);
            SqlDataReader rdr = cmd.ExecuteReader();

            int foundClientId = 0;
            string foundClientName = null;
            string foundClientPhone = null;
            int foundClientStylistId = 0;

            while(rdr.Read())
            {
                foundClientId = rdr.GetInt32(0);
                foundClientName = rdr.GetString(1);
                foundClientPhone = rdr.GetString(2);
                foundClientStylistId = rdr.GetInt32(3);
            }
            Client foundClient = new Client(foundClientName, foundClientPhone, foundClientStylistId, foundClientId);

            if (rdr != null)
            {
              rdr.Close();
            }
            if (conn != null)
            {
              conn.Close();
            }
            return foundClient;
        }

        public void Update(string newName, string newPhone)
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("UPDATE clients SET name = @NewName, phone = @NewPhone OUTPUT INSERTED.* WHERE id = @ClientId;", conn);

            SqlParameter newNameParameter = new SqlParameter();
            newNameParameter.ParameterName = "@NewName";
            newNameParameter.Value = newName;
            cmd.Parameters.Add(newNameParameter);

            SqlParameter newPhoneParameter = new SqlParameter();
            newPhoneParameter.ParameterName = "@NewPhone";
            newPhoneParameter.Value = newPhone;
            cmd.Parameters.Add(newPhoneParameter);

            SqlParameter clientIdParameter = new SqlParameter();
            clientIdParameter.ParameterName = "@ClientId";
            clientIdParameter.Value = this.GetId();
            cmd.Parameters.Add(clientIdParameter);
            SqlDataReader rdr = cmd.ExecuteReader();

            while(rdr.Read())
            {
                this._name = rdr.GetName(1);
                this._phone = rdr.GetString(2);
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

        public void Delete()
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("DELETE FROM clients WHERE id = @ClientId", conn);

            SqlParameter clientIdParameter = new SqlParameter();
            clientIdParameter.ParameterName = "@ClientId";
            clientIdParameter.Value = this.GetId();

            cmd.Parameters.Add(clientIdParameter);
            cmd.ExecuteNonQuery();

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
