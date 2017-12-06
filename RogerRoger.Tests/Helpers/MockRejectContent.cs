using System;
using System.Threading.Tasks;
using RogerRoger.IO;

namespace RogerRoger.Tests.Helpers
{
  public class MockRejectContent : IFileProcessTask
  {
    private readonly string _badContent;

    public MockRejectContent(string badContent)
    {
      _badContent = badContent;
    }

    public Task<string> Process(string input)
    {
      if (input == _badContent) throw new Exception("Invalid content");
      return Task.Run(() => input);
    }
  }
}