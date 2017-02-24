using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace SalonApp
{
    public class ClientTest : IDisposable
    {
        public ClientTest()
        {
            DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=hair_salon_test;Integrated Security=SSPI;";
        }

        [Fact]
        public void Test_DatabaseEmptyAtFirst()
        {
            //Arrange, Act
            int result = Client.GetAll().Count;

            //Assert
            Assert.Equal(0, result);
        }

        [Fact]
        public void Test_Equal_ReturnsTrueIfClientsAreTheSame()
        {
            //Arrange, Act
            Client firstClient = new Client("Brittany", "253-234-5678");
            Client secondClient = new Client("Brittany", "253-234-5678");

            //Assert
            Assert.Equal(firstClient, secondClient);
        }

        [Fact]
        public void Dispose()
        {
            Client.DeleteAll();
        }
    }
}
