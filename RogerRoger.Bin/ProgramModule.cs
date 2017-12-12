using System.IO.Abstractions;
using Autofac;

namespace RogerRoger.Bin
{
  internal class ProgramModule : Module
  {
    protected override void Load(ContainerBuilder builder)
    {
      base.Load(builder);
      builder.RegisterType<FileSystem>().AsImplementedInterfaces().SingleInstance();
    }
  }
}