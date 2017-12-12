using System.Threading.Tasks;
using Autofac;
using NCore.Base.Commands;
using NCore.Base.Commands.Conventions;

namespace RogerRoger
{
  public class Commander : ICommander, ISingleton
  {
    private CommandService _serivce;

    public Task Execute<T>(T command) where T : ICommand
    {
      return _serivce.Execute<T>(command);
    }

    public Task<TRtn> Execute<T, TRtn>(T command) where T : ICommand
    {
      return _serivce.Execute<T, TRtn>(command);
    }

    public ICommander Configure(IContainer container)
    {
      _serivce = new CommandService(container);
      return this;
    }
  }
}