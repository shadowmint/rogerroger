using NCore.Base.Commands;

namespace RogerRoger.Bin.Commands
{
  public class LoadConfigCommand : ICommand
  {
    public string ConfigFile = "config.json";
  }
}