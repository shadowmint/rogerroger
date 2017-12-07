using System;
using System.ComponentModel;
using Autofac;
using NCore.Base.Commands;
using NCore.Base.Commands.Conventions;
using NCore.Base.Log;
using RogerRoger.Bin.Commands;

namespace RogerRoger.Bin
{
  class Program
  {
    static void Main(string[] args)
    {
      var logBuilder = new DefaultConfigBuilder();
      logBuilder.Build();

      var builder = new ContainerBuilder();
      new ServiceLocator().RegisterAllByConvention(builder);

      var container = builder.Build();
      var service = new CommandService(container);
      service.Execute<ProcessAllSqlFilesCommand>().Wait();
    }
  }
}