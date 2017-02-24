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
            Client firstClient = new Client("Brittany", "253-234-5678", 1);
            Client secondClient = new Client("Brittany", "253-234-5678", 1);

            //Assert
            Assert.Equal(firstClient, secondClient);
        }

        [Fact]
        public void Test_Save_SavesToDatabase()
        {
            //Arrange
            Client testClient = new Client("Brittany", "253-234-5678", 1);

            //Act
            testClient.Save();
            List<Client> result = Client.GetAll();
            List<Client> testList = new List<Client>{testClient};

            //Assert
            Assert.Equal(testList, result);
        }

        [Fact]
        public void Test_Save_AssignsIdToObject()
        {
            //Arrange
            Client testClient = new Client("Brittany", "253-234-5678", 1);

            //Act
            testClient.Save();
            Client savedClient = Client.GetAll()[0];

            int result = savedClient.GetId();
            int testId = testClient.GetId();

            //Assert
            Assert.Equal(testId, result);
        }

        [Fact]
        public void Test_Find_FindsClientInDatabase()
        {
            //Arrange
            Client testClient = new Client("Brittany", "253-234-6789", 1);
            testClient.Save();

            //Act
            Client foundClient = Client.Find(testClient.GetId());

            //Assert
            Assert.Equal(testClient, foundClient);
        }

        [Fact]
        public void Test_GetClients_RetrievesAllClientsWithStylist()
        {
            Stylist testStylist = new Stylist("Becky", "253-234-6789");
            testStylist.Save();

            Client firstClient = new Client("Bella", "253-234-6789", testStylist.GetId());
            firstClient.Save();
            Client secondClient = new Client("Camille", "234-234-0494", testStylist.GetId());
            secondClient.Save();


            List<Client> testClientList = new List<Client> {firstClient, secondClient};
            List<Client> resultClientList = testStylist.GetClients();

            Assert.Equal(testClientList, resultClientList);
        }

        [Fact]
        public void Dispose()
        {
            Client.DeleteAll();
        }
    }
}
