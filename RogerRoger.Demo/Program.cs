using System;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using Microsoft.EntityFrameworkCore;

namespace RogerRoger.Demo
{
  class Program
  {
    static void Main(string[] args)
    {
      var connectionString = @"Server=.\RAMBO;Database=EFTripleJ;Trusted_Connection=True;";

      var connection = new SqlConnection(connectionString);

      using (IDbConnection dbConnection = connection)
      {
        string sQuery = "PRINT('hello world');";

        var x = dbConnection as SqlConnection;
        x.InfoMessage += conn_InfoMessage;

        dbConnection.Open();


        SqlCommand command = connection.CreateCommand();
        command.CommandText =
          "select @City as Message";

        SqlParameter param = new SqlParameter();
        param.ParameterName = "@City";
        param.Value = "NAME";

        command.Parameters.Add(param);

        SqlDataReader reader = command.ExecuteReader();

        if (reader.HasRows)
        {
          while (reader.Read())
          {
            Console.WriteLine("{0}", reader.GetString(0));
          }
        }
        else
        {
          Console.WriteLine("No rows found.");
        }

        reader.Close();
      }
    }

    static void conn_InfoMessage(object sender, SqlInfoMessageEventArgs e)
    {
      foreach (var error in e.Errors)
      {
        Console.WriteLine("---------------------------------------------------");
        Console.WriteLine("Source {0} $ Message{1} $ error{2}", e.Source, e.Message,
          error.ToString()
        );
      }
    }
  }
}