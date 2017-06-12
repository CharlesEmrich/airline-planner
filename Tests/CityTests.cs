using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;
using AirlineProject.Objects;

namespace AirlineProject
{
  [Collection("AirlineTest")]
  public class CityTest :IDisposable
  {
    public CityTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=airline_test;Integrated Security=SSPI;";
    }
    public void Dispose()
    {
      City.DeleteAll();
    }

    [Fact]
    public void Test_Equal_ReturnsTrueForIdenticalObjects()
    {
      //Arrange, Act
      City firstCity = new City("Chicago");
      City secondCity = new City("Chicago");

      //Assert
      Assert.Equal(firstCity, secondCity);
    }
    [Fact]
    public void Test_CitiesEmptyAtFirst()
    {
      //Arrange, Act
      int result = City.GetAll().Count;

      //Assert
      Assert.Equal(0, result);
    }
    [Fact]
    public void Test_Save_SavesCityToDatabase()
    {
      //Arrange
      City testCity = new City("Chicago");
      testCity.Save();

      //Act
      List<City> result = City.GetAll();
      List<City> testList = new List<City>{testCity};

      //Assert
      Assert.Equal(testList, result);
    }
  }
}
