using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;
using AirlineProject.Objects;

namespace AirlineProject
{
  [Collection("AirlineTest")]
  public class FlightTest : IDisposable
  {
    public FlightTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=airline_test;Integrated Security=SSPI;";
    }
    public void Dispose()
    {
      Flight.DeleteAll();
    }

    [Fact]
    public void Test_Equal_ReturnsTrueIdenticalObjects()
    {
      //Arrange, Act
      Flight firstFlight = new Flight(new DateTime(2017, 6, 9, 14, 0, 0), "Chicago", "Boise", "On Time");
      Flight secondFlight = new Flight(new DateTime(2017, 6, 9, 14, 0, 0), "Chicago", "Boise", "On Time");

      //Assert
      Assert.Equal(firstFlight, secondFlight);
    }
    [Fact]
    public void Test_FlightsEmptyAtFirst()
    {
      //Arrange, Act
      int result = Flight.GetAll().Count;

      //Assert
      Assert.Equal(0, result);
    }
    [Fact]
    public void Test_Save_SavesFlightToDatabase()
    {
      //Arrange
      Flight testFlight = new Flight(new DateTime(2017, 6, 9, 14, 0, 0), "Chicago", "Boise", "On Time");
      testFlight.Save();

      //Act
      List<Flight> result = Flight.GetAll();
      List<Flight> testList = new List<Flight>{testFlight};

      //Assert
      Assert.Equal(testList, result);
    }
    [Fact]
    public void Test_Update_UpdatesFlightStatusInDatabase()
    {
      //Arrange
      Flight testFlight = new Flight(new DateTime(2017, 6, 9, 14, 0, 0), "Chicago", "Boise", "On Time");
      testFlight.Save();
      string newStatus = "Delayed";

      //Act
      testFlight.Update(newStatus);

      string result = testFlight.Status;

      //Assert
      Assert.Equal(newStatus, result);
    }
  }
}
