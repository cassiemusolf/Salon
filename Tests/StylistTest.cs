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

        public void Dispose()
        {
            Client.DeleteAll();
            Stylist.DeleteAll();
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

    }
}
