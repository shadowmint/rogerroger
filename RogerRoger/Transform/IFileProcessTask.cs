using System.Threading.Tasks;

namespace RogerRoger.IO
{
  public interface IFileProcessTask
  {
    Task<string> Process(string input);
  }
}