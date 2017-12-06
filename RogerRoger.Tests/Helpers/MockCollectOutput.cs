using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using RogerRoger.IO;

namespace RogerRoger.Tests
{
  public class MockCollectOutput : IFileProcessTask
  {
    public ConcurrentBag<string> Inputs { get; } = new ConcurrentBag<string>();

    public Task<string> Process(string input)
    {
      Inputs.Add(input);
      return Task.Run(() => input);
    }
  }
}