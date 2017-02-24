using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace SalonApp
{
    public class StylistTest : IDisposable
    {
        public StylistTest()
        {
            DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=hair_salon_test;Integrated Security=SSPI;";
        }

        [Fact]
        public void Test_StylistsEmptyAtFirst()
        {
            //Arrange, Act
            int result = Stylist.GetAll().Count;

            //Assert
            Assert.Equal(0, result);
        }

        [Fact]
        public void Test_Equal_ReturnsTrueForSameStylist()
        {
          //Arrange, Act
          Stylist firstStylist = new Stylist("Becky", "253-234-5678");
          Stylist secondStylist = new Stylist("Becky", "253-234-5678");

          //Assert
          Assert.Equal(firstStylist, secondStylist);
        }

        [Fact]
        public void Test_Save()
        {
          //Arrange
          Stylist testStylist = new Stylist("Becky", "253-234-5678");
          testStylist.Save();

          //Act
          List<Stylist> result = Stylist.GetAll();
          List<Stylist> testList = new List<Stylist>{testStylist};

          //Assert
          Assert.Equal(testList, result);
        }

        [Fact]
        public void Test_Save_AssignsIdToObject()
        {
            //Arrange
            Stylist testStylist = new Stylist("Becky", "253-234-5678");

            //Act
            testStylist.Save();
            Stylist savedStylist = Stylist.GetAll()[0];

            int result = savedStylist.GetId();
            int testId = testStylist.GetId();

            //Assert
            Assert.Equal(testId, result);
        }

        [Fact]
        public void Test_Find_FindsStylistInDatabase()
        {
            //Arrange
            Stylist testStylist = new Stylist("Becky", "253-234-5678");
            testStylist.Save();

            //Act
            Stylist foundStylist = Stylist.Find(testStylist.GetId());

            //Assert
            Assert.Equal(testStylist, foundStylist);
        }

        [Fact]
        public void Test_Update_UpdatesStylistInDatabase()
        {
          //Arrange
          string name = "Becky";
          string phone = "253-234-5678";
          Stylist testStylist = new Stylist(name, phone);
          testStylist.Save();
          string newName = "Rebecca";

          //Act
          testStylist.Update(newName);

          string result = testStylist.GetName();

          //Assert
          Assert.Equal(newName, result);
        }

        public void Dispose()
        {
            Stylist.DeleteAll();
        }
    }
}
