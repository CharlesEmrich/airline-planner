using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace AirlineProject.Objects
{
  public class Flight
  {
    public int Id { get; set; }
    public DateTime DepartureTime { get; set; }
    public string DepartureCity { get; set; }
    public string ArrivalCity { get; set; }
    public string Status { get; set; }

    //Id set to zero to avoid null exception being thrown
    public Flight(DateTime time, string departCity, string arrivalCity, string status, int id = 0)
    {
      Id = id;
      DepartureTime = time;
      DepartureCity = departCity;
      ArrivalCity = arrivalCity;
      Status = status;
    }
    public override bool Equals(System.Object otherFlight)
    {
        // format for checking data type: (variable) is (type)
        if (!(otherFlight is Flight))
        {
          return false;
        }
        else
        {
          Flight newFlight = (Flight) otherFlight;
          bool idEquality = (this.Id == newFlight.Id);
          bool departureTimeEquality = (this.DepartureTime == newFlight.DepartureTime);
          bool departureCityEquality = (this.DepartureCity == newFlight.DepartureCity);
          bool arrivalCityEquality = (this.ArrivalCity == newFlight.ArrivalCity);
          bool statusEquality = (this.Status == newFlight.Status);

          return (idEquality && departureTimeEquality && departureCityEquality && arrivalCityEquality && statusEquality);
        }
    }
    public void Save()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO flights (departure_time, departure_city, arrival_city, status) OUTPUT INSERTED.id VALUES (@DepartureTime, @DepartureCity, @ArrivalCity, @Status);", conn);

      SqlParameter departureTimeParameter = new SqlParameter("@DepartureTime", this.DepartureTime.ToString());
      cmd.Parameters.Add(departureTimeParameter);
      SqlParameter departureCityParameter = new SqlParameter("@DepartureCity", this.DepartureCity);
      cmd.Parameters.Add(departureCityParameter);
      SqlParameter arrivalCityParameter = new SqlParameter("@ArrivalCity", this.ArrivalCity);
      cmd.Parameters.Add(arrivalCityParameter);
      SqlParameter statusParameter = new SqlParameter("@Status", this.Status);
      cmd.Parameters.Add(statusParameter);
      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        this.Id = rdr.GetInt32(0);
      }

      if (rdr != null)
      {
        rdr.Close();
      }
      if(conn != null)
      {
        conn.Close();
      }
    }
    public static List<Flight> GetAll()
    {
      List<Flight> allFlights = new List<Flight>{};
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM flights;", conn);
      SqlDataReader rdr = cmd.ExecuteReader();
      while(rdr.Read())
      {
        int flightId = rdr.GetInt32(0);
        DateTime flightDate = Convert.ToDateTime(rdr.GetString(1));
        string flightDepartureCity = rdr.GetString(2);
        string flightArrivalCity = rdr.GetString(3);
        string flightStatus = rdr.GetString(4);
        Flight newFlight = new Flight(flightDate, flightDepartureCity, flightArrivalCity, flightStatus, flightId);
        allFlights.Add(newFlight);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return allFlights;
    }
    public void Update(string newStatus)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("UPDATE flights SET status = @NewStatus OUTPUT INSERTED.status WHERE id = @FlightId;", conn);
      SqlParameter newStatusParameter = new SqlParameter("@NewStatus", newStatus);
      cmd.Parameters.Add(newStatusParameter);
      SqlParameter flightIdParameter = new SqlParameter("@FlightId", this.Id);
      cmd.Parameters.Add(flightIdParameter);

      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        this.Status = rdr.GetString(0);
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
      SqlCommand cmd = new SqlCommand("DELETE FROM flights;", conn);
      cmd.ExecuteNonQuery();
      conn.Close();
    }
  }
}

// TODO: Start using SQL's datetime instead of string
// TODO: Use join table to track arrival, departure and connecting flight relationships.
