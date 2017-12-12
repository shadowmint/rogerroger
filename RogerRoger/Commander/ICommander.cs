using System.Threading.Tasks;
using Autofac;
using NCore.Base.Commands;

namespace RogerRoger
{
  public interface ICommander
  {
    Task Execute<T>(T command) where T : ICommand;
    Task<TRtn> Execute<T, TRtn>(T command) where T : ICommand;
    ICommander Configure(IContainer container);
  }
}