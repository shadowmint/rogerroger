using System.IO.Abstractions;
using System.Threading.Tasks;
using RogerRoger.IO;

namespace RogerRoger.Transform.Standard
{
  public class ReadFileTask : IFileProcessTask
  {
    private readonly IFileSystem _fileSystem;

    public ReadFileTask(IFileSystem fileSystem)
    {
      _fileSystem = fileSystem;
    }

    public Task<string> Process(string input)
    {
      return Task.Run(() => _fileSystem.File.ReadAllText(input));
    }
  }
}