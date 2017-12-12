using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Autofac;
using NCore.Base.Commands;
using NCore.Base.Commands.Conventions;
using NCore.Base.Log;
using NLog;
using RogerRoger.Bin.Commands;

namespace RogerRoger.Bin
{
  class Program
  {
    static void Main(string[] args)
    {
      Run().Wait();
    }

    static async Task Run()
    {
      try
      {
        var logBuilder = new DefaultConfigBuilder();
        LogManager.Configuration = logBuilder.Build();

        var builder = new ContainerBuilder();
        new ServiceLocator().RegisterAllByConvention(builder);
        builder.RegisterModule<ProgramModule>();

        var container = builder.Build();
        var service = container.Resolve<ICommander>().Configure(container);

        var config = await service.Execute<LoadConfigCommand, Config>(new LoadConfigCommand());
        await service.Execute(new ProcessAllSqlFilesCommand()
        {
          Folder = config.Folder,
          Connection = config.Connection
        });
      }
      catch (Exception error)
      {
        RecursiveErrorReport(error);
      }
    }

    private static void RecursiveErrorReport(Exception rootError)
    {
      var logger = LogManager.GetCurrentClassLogger();
      var errors = new Queue<Exception>();
      errors.Enqueue(rootError);
      while (errors.Count > 0)
      {
        var error = errors.Dequeue();
        logger.Error($"Error: {error.Message}");
        if (error.InnerException != null)
        {
          errors.Enqueue(error.InnerException);
        }
      }
    }
  }
}