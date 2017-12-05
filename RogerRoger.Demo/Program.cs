using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace RogerRoger.Demo
{
  class Program
  {
    static void Main(string[] args)
    {
      if (args.Length == 0)
      {
        Console.WriteLine("Usage: dotnet run rogerroger.dll config.json");
        Environment.Exit(1);
      }

      var rawConfig = File.ReadAllText(args[0]);
      var config = JsonConvert.DeserializeObject<Config>(rawConfig);
      foreach (var path in WalkFolder(config.Folder))
      {
        var connectionString = config.Connection;
        try
        {
          var connection = new SqlConnection(connectionString);
          connection.Open();
          ExecSql(connection, path);
        }
        catch (Exception error)
        {
          Console.WriteLine(error.Message);
        }
      }
    }

    private static void ExecSql(SqlConnection connection, string path)
    {
      Console.WriteLine(path);
      try
      {
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
      catch (Exception error)
      {
        Console.WriteLine(error.Message);
      }
    }

    private static IEnumerable<string> WalkFolder(string root)
    {
      var pending = new Queue<string>();
      pending.Enqueue(root);
      while (pending.Any())
      {
        var here = pending.Dequeue();
        foreach (string f in Directory.GetFiles(here))
        {
          yield return f;
        }

        foreach (string d in Directory.GetDirectories(here))
        {
          pending.Enqueue(d);
        }
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