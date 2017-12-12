using System.IO.Abstractions;
using System.Threading.Tasks;
using NCore.Base.Commands;
using Newtonsoft.Json;

namespace RogerRoger.Bin.Commands
{
  public class LoadConfigCommandHandler : ICommandHandler<LoadConfigCommand, Config>
  {
    private readonly IFileSystem _fileSystem;

    public LoadConfigCommandHandler(IFileSystem fileSystem)
    {
      _fileSystem = fileSystem;
    }
    
    public async Task<Config> Execute(LoadConfigCommand command)
    {
      var raw = _fileSystem.File.ReadAllText(command.ConfigFile);
      return JsonConvert.DeserializeObject<Config>(raw);
    }
  }
}