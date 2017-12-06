using System.Threading.Tasks;

namespace RogerRoger.IO
{
  public interface IFileTransform
  {
    bool Matches(string path);
    Task<bool> Process(string path);
    void Success(string path);
    void Failed(string path);
  }
}